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

    private void Awake()
    {
        instance = this;
        PhotonNetwork.AutomaticallySyncScene = true;

        //string player = PhotonNetwork.IsMasterClient ? "MasterPlayer" : "Player";
        int playerNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        GameObject obj = PhotonNetwork.Instantiate("Player" + playerNumber, new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log("실행");
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
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        playerCountText.text = playerCount.ToString();
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
    }
}
