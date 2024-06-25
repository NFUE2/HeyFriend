using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToStart : MonoBehaviour
{

    void Update(){
        Debug.Log(PhotonNetwork.NetworkClientState);
    }
    public void GoToStartBtn()
    {
        Time.timeScale = 1.0f;

        PhotonNetwork.LeaveRoom();
        //PhotonNetwork.Disconnect();
        //StartCoroutine(ReturnStartScene());
    }

    IEnumerator ReturnStartScene(){
        Debug.Log("끊기시작");
        PhotonNetwork.Disconnect();
        Debug.Log("끊어짐");
        yield return null;
        // while (PhotonNetwork.NetworkClientState != ClientState.Disconnected){
        //     Debug.Log("while문진행");
        //     yield return null;

        // }
            
        // Debug.Log("while문 탈출");
        // PhotonNetwork.LoadLevel("StartScene");
        // Debug.Log("씬 호출");
    }

}
