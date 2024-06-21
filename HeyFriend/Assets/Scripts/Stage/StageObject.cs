using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageObject : MonoBehaviour
{
    public Stage stage;
    private void Start()
    {
        GameObject objectManager = GameObject.Find("ObjectManager");
        stage = objectManager.GetComponent<Stage>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            stage.stagePoint += 1;
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Finish")
        {
            stage.NextStage();
        }
    }
}
