using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToStart : MonoBehaviour
{
    public void GoToStartBtn()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("StartScene");
    }
}
