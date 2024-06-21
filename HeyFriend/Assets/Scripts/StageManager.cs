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
    Color[] color = new Color[] { Color.yellow, Color.green, Color.blue, Color.red };

    PhotonView pv;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        pv = GetComponent<PhotonView>();

        string player = PhotonNetwork.IsMasterClient ? "MasterPlayer" : "Player";
        GameObject obj = PhotonNetwork.Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);

        pv.RPC("SpawnCharacter", RpcTarget.AllBuffered, obj);

        //obj.GetComponent<SpriteRenderer>().color = color[PhotonNetwork.LocalPlayer.ActorNumber - 1];
        //pv.RPC("SpawnCharacter", RpcTarget.OthersBuffered);
        //SpawnCharacter();
        //PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
        //pv.RPC("SpawnCharacter",)

        SetPlayerText();
    }

    [PunRPC]
    private void SpawnCharacter(GameObject obj)
    {
        Debug.Log(1);
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
}
