using System.Collections; 
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpSpot : MonoBehaviour
{
    public float jumpPower=10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isFoot = collision.contacts[0].normal == Vector2.down;

        if (collision.gameObject.gameObject.CompareTag("Player") && isFoot)
        {
            collision.gameObject.GetComponent<PlayerMoveController>().JumpAction(jumpPower);
        }
    }
 

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    //other.contac
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("밟음");
    //        other.GetComponent<PlayerMoveController>().JumpAction(jumpPower);
    //    }
    //}

}
