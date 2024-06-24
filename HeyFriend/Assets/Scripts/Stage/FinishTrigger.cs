using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public Stage stage;  
    private int playerCount = 0;
    private int totlaPlayerCount=0;
    private PhotonView pv;

    private void Awake(){
        pv = GetComponent<PhotonView>();
    }
    private void Start(){
        totlaPlayerCount = PhotonNetwork.CountOfPlayersInRooms;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("들어옴");
        if (other.CompareTag("Player"))
        {
            pv.RPC("PlusPlayerCount",RpcTarget.All);
            CheckPlayerCount();
            stage.PlayerFinish(other.gameObject);
        }
    }
    [PunRPC]
    private void PlusPlayerCount(){
        playerCount++;
    }
    void CheckPlayerCount()
    {
        if (playerCount >= totlaPlayerCount)
        {
            stage.NextStage();
            playerCount = 0;
        }
    }
}