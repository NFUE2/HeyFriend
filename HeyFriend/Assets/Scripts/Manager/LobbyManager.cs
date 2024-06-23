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



public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerCountText;
    public GameObject startBtn;

    private PhotonView photonView;

    private CameraManager cameraManager;

    private Dictionary<string , float> players = new Dictionary<string, float>();
    string key;

    string[] keys;
    float[] values;
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
        key = $"Player{newPlayer.ActorNumber}(Clone)";
        bool isFool = isFull();

        if (isFool) PhotonNetwork.CurrentRoom.IsOpen = false;

        if (isFool && PhotonNetwork.IsMasterClient)
            startBtn.SetActive(true);
        if(PhotonNetwork.IsMasterClient){
            players.Add(key, 0);
        }
        Debug.Log(key);
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
        if(players.ContainsKey(key)){
            players[key] = obj.transform.position.x;
        }else{
            players.Add(key, obj.transform.position.x);
        }
        ChangeToArray(players);
        photonView.RPC("AddPlayer",RpcTarget.All, playerNumber,obj.transform.position.x);
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
            players.Add(keys[i],values[i]);
        }
    }

    [PunRPC]
    public void AddPlayer(int num,float pos){
        cameraManager.players.Add("Player"+ num+"(Clone)", pos);        
    }
}
