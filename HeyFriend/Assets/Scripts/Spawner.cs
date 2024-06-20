using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    string isMaster;
    GameObject obj;
    Color[] color = new Color[] {Color.yellow,Color.green,Color.blue,Color.red};
    private void Start(){
        isMaster = PhotonNetwork.IsMasterClient?"MasterPlayer":"Player";
        obj = PhotonNetwork.Instantiate(isMaster, transform.position, Quaternion.identity);
        //obj.GetComponent<SpriteRenderer>().color = color[PhotonNetwork.LocalPlayer.ActorNumber];
    }
}
