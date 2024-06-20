using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class StageManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerCountText;
    private int playerCount;
    Color[] color = new Color[] { Color.yellow, Color.green, Color.blue, Color.red };


    private void Awake()
    {
        string player = PhotonNetwork.IsMasterClient ? "MasterPlayer" : "Player";
        PhotonNetwork.Instantiate(player,Vector2.zero,Quaternion.identity);

        GameObject obj = PhotonNetwork.Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        obj.GetComponent<SpriteRenderer>().color = color[PhotonNetwork.LocalPlayer.ActorNumber - 1];

        SetPlayerText();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        SetPlayerText();
    }

    private void SetPlayerText()
    {
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        playerCountText.text = playerCount.ToString();
    }
}
