using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    private void OnCollisionEnter2D(Collision2D other) {
        rigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update(){
        Debug.Log(rigidbody2D.velocity);
    }
}
