using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public Coroutine coroutine;

    public void GameStartBtn()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1.0f);
        //SceneManager.LoadScene("MainScene"); //본문
        SceneManager.LoadScene(1); //임시조치

        StopCoroutine(LoadScene());
    }
}