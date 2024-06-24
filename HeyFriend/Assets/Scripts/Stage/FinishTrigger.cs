using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public Stage stage;  
    [SerializeField]private int playerCount = 0;
    [SerializeField]private int totlaPlayerCount=0;
    private PhotonView pv;

    private void Awake(){
        pv = GetComponent<PhotonView>();
    }
    private void Start(){
        int playerCounta;
        totlaPlayerCount = PhotonNetwork.CountOfPlayersInRooms;
        playerCounta = PhotonNetwork.CountOfPlayers;
        Debug.Log("totlaPlayerCount" + totlaPlayerCount);
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
        
        if (playerCount >= 2)
        {
            stage.NextStage();
            playerCount = 0;
        }
    }
}