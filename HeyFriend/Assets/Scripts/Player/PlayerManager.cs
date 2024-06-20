using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Vector2 position = new Vector2(0,0);
    // Start is called before the first frame update
    private Rigidbody2D rigid;
    private PlayerMoveController playerMoveController;
    void Start()
    {
        rigid=GetComponent<Rigidbody2D>();
        playerMoveController = GetComponent<PlayerMoveController>();
        //PhotonNetwork.Instantiate("Player", position, Quaternion.identity);  
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D other) { //other값이 밟은 사람
        if(other.gameObject.CompareTag("Player")){
            other.transform.SetParent(transform);

        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerMoveController>().AddParentVelocity(rigid.velocity.x);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.transform.SetParent(null);
            other.gameObject.GetComponent<PlayerMoveController>().AddParentVelocity(0);
        }
    }
}
