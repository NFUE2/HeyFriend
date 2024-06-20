using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject votingPause;
    [SerializeField] private GameObject votingQuit;

    public void MenuBtn()
    {
        menu.SetActive(true);
    }

    public void VotingPause()
    {
        votingPause.SetActive(true);
        menu.SetActive(false);
    }

    public void VotingQuit()
    {
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