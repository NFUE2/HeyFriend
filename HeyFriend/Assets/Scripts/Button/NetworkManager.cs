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

    public GameObject audioManager;
    //string isMaster;
    //GameObject obj;

    int roomCount;

    public void Awake()
    {
        Debug.Log("네트워크매니저 Awake");
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 10;
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.PhotonServerSettings.DevRegion = "";

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //서버접속
        if (PhotonNetwork.NetworkClientState == ClientState.PeerCreated){
            PhotonNetwork.ConnectUsingSettings();
        }else{
        }
            

        StartCoroutine(NetworkCheck());

    }
    public override void OnConnected()
    {
        Debug.Log("됌");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        roomCount = roomList.Count;

    }

    public void OnGameStart()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 2 };
        PhotonNetwork.JoinRandomOrCreateRoom(null,0,MatchmakingMode.FillRoom,null,null,$"Test{roomCount}",options);
        Debug.Log("게임시작 버튼 누름");
        Destroy(audioManager);
        //StartCoroutine(CheckChangeScene());
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("방생성 , 이름 : " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장 , 이름 : "+ PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("입장 실패 : Random" );
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("입장 실패 message : " +message);
    }
    private void Update()
    {
        //counttext.text = PhotonNetwork.LocalPlayer.get;
        text.text = PhotonNetwork.NetworkClientState.ToString();
    }

    IEnumerator NetworkCheck()
    {
        connectPanel.SetActive(true);
        Debug.Log("패널킴");
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