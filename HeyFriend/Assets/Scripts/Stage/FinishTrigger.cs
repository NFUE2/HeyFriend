using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public Stage stage;  
    private int playerCount = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            playerCount++;
            CheckPlayerCount();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            playerCount--;
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