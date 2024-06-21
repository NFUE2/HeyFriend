using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldObject : MonoBehaviour
{
    public AudioClip cilp;
    public AudioSource source;

    public Stage stage;
    private void Start()
    {
        GameObject objectManager = GameObject.Find("ObjectManager");
        stage = objectManager.GetComponent<Stage>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            stage.stagePoint += 1;
            source.PlayOneShot(cilp);
            gameObject.SetActive(false);
        }
    }
}
