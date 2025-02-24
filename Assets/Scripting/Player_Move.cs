using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour
{
    //Component
    private Rigidbody2D rb;
    [HideInInspector] public Vector2 dir;
    [SerializeField] SpriteRenderer sr;
    
    //Physics
    public float MoveSpeed = 0;
    private float CurrentSpeed;
    public float JumpForce = 0;
    public float GravityForce = 0;
    
    //Ground checking
    [SerializeField] private LayerMask WhatisGround;
    [SerializeField] private Vector2 BoxSize;
    [SerializeField] private float CastDistance;
    private bool IsGround()
    {
        return Physics2D.BoxCast(transform.position, BoxSize, 0, -transform.up, CastDistance, WhatisGround);
    }
    
    //Sprinting Var
    public float SprintSpeed = 14;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * CastDistance, BoxSize);
    }
    
    //For Flipping
    private bool isFacingLeft = false;
    
    //Animation
    [SerializeField] private Animator Anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentSpeed = MoveSpeed; 
    }

    void Update()
    {
        //Get the Input
        dir.x = Input.GetAxisRaw("Horizontal");

        //Jump Control
        Player_Variable.isJumping = Input.GetKey(KeyCode.Space) && IsGround();
        
        //Sprint control
        if(IsGround() && Player_Variable.CanRun)
        {
            Player_Variable.isRunning = Input.GetKey(KeyCode.LeftShift);
        }
        else
        {
            Player_Variable.isRunning = false;
        }

        //Animation activation
        //Jumping
        if(!IsGround())
        {
            Anim.Play("Jump");
        }
        //Sprinting
        else if (Player_Variable.isRunning && dir.x != 0)
        {
            Anim.Play("Sprinting");
        }
        //Walk
        else if(dir.x != 0)
        {
            Anim.Play("Walk");
        }
        //Idle
        else
        {
            Anim.Play("Idle");
        }
        
        //Flipping
        if(!isFacingLeft && dir.x < 0)
        {
            Flipping();
        } 
        else if(isFacingLeft && dir.x > 0)
        {
            Flipping();
        }
    }

    void FixedUpdate()
    {
        //Running Physics
        float TargetSpeed = dir.x * CurrentSpeed;
        float HorizontalSpeed = IsGround() ? TargetSpeed : rb.velocity.x;
        
        //Apply the movement
        rb.velocity = new Vector2(HorizontalSpeed, rb.velocity.y);
        
        //Jumping Physics
        if(Player_Variable.isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            Player_Variable.isJumping = false;
        }
        //Cut the jump
        if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y >= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        //Gravity Physics
        rb.AddForce(Vector2.down * GravityForce, ForceMode2D.Force);
        
        //Sprinting Physics
        CurrentSpeed = Player_Variable.isRunning ? SprintSpeed : MoveSpeed;
        
        Debug.Log($"Current speed is {CurrentSpeed}");
        Debug.Log($"Can Run: {Player_Variable.CanRun}, is Running: {Player_Variable.isRunning}");
    }

    void Flipping()
    {
        isFacingLeft = !isFacingLeft;
        sr.flipX = isFacingLeft;
    }
}
