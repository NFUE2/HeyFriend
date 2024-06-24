using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Menu : MonoBehaviourPunCallbacks , IPunObservable
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject votingPause;
    [SerializeField] private GameObject votingQuit;
    public VotingSystem votingSystem;
    PhotonView pv;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Debug.Log(1);
        if (stream.IsWriting)
        {
            Debug.Log(1);
            stream.SendNext(votingPause.activeInHierarchy);
            //stream.SendNext(menu.activeInHierarchy);
            stream.SendNext(votingQuit.activeInHierarchy);
        }
        else if(stream.IsReading)
        {
            Debug.Log(2);
            votingPause.SetActive((bool)stream.ReceiveNext());
            //menu.SetActive((bool)stream.ReceiveNext());
            votingQuit.SetActive((bool)stream.ReceiveNext());
        }
    }

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    public void MenuBtn()
    {
        Debug.Log("메뉴오픈");
        if (votingSystem.OnVotingPanel|| votingSystem.isPaused) return;

        menu.SetActive(true);
    }

    //[PunRPC]
    //public void Test()
    //{
    //    votingPause.SetActive(true);
    //    menu.SetActive(false);
    //}

    public void VotingPause()
    {
        pv.RPC("VotingPauseRPC", RpcTarget.All);
        //votingSystem.OnVotingPanel = true;
        //Debug.Log("일시정지 투표 패널 키기");

        //votingPause.SetActive(true);
        //menu.SetActive(false);
    }

    [PunRPC]
    private void VotingPauseRPC()
    {
        votingSystem.OnVotingPanel = true;
        Debug.Log("일시정지 투표 패널 키기");

        votingPause.SetActive(true);
        menu.SetActive(false);
    }

    public void VotingQuit()
    {
        pv.RPC("VotingQuitRPC", RpcTarget.All);

        //votingSystem.OnVotingPanel = true;
        //Debug.Log("게임종료 투표 패널 켜ㅣ");
        //votingQuit.SetActive(true);
        //menu.SetActive(false);
    }

    [PunRPC]
    private void VotingQuitRPC()
    {
        votingSystem.OnVotingPanel = true;
        Debug.Log("게임종료 투표 패널 켜ㅣ");
        votingQuit.SetActive(true);
        menu.SetActive(false);
    }

    public void SoundBtn()
    {
        soundMenu.SetActive(true);
        menu.SetActive(false);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void CloseSoundMenu()
    {
        soundMenu.SetActive(false);
        menu.SetActive(true);
    }
}