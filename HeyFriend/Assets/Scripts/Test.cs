using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Test : MonoBehaviour
{

    private void Awake() {
        DontDestroyOnLoad(this);
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(PhotonNetwork.NetworkClientState);
    }

    public void DiconnectBtn(){
        PhotonNetwork.Disconnect();
    }
}
