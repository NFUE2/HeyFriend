using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq.Expressions;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI text;
    private void Awake()
    {
        //서버접속
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnGameStart()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    private void Update()
    {
        text.text = PhotonNetwork.NetworkClientState.ToString();
    }
}