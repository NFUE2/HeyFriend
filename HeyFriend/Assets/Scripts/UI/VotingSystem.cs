using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class VotingSystem : MonoBehaviour
{
    private int pauseVote = 0;
    private int unpauseVote = 0;
    private int quitVote = 0;
    private int continueVote = 0;

    private int totalPlayer = 4;
    private int requiredVote = 3;

    public GameObject pauseMenu;
    public float pauseDuration;

    private bool isPaused = false;

    // 투표 초기화
    private void ResetVote()
    {
        pauseVote = 0;
        unpauseVote = 0;
        quitVote = 0;
        continueVote = 0;
    }

    // 일시정지 투표 처리
    public void VotePause()
    {
        if (isPaused) return;
        pauseVote++;
        CheckPauseVotes();
    }

    public void VoteUnpause()
    {
        if (isPaused) return;
        unpauseVote++;
        CheckPauseVotes();
    }

    // 게임 종료 투표 처리
    public void VoteQuit()
    {
        quitVote++;
        CheckQuitVotes();
    }

    public void VoteContinue()
    {
        continueVote++;
        CheckQuitVotes();
    }

    // 일시정지 투표 결과 확인
    private void CheckPauseVotes()
    {
        if (pauseVote >= requiredVote)
        {
            StartCoroutine(PauseGame());
        }
        else if (unpauseVote >= requiredVote)
        {
            ResetVote();
        }
    }

    // 게임 종료 투표 결과 확인
    private void CheckQuitVotes()
    {
        if (quitVote >= requiredVote)
        {
            SceneManager.LoadScene("StartScene");
        }
        else if (continueVote >= requiredVote)
        {
            ResetVote();
        }
    }

    // 일시정지 처리
    private IEnumerator PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        yield return new WaitForSecondsRealtime(pauseDuration);
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        ResetVote();
        isPaused = false;
    }
}
