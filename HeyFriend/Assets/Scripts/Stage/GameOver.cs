using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviourPunCallbacks, IPunObservable
{
    bool collision = false;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            if (collision)
            {
                var players = GameObject.FindGameObjectsWithTag("Player");

                foreach (var p in players)
                    p.GetComponent<PlayerManager>().SetPosition();

                collision = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collision = true;
            //other.gameObject.GetComponent<PlayerManager>().SetPosition();
        }
    }
}
