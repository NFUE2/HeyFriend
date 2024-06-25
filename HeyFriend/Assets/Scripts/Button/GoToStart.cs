using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToStart : MonoBehaviourPunCallbacks
{
    public void GoToStartBtn()
    {
        Time.timeScale = 1.0f;
        StopAllCoroutines();
        
        StartCoroutine(ReturnStartScene());
    }

    IEnumerator ReturnStartScene(){
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.NetworkClientState != ClientState.Disconnected){
            yield return null;

        }
        PhotonNetwork.LoadLevel("StartScene");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("방 나감");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("끊김");
    }
}
