using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;

public class VotingSystem : MonoBehaviourPunCallbacks
{
    private int pauseVote = 0;
    private int unpauseVote = 0;
    private int quitVote = 0;
    private int continueVote = 0;

    private int totalPlayer = 4;
    private int requiredVote = 3;

    public GameObject pauseMenu;
    public GameObject pausevotingPanel;
    public GameObject quitVotingPanel;
    public float pauseDuration;

    private bool isPaused = false;
    private bool isVoting = false;

    private HashSet<int> votedPlayers = new HashSet<int>();

    private enum VoteType { None, Pause, Quit }
    private VoteType currentVoteType = VoteType.None;

    private void ResetVote()
    {
        pauseVote = 0;
        unpauseVote = 0;
        quitVote = 0;
        continueVote = 0;
        isVoting = false;
        currentVoteType = VoteType.None;
        votedPlayers.Clear();
    }

    public void VotePause()
    {
        if (isPaused || isVoting || votedPlayers.Contains(PhotonNetwork.LocalPlayer.ActorNumber)) return;
        StartVoting(VoteType.Pause);
        votedPlayers.Add(PhotonNetwork.LocalPlayer.ActorNumber);
        photonView.RPC("RPC_VotePause", RpcTarget.All);
    }

    public void VoteUnpause()
    {
        if (isPaused || isVoting || votedPlayers.Contains(PhotonNetwork.LocalPlayer.ActorNumber)) return;
        StartVoting(VoteType.Pause);
        votedPlayers.Add(PhotonNetwork.LocalPlayer.ActorNumber);
        photonView.RPC("RPC_VoteUnpause", RpcTarget.All);
    }

    public void VoteQuit()
    {
        if (isVoting || votedPlayers.Contains(PhotonNetwork.LocalPlayer.ActorNumber)) return;
        StartVoting(VoteType.Quit);
        votedPlayers.Add(PhotonNetwork.LocalPlayer.ActorNumber);
        photonView.RPC("RPC_VoteQuit", RpcTarget.All);
    }

    public void VoteContinue()
    {
        if (isVoting || votedPlayers.Contains(PhotonNetwork.LocalPlayer.ActorNumber)) return;
        StartVoting(VoteType.Quit);
        votedPlayers.Add(PhotonNetwork.LocalPlayer.ActorNumber);
        photonView.RPC("RPC_VoteContinue", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_VotePause()
    {
        pauseVote++;
        CheckPauseVotes();
    }

    [PunRPC]
    private void RPC_VoteUnpause()
    {
        unpauseVote++;
        CheckPauseVotes();
    }

    [PunRPC]
    private void RPC_VoteQuit()
    {
        quitVote++;
        CheckQuitVotes();
    }

    [PunRPC]
    private void RPC_VoteContinue()
    {
        continueVote++;
        CheckQuitVotes();
    }

    private void CheckPauseVotes()
    {
        if (pauseVote >= requiredVote)
        {
            StartCoroutine(PauseGame());
        }
        else if (unpauseVote >= requiredVote)
        {
            ResetVote();
            CloseVotingPanels();
        }
    }

    private void CheckQuitVotes()
    {
        if (quitVote >= requiredVote)
        {
            SceneManager.LoadScene("StartScene");
        }
        else if (continueVote >= requiredVote)
        {
            ResetVote();
            CloseVotingPanels();
        }
    }

    private IEnumerator PauseGame()
    {
        isVoting = true;
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        yield return new WaitForSecondsRealtime(pauseDuration);
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        ResetVote();
        isPaused = false;
        CloseVotingPanels();
    }

    private void StartVoting(VoteType voteType)
    {
        if (currentVoteType == VoteType.None)
        {
            currentVoteType = voteType;
            isVoting = true;
            OpenVotingPanel(voteType);
        }
    }

    private void OpenVotingPanel(VoteType voteType)
    {
        switch (voteType)
        {
            case VoteType.Pause:
                pausevotingPanel.SetActive(true);
                break;
            case VoteType.Quit:
                quitVotingPanel.SetActive(true);
                break;
        }
    }

    private void CloseVotingPanels()
    {
        pausevotingPanel.SetActive(false);
        quitVotingPanel.SetActive(false);
    }
}
