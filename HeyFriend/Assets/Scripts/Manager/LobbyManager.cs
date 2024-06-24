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
using System;
using UnityEngine.TextCore.Text;



public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerCountText;
    public GameObject startBtn;

    private PhotonView photonView;

    private CameraManager cameraManager;

    string key;

    public string[] keys;
    public float[] values;
    //public GameObject player;

    //Color[] color = new Color[] { Color.yellow, Color.green, Color.blue, Color.red };

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        cameraManager = Camera.main.GetComponent<CameraManager>();
        StartCoroutine(CheckChangeScene());
        isFull();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    //참가자라면 모두 사용

    //public override void OnJoinedRoom()
    //{

    //    Debug.Log(1);
    //}
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        key = $"Player{newPlayer.ActorNumber}(Clone)";
        bool isFool = isFull();

        if (isFool) PhotonNetwork.CurrentRoom.IsOpen = false;

        if (isFool && PhotonNetwork.IsMasterClient)
            startBtn.SetActive(true);

        if(PhotonNetwork.IsMasterClient){
            cameraManager.players.Add(key, 0);
            ChangeToArray(cameraManager.players);
            photonView.RPC("SendDictionary",RpcTarget.OthersBuffered,keys,values);
        }
        Debug.Log("OnPlayerEnteredRoom"+ key);
    }
    [PunRPC]
    private void SendDictionary(string[] keys, float[] values){
        Debug.Log("받았다.");
        ChangeToDictioniary(keys,values);
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

        int playerNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        GameObject obj = PhotonNetwork.Instantiate("Player"+playerNumber, Vector3.zero, Quaternion.identity);
        key = $"Player{playerNumber}(Clone)";
        if (cameraManager.players.ContainsKey(key)){
            cameraManager.players[key] = obj.transform.position.x;
        }else{
            cameraManager.players.Add(key, obj.transform.position.x);
        }
        Debug.Log("CheckChangeScene" + key);
    }
    
    private void ChangeToArray(Dictionary<string, float> players)
    {
        keys = new string[players.Count];
        values = new float[players.Count];
        int i=0;
        foreach(string key in players.Keys){
            keys[i] = key;
            i++;
        }
        i =0;
        foreach(float value in players.Values){
            values[i] = value;
            i++;
        }
    }

    private void ChangeToDictioniary(string[] keys, float[] values)
    {
        for(int i=0; i<keys.Length;i++){
            if(cameraManager.players.ContainsKey(keys[i]))return;
            cameraManager.players.Add(keys[i],values[i]);
        }
    }

}
