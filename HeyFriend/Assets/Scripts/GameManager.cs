using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    string player = "Player";
    
    private void Start()
    {
        PhotonNetwork.Instantiate(player, Vector3.zero,Quaternion.identity);
    }
}
