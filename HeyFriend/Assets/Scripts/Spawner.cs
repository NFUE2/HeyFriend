using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    string isMaster;
    private void Start(){
        isMaster = PhotonNetwork.IsMasterClient?"MasterPlayer":"Player";
        PhotonNetwork.Instantiate(isMaster, transform.position, Quaternion.identity);
    }
}
