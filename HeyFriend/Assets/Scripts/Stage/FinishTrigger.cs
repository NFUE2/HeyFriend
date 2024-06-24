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
            Debug.Log("µµÂø");
            stage.PlayerFinish(other.gameObject);
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
        if (playerCount >= 1)
        {
            stage.NextStage();
        }
    }

}