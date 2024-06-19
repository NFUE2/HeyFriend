using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayButton : MonoBehaviour
{
    public GameObject HowToPlay;

    public void HowToPlayBtn()
    {
        HowToPlay.SetActive(true);
    }

    public void CloseBtn()
    {
        HowToPlay.SetActive(false);
    }
}