using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSpot : MonoBehaviour
{
    public float jumpPower=10f;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("밟음");
            other.GetComponent<PlayerMoveController>().JumpAction(jumpPower);
        }
    }

}
