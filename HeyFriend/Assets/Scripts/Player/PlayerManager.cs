using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Vector2 position = new Vector2(0,0);
    // Start is called before the first frame update
    private Rigidbody2D rigid;
    private PlayerMoveController playerMoveController;
    private PhotonView photonView;

    private bool check;
    void Start()
    {
        rigid=GetComponent<Rigidbody2D>();
        playerMoveController = GetComponent<PlayerMoveController>();
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D other) {
        bool isFoot = other.contacts[0].normal == Vector2.down;
        if (other.gameObject.CompareTag("Player")&& isFoot)
        {
            other.transform.SetParent(transform);
            check = true;
        }
    }
    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")&& check)
        {
            other.gameObject.GetComponent<PlayerMoveController>().AddParentVelocity(rigid.velocity.x);

        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")&& check)
        {
            other.transform.SetParent(null);
            other.gameObject.GetComponent<PlayerMoveController>().AddParentVelocity(0);
            check = false;
        }
    }
    public void SetPosition(){
        photonView.RPC("SetPositionRPC",RpcTarget.All);
    }

    [PunRPC]
    private void SetPositionRPC(){
        transform.position = new Vector3(0, 0, 0);
    }
}
