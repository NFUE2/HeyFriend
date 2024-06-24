using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class FallingCloud : MonoBehaviour
{
    private Rigidbody2D rigid;
    private BoxCollider2D boxCollider2D;
    private PhotonView pv;
    private Animator anim;
    // Start is called before the first frame update
    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        pv = GetComponent<PhotonView>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        bool isFoot = other.contacts[0].normal == Vector2.up;
        if (other.gameObject.CompareTag("Player")&& isFoot)
        {
            //pv.RPC("FallingCoroutine",RpcTarget.All);
            StartCoroutine(Falling());
        }
    }
    [PunRPC]
    public void FallingCoroutine(){
        StartCoroutine(Falling());
    }
    IEnumerator Falling(){
        yield return new WaitForSecondsRealtime(0.3f);
        anim.SetTrigger("On");
        yield return new WaitForSecondsRealtime(1f);
        rigid.constraints = RigidbodyConstraints2D.None;
        boxCollider2D.isTrigger = true;
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
    }
}
