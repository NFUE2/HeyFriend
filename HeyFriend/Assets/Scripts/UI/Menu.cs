using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject votingPause;
    [SerializeField] private GameObject votingQuit;
    public VotingSystem votingSystem;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(votingPause.activeInHierarchy);
            //stream.SendNext(menu.activeInHierarchy);
            stream.SendNext(votingQuit.activeInHierarchy);
        }
        else
        {
            votingPause.SetActive((bool)stream.ReceiveNext());
            //menu.SetActive((bool)stream.ReceiveNext());
            votingQuit.SetActive((bool)stream.ReceiveNext());
        }
    }

    public void MenuBtn()
    {
        Debug.Log("메뉴오픈");
        if (votingSystem.OnVotingPanel|| votingSystem.isPaused) return;
        
        menu.SetActive(true);
    }

    public void VotingPause()
    {
        votingSystem.OnVotingPanel = true;
        Debug.Log("일시정지 투표 패널 키기");
        votingPause.SetActive(true);
        menu.SetActive(false);
    }

    public void VotingQuit()
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