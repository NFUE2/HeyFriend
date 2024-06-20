using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Spawner : MonoBehaviour
{


    private void Start(){
        PhotonNetwork.Instantiate("Player",transform.position, Quaternion.identity);
    }
}
