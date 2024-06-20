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


    private void Awake()
    {
        string player = PhotonNetwork.IsMasterClient ? "MasterPlayer" : "Player";
        PhotonNetwork.Instantiate(player,Vector2.zero,Quaternion.identity);

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
