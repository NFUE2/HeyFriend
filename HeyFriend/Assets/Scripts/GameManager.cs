using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerCountText;
    public GameObject startBtn;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    //참가자라면 모두 사용
    public override void OnJoinedRoom()
    {
        isFull();
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(isFull() && PhotonNetwork.IsMasterClient)
            startBtn.SetActive(true);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(!isFull()) startBtn.SetActive(false);
    }

    bool isFull()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        playerCountText.text = playerCount.ToString();

        if (playerCount == PhotonNetwork.CurrentRoom.MaxPlayers) return true;

        return false;
    }

    public void OnClickStart()
    {
        int curLevel = SceneManager.GetActiveScene().buildIndex;

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        PhotonNetwork.LoadLevel(2);
        //PhotonNetwork.LoadLevel(curLevel + 1);
        //StartCoroutine(CheckChangeScene());
    }

    //IEnumerator CheckChangeScene()
    //{
        //int prevSceneNumber, curSceneNumber;

        //prevSceneNumber = curSceneNumber = SceneManager.GetActiveScene().buildIndex;

        //while (prevSceneNumber == curSceneNumber)
        //{
        //    curSceneNumber = SceneManager.GetActiveScene().buildIndex;
        //    yield return null;
        //}

        //GameObject go = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    //}
}
