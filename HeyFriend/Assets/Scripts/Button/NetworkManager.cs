using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq.Expressions;


public class NetworkManager : Singleton<NetworkManager>
{
    public TextMeshProUGUI text;
    public override void Awake()
    {
        base.Awake();

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