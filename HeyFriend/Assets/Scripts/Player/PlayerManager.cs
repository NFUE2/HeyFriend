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
    private void OnCollisionEnter2D(Collision2D other) {
        bool isFoot = other.contacts[0].normal == Vector2.down;
        if (other.gameObject.CompareTag("Player")&& isFoot)
        {
            Debug.Log("비비는 중 ");
            other.gameObject.GetComponent<PlayerMoveController>().AddParentVelocity(rigid.velocity.x);
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
