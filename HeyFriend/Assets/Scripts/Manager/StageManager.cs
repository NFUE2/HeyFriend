using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using Unity.VisualScripting;

public class StageManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerCountText;
    //Color[] color = new Color[] { Color.yellow, Color.green, Color.blue, Color.red };

    public static StageManager instance;
    public GameObject menu;

    private PhotonView PV;

    private CameraManager cameraManager;

    string key;

    public string[] keys;
    public float[] values;
    private void Awake()
    {
        instance = this;
        PhotonNetwork.AutomaticallySyncScene = true;

        //string player = PhotonNetwork.IsMasterClient ? "MasterPlayer" : "Player";
        int playerNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        GameObject obj = PhotonNetwork.Instantiate("Player" + playerNumber, new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log("실행");
        PV = GetComponent<PhotonView>();
        cameraManager = Camera.main.GetComponent<CameraManager>();
        //pv.RPC("SpawnCharacter", RpcTarget.AllBuffered, obj);

        //obj.GetComponent<SpriteRenderer>().color = color[PhotonNetwork.LocalPlayer.ActorNumber - 1];
        //pv.RPC("SpawnCharacter", RpcTarget.OthersBuffered);
        //SpawnCharacter();
        //PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
        //pv.RPC("SpawnCharacter",)

        SetPlayerText();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        SetPlayerText();
    }

    private void SetPlayerText()
    {
        if(PhotonNetwork.CurrentRoom == null) return;

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        playerCountText.text = playerCount.ToString();
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        key = $"Player{newPlayer.ActorNumber}(Clone)";

        if (PhotonNetwork.IsMasterClient)
        {
            if (cameraManager.players.ContainsKey(key)) return;
            cameraManager.players.Add(key, 0);
            ChangeToArray(cameraManager.players);
            PV.RPC("SendDictionary", RpcTarget.OthersBuffered, keys, values);
        }
    }

    [PunRPC]
    private void SendDictionary(string[] keys, float[] values)
    {
        Debug.Log("받았다.");
        ChangeToDictioniary(keys, values);
    }

    private void ChangeToArray(Dictionary<string, float> players)
    {
        keys = new string[players.Count];
        values = new float[players.Count];
        int i = 0;
        foreach (string key in players.Keys)
        {
            keys[i] = key;
            i++;
        }
        i = 0;
        foreach (float value in players.Values)
        {
            values[i] = value;
            i++;
        }
    }

    private void ChangeToDictioniary(string[] keys, float[] values)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (cameraManager.players.ContainsKey(keys[i])) return;
            cameraManager.players.Add(keys[i], values[i]);
        }
    }
}
