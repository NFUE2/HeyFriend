using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveSquare : MonoBehaviour
{
    [SerializeField]private int initalNumber=2;
    [SerializeField]private TextMeshProUGUI numberTxt;
    [SerializeField] private Dictionary<string,GameObject> playerLists = new Dictionary<string, GameObject>();

    Rigidbody2D rigid;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            GameObject obj;
            if (playerLists.TryGetValue(other.gameObject.name, out obj))
            {
                return;
            }
            playerLists.Add(other.gameObject.name, other.gameObject);
            numberTxt.text = (initalNumber - playerLists.Count).ToString();
        }
        if(playerLists.Count==initalNumber){
            rigid.constraints = RigidbodyConstraints2D.None;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        playerLists.Remove(other.gameObject.name);
        numberTxt.text = (initalNumber - playerLists.Count).ToString();
        if (playerLists.Count != initalNumber)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
