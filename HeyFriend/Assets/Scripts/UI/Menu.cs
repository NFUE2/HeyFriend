using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject votingPause;
    [SerializeField] private GameObject votingQuit;
    public VotingSystem votingSystem;

    public void MenuBtn()
    {
        Debug.Log("메뉴오픈");
        if (votingSystem.isVoting) return;
        
        menu.SetActive(true);
    }

    public void VotingPause()
    {
        
        Debug.Log("일시정지 투표 패널 키기");
        votingPause.SetActive(true);
        menu.SetActive(false);
    }

    public void VotingQuit()
    {
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