using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldObject : MonoBehaviour
{
    public AudioClip cilp;
    public AudioSource source;

    public Stage stage;

    private PhotonView pv;
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        GameObject objectManager = GameObject.Find("ObjectManager");
        GameObject audioManager = GameObject.Find("GoldSound");
        stage = objectManager.GetComponent<Stage>();
        source = audioManager.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pv.RPC("PlusStagePoint",RpcTarget.All);
            source.PlayOneShot(cilp);
            gameObject.SetActive(false);
        }
    }
    
    [PunRPC]
    private void PlusStagePoint(){
        stage.stagePoint += 1;
    }
}
