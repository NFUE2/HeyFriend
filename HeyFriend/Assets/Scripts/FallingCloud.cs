using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCloud : MonoBehaviour
{
    private Rigidbody2D rigid;
    private BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            StartCoroutine(Falling());
        }
    }
    IEnumerator Falling(){
        yield return new WaitForSecondsRealtime(1f);
        rigid.constraints = RigidbodyConstraints2D.None;
        boxCollider2D.isTrigger = true;
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
    }
}
