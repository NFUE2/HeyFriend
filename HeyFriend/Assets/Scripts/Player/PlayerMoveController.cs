using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    private bool isMove=false;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    private void Awake() {
        playerInputController = GetComponent<PlayerInputController>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        playerInputController.OnMoveEvent+= OnMove;
        playerInputController.OnJumpEvent+=JumpMoveMent;
    }
    void FixedUpdate()
    {
        MoveMent();
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
        rigidBody2D.velocity = moveDirection+ parentMove;
        if (moveDirection.x != 0) PV.RPC("FilpXRPC", RpcTarget.AllBuffered, moveDirection.x);
        animator.SetFloat(speedParamToHash, Mathf.Abs(rigidBody2D.velocity.x));
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

    public void AddParentVelocity(float velocity_X){
        parentMove.x=velocity_X;
    }
}
