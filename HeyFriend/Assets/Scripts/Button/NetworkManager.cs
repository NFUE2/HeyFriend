using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq.Expressions;
using Photon.Pun.UtilityScripts;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance { get; private set; }

    public TextMeshProUGUI text;
    //public TextMeshProUGUI counttext;

    public GameObject connectPanel;

    //string isMaster;
    //GameObject obj;

    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //서버접속
        PhotonNetwork.ConnectUsingSettings();

        StartCoroutine(NetworkCheck());

        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void OnGameStart()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 4 };

        PhotonNetwork.JoinRandomOrCreateRoom(null,0,MatchmakingMode.FillRoom,null,null,"Test",options);
        //StartCoroutine(CheckChangeScene());
    }

    private void Update()
    {
        //counttext.text = PhotonNetwork.LocalPlayer.get;
        text.text = PhotonNetwork.NetworkClientState.ToString();
    }

    IEnumerator NetworkCheck()
    {
        connectPanel.SetActive(true);
        
        while (PhotonNetwork.NetworkClientState != ClientState.ConnectedToMasterServer)
            yield return null;

        connectPanel.SetActive(false);
    }

    //IEnumerator CheckChangeScene()
    //{
    //    PhotonNetwork.LoadLevel(1);

    //    //int prevSceneNumber, curSceneNumber;
    //    //prevSceneNumber = curSceneNumber = SceneManager.GetActiveScene().buildIndex;

    //    while (PhotonNetwork.NetworkClientState != ClientState.Joined)
    //    {
    //        //curSceneNumber = SceneManager.GetActiveScene().buildIndex;
    //        yield return null;
    //    }

    //    //GameObject obj = PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
    //    //isMaster = PhotonNetwork.IsMasterClient ? "MasterPlayer" : "Player";
    //}



    //IEnumerator CreatePlayer()
    //{
    //    yield return new WaitForSeconds(2.0f);

    //    PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    //}

    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //    base.OnPlayerEnteredRoom(newPlayer);
    //    Debug.Log(1);
    //}
}