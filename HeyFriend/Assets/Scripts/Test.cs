using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    private PhotonView pv;

    private void Awake(){
        pv = GetComponent<PhotonView>();
    }
    public void OnButton(){
        pv.RPC("LoadLevel",RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void LoadLevel(){
        Debug.Log("불러왔음");
        PhotonNetwork.LoadLevel("Stage 2");
    }
}
