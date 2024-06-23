using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEditor;

public class PlayerMoveController : MonoBehaviourPunCallbacks
{
    PlayerInputController playerInputController;
    Rigidbody2D rigidBody2D;
    Animator animator;
    SpriteRenderer spriteRenderer;
    PhotonView PV;
    int jumpParamToHash = Animator.StringToHash("Jump");
    int speedParamToHash = Animator.StringToHash("Speed");
    private Vector2 moveDirection;
    private Vector2 parentMove;
    public float gravityscale;
    public float velocty_y;
    public float jumpPower;
    public Rigidbody2D bottomPlayer;

    private bool isMove=false;
    [SerializeField] private float speed;


    //추가사항
    CameraManager cameraManager;
    // Start is called before the first frame update
    private void Awake() {
        playerInputController = GetComponent<PlayerInputController>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        PV = GetComponent<PhotonView>();

        //추가사항
        cameraManager = Camera.main.GetComponent<CameraManager>();
    }
    void Start()
    {
        if (!pv.IsMine) return;
        playerInputController.OnMoveEvent+= OnMove;
        playerInputController.OnJumpEvent+=JumpMoveMent;
    }
    void FixedUpdate()
    {
        if (!pv.IsMine) return;
        MoveMent();
        
        //추가사항
        PV.RPC("Pos",RpcTarget.All,gameObject.name,transform.position.x);
    }
    [PunRPC]
    void Pos(string name, float posX){
        cameraManager.players[name] = posX;
    }
    private void JumpMoveMent()
    {
        if(Math.Round(rigidBody2D.velocity.y,2)==0){
            rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetTrigger(jumpParamToHash);
        }
    }
    private void OnMove(Vector2 direction){
        moveDirection = direction* speed;
    }
    private void MoveMent()
    {
        moveDirection.y = rigidBody2D.velocity.y- gravityscale;
        if(bottomPlayer==null){
            rigidBody2D.velocity = moveDirection;
        }else{
            rigidBody2D.velocity = moveDirection + bottomPlayer.velocity;;
        }
        
        if (moveDirection.x != 0) PV.RPC("FilpXRPC", RpcTarget.AllBuffered, moveDirection.x);
        animator.SetFloat(speedParamToHash, Mathf.Abs(rigidBody2D.velocity.x));
        
        //추가사항

        // foreach(Rigidbody2D rb in bottomPlayer)
        // {
        //     Vector2 dir = moveDirection * Time.fixedDeltaTime;
        //     rb.MovePosition(rb.position + dir);
        // }
    }

    [PunRPC]
    void FilpXRPC(float x)
    {
        spriteRenderer.flipX = x <= 0; /*? true : false;*/
    }


    public void JumpAction(float jumpPower)
    {
        Debug.Log("점프액션");
        rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        animator.SetTrigger(jumpParamToHash);
    }

    // public void AddParentVelocity(float velocity_X){
    //     parentMove.x=velocity_X;
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision -> 나
        bool isFoot = collision.contacts[0].normal == Vector2.up;

        if (collision.gameObject.gameObject.CompareTag("Player") && isFoot)
            bottomPlayer=collision.gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.gameObject.CompareTag("Player"))
            bottomPlayer = null;
    }

}
