using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviourPun
{
    PlayerInputController playerInputController;
    Rigidbody2D rigidBody2D;
    Animator animator;
    SpriteRenderer spriteRenderer;
    int jumpParamToHash = Animator.StringToHash("Jump");
    int speedParamToHash = Animator.StringToHash("Speed");
    private Vector2 MoveDirection;
    public float gravityscale;
    public float velocty_y;
    [SerializeField] private float jumpPower;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    private void Awake() {
        playerInputController = GetComponent<PlayerInputController>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        playerInputController.OnMoveEvent+= OnMove;
        playerInputController.OnJumpEvent+=JumpMoveMent;
    }
    void FixedUpdate(){
        MoveMent();
        velocty_y = (float)Math.Round(rigidBody2D.velocity.y, 2);
    }
    private void JumpMoveMent()
    {
        if (!photonView.IsMine) return;

        if (Math.Round(rigidBody2D.velocity.y,2)==0){
            Debug.Log("호출" + Vector2.up * jumpPower);
            rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetTrigger(jumpParamToHash);
        }
    }
    private void OnMove(Vector2 direction){
        if (!photonView.IsMine) return;

        MoveDirection = direction* speed;
    }
    private void MoveMent()
    {
        MoveDirection.y = rigidBody2D.velocity.y- gravityscale;
        rigidBody2D.velocity = MoveDirection;

        spriteRenderer.flipX = MoveDirection.x<= 0?true:false;
        animator.SetFloat(speedParamToHash, Mathf.Abs(rigidBody2D.velocity.x));
    }
}
