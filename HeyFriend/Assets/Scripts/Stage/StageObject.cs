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
        if (collision.gameObject.CompareTag("Coin"))
        {
            stage.stagePoint += 1;
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                stage.PlayerFinish(collision.gameObject);
            }
        }
}}
