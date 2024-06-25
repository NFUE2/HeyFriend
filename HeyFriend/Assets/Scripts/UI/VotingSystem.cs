using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;


public class VotingSystem : MonoBehaviourPunCallbacks, IPunObservable
{
    private int pauseVote = 0; // 일시정지 찬성투표값
    private int unpauseVote = 0; // 일시정지 반대투표값
    private int quitVote = 0; // 게임종료 찬성투표값
    private int continueVote = 0; // 게임종료 반대투표값

    private int requiredVote = 3; // 과반 인원

    public GameObject pauseMenu; // 일시정지 이미지
    public GameObject pausevotingPanel; // 일시정지 투표 패널
    public GameObject quitVotingPanel; // 게임종료 투표 패널
    public float pauseDuration; // 일시정지되는 시간

    public bool isPaused = false; // 일시정지 bool값
    private bool isVoting = false; // 투표진행 bool값
    public bool OnVotingPanel = false;
    private HashSet<int> votedPlayers = new HashSet<int>(); // 투표한 플레이어 목록

    private enum VoteType { None, Pause, Quit } // 투표 타입값
    private VoteType currentVoteType = VoteType.None; // 현재 진행 중인 투표 종류
    private PhotonView PV;

    void Awake(){
        PV = GetComponent<PhotonView>();
    }

    private void ResetVote() // 투표 리셋 함수
    {
        pauseVote = 0;
        unpauseVote = 0;
        quitVote = 0;
        continueVote = 0;
        isVoting = false;
        currentVoteType = VoteType.None;
        votedPlayers.Clear(); // 투표인원 초기화
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            stream.SendNext(pauseVote);
            stream.SendNext(unpauseVote);
            stream.SendNext(quitVote);
            stream.SendNext(continueVote);
        }
        else if(stream.IsReading)
        {
            pauseVote = (int)stream.ReceiveNext();
            unpauseVote = (int)stream.ReceiveNext();
            quitVote = (int)stream.ReceiveNext();
            continueVote = (int)stream.ReceiveNext();
        }
    }

    public void VotePause() // 일시정지 찬성투표
    {
        if (isPaused || isVoting || votedPlayers.Contains(PhotonNetwork.LocalPlayer.ActorNumber)) return;
        // 투표가 진행 중이거나 일시정지 상태거나 서버에 포함된 투표 인원이면 리턴해라
        StartVoting(VoteType.Pause); // 투표를 시작해라 (투표종류 일시정지)
        votedPlayers.Add(PhotonNetwork.LocalPlayer.ActorNumber); // 서버에서 투표 인원을 더해라
        PV.RPC("RPC_VotePause", RpcTarget.All); // RPC_VotePause 를 RPC에 포함된 모두에게 발생시켜라
    }

    public void VoteUnpause() // 일시정지 반대투표
    {
        if (isPaused || isVoting || votedPlayers.Contains(PhotonNetwork.LocalPlayer.ActorNumber)) return;
        // 투표가 진행 중이거나 일시정지 상태거나 서버에 포함된 투표 인원이면 리턴해라
        StartVoting(VoteType.Pause); // 투표를 시작해라 (투표종류 일시정지)
        votedPlayers.Add(PhotonNetwork.LocalPlayer.ActorNumber); // 서버에서 투표 인원을 더해라
        PV.RPC("RPC_VoteUnpause", RpcTarget.All); // RPC_VoteUnpause 를 RPC에 포함된 모두에게 발생시켜라
    }

    public void VoteQuit() // 게임종료 찬성투표
    {
        if (isVoting || votedPlayers.Contains(PhotonNetwork.LocalPlayer.ActorNumber)) return;
        // 투표가 진행 중이거나 서버에 포함된 투표 인원이면 리턴해라
        StartVoting(VoteType.Quit); // 투표를 시작해라 (투표종류 게임종료)
        votedPlayers.Add(PhotonNetwork.LocalPlayer.ActorNumber); // 서버에서 투표 인원을 더해라
        PV.RPC("RPC_VoteQuit", RpcTarget.All); // RPC_VoteQuit 를 RPC에 포함된 모두에게 발생시켜라
    }

    public void VoteContinue()
    {
        if (isVoting || votedPlayers.Contains(PhotonNetwork.LocalPlayer.ActorNumber)) return;
        // 투표가 진행 중이거나 서버에 포함된 투표 인원이면 리턴해라
        StartVoting(VoteType.Quit); // 투표를 시작해라 (투표종류 게임종료)
        votedPlayers.Add(PhotonNetwork.LocalPlayer.ActorNumber); // 서버에서 투표 인원을 더해라
        PV.RPC("RPC_VoteContinue", RpcTarget.All); // RPC_VoteContinue 를 RPC에 포함된 모두에게 발생시켜라
    }

    [PunRPC]
    private void RPC_VotePause() // RPC 일시정지 찬성투표
    {
        pauseVote++; // 일시정지 찬성 투표수 더해라
        CheckPauseVotes(); // 해당 함수 적용
    }

    [PunRPC]
    private void RPC_VoteUnpause() // RPC 일시정지 반대투표
    {
        unpauseVote++; // 일시정지 반대 투표수 더해라
        CheckPauseVotes(); // 해당 함수 적용
    }

    [PunRPC]
    private void RPC_VoteQuit() // RPC 게임종료 찬성투표
    {
        quitVote++; // 게임종료 찬성 투표수 더해라
        CheckQuitVotes(); // 해당 함수 적용
    }

    [PunRPC]
    private void RPC_VoteContinue() // RPC 게임종료 반대투표
    {
        continueVote++; // 게임종료 반대 투표수 더해라
        CheckQuitVotes(); // 해당 함수 적용
    }

    private void CheckPauseVotes() // 일시정지 투표 확인
    {
        if (pauseVote >= requiredVote) // 일시정지 찬성투표가 과반수보다 많거나 같으면
        {
            StartCoroutine(PauseGame()); // 게임 일시정지 코루틴을 시작해라
        }
        else if (unpauseVote >= requiredVote) // 일시정지 반대투표가 과반수보다 많거나 같으면
        {
            ResetVote(); // 투표를 리셋해라
            CloseVotingPanels(); // 투표 패널을 닫아라
        }
        OnVotingPanel = false;
    }

    private void CheckQuitVotes() // 게임종료 투표 확인
    {
        if (quitVote >= requiredVote) // 게임종료 찬성투표가 과반수보다 많거나 같으면
        {
            PhotonNetwork.AutomaticallySyncScene = false;
            StartCoroutine(ReturnStartScene());
            //pv.RPC("QuitRPC",RpcTarget.All);
            //PhotonNetwork.LeaveRoom();
            //PhotonNetwork.Disconnect();
            //PhotonNetwork.LoadLevel("StartScene"); // 스타트씬을 불러와라
        }
        else if (continueVote >= requiredVote) // 게임종료 반대투표가 과반수보다 많거나 같으면
        {
            ResetVote(); // 투표를 리셋해라
            CloseVotingPanels(); // 투표 패널을 닫아라
        }
        OnVotingPanel = false;
    }

    //[PunRPC]
    //private void QuitRPC()
    //{
    //    PhotonNetwork.LeaveRoom();
    //    PhotonNetwork.LoadLevel("StartScene"); // 스타트씬을 불러와라
    //}

    //--------------------------------------------------------투표가 과반수보다 높으면 일시정지를 한다.
    private IEnumerator PauseGame() // 코루틴 일시정지게임 로직
    {
        
        isVoting = true; // 투표진행 켜라
        isPaused = true; // 일시정지 켜라
        pauseMenu.SetActive(true); // 일시정지 이미지를 켜라
        CloseVotingPanels(); // 투표 패널을 닫아라
        Time.timeScale = 0f; // 시간을 멈춰라
        yield return new WaitForSecondsRealtime(pauseDuration); // 게임 시간을 멈췄으니 실제 시간으로 일시정지 시간을 가게 해라
        Time.timeScale = 1f; // 끝났으면 다시 시간을 흐르게 해라
        pauseMenu.SetActive(false); // 일시정지 메뉴를 닫아라
        ResetVote(); // 투표를 리셋해라
        isPaused = false; // 일시정지 풀어라
    }

    private void StartVoting(VoteType voteType) // 투표시작
    {
        //---------------------------------------------투표 용지가 켜져있고 투표 버튼을 눌렀을 때 
        Debug.Log("투표용지를 눌렀을 때 " + VoteType.None);
        if (currentVoteType == VoteType.None) // 현재 투표값이 아무것도 없다면
        {
            currentVoteType = voteType; // 현재 투표값은 voteType 변수값 적용
            isVoting = true; // 투표진행 켜라
        }
    }

    private void CloseVotingPanels() // 투표 패널 닫기
    {
        pausevotingPanel.SetActive(false); // 일시정지 투표 패널 꺼라
        quitVotingPanel.SetActive(false); // 게임종료 투표 패널 꺼라
    }

    IEnumerator ReturnStartScene()
    {
        GameObject[] players = StageManager.instance.players;

        for(int i = players.Length - 1; i >= 0; i--)
        {
            if (players[i] == null) continue;
            PhotonNetwork.RemoveRPCs(players[i].GetComponent<PhotonView>());
            PhotonNetwork.Destroy(players[i]);
        }

        Camera.main.GetComponent<CameraManager>().players.Clear();



            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LeaveLobby();

            while (PhotonNetwork.NetworkClientState != ClientState.ConnectedToMasterServer)
                yield return null;
            PhotonNetwork.LoadLevel("StartScene");
            

    }

    public override void OnLeftRoom()
    {
        Debug.Log("방나옴");
    }
}
