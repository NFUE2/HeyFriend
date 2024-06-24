using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public Stage stage;  
    private int playerCount = 0;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("들어옴");
        if (other.CompareTag("Player"))
        {
            playerCount++;
            CheckPlayerCount();
            stage.PlayerFinish(other.gameObject);
        }
    }

    void CheckPlayerCount()
    {
        if (playerCount >= 4)
        {
            stage.NextStage();
        }
    }
}