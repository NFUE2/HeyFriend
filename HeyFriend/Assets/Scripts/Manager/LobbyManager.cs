using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.ComponentModel;
using System.IO;



public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerCountText;
    public GameObject startBtn;

    private PhotonView photonView;

    private CameraManager cameraManager;
    int playerNumber;
    GameObject obj;
    //public GameObject player;

    //Color[] color = new Color[] { Color.yellow, Color.green, Color.blue, Color.red };

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        cameraManager = Camera.main.GetComponent<CameraManager>();
        StartCoroutine(CheckChangeScene());
        isFull();
    }
    //참가자라면 모두 사용

    //public override void OnJoinedRoom()
    //{
        
    //    Debug.Log(1);
    //}
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        bool isFool = isFull();

        if (isFool) PhotonNetwork.CurrentRoom.IsOpen = false;

        if (isFool && PhotonNetwork.IsMasterClient)
            startBtn.SetActive(true);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(!isFull())
        {
            startBtn.SetActive(false);
            PhotonNetwork.CurrentRoom.IsOpen = true;
        }
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
    }

    IEnumerator CheckChangeScene()
    {
        while (PhotonNetwork.NetworkClientState != ClientState.Joined)
            yield return null;

        playerNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        obj = PhotonNetwork.Instantiate("Player"+playerNumber, Vector3.zero, Quaternion.identity);
        photonView.RPC("AddPlayer",RpcTarget.All);
    }
    [PunRPC]
    public void AddPlayer(){
        cameraManager.players.Add("Player"+ playerNumber, obj);        
    }
}
