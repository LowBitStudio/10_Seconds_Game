using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
        //Disabled the input
        if(Timer_Countdown.instance.PlayerWins || Timer_Countdown.instance.PlayerLoses)
        {
            dir = Vector2.zero;
            Player_Variable.isJumping = false;
            Player_Variable.isRunning = false;
            Anim.Play("Idle");
            return;
        }

        //Get the Input
        dir.x = Input.GetAxisRaw("Horizontal");

        //Jump Control
        Player_Variable.isJumping = Input.GetKey(KeyCode.Space) && IsGround();
        
        if(Player_Variable.isJumping)
        {
            Audio_Effect.instance.PlayingSFX(Audio_Effect.instance.Sfx_player_clip[1]);
        }
        
        //Sprint control
        if(IsGround() && Player_Variable.CanRun)
        {
            Player_Variable.isRunning = Input.GetKey(KeyCode.LeftShift);
        }
        else
        {
            Player_Variable.isRunning = false;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Audio_Effect.instance.PlayingSFX(Audio_Effect.instance.Sfx_player_clip[0]);
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
        //This will make the control disabled
        //once the game is done
        if(Timer_Countdown.instance.PlayerWins)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, Time.deltaTime * 5), rb.velocity.y);
            return; //Nullified all the commands
        }

        //Running Physics
        float TargetSpeed = dir.x * CurrentSpeed;
        float HorizontalSpeed = IsGround() ? TargetSpeed : rb.velocity.x;
        
        //Apply the movement
        //rb.velocity = new Vector2(dir.x * CurrentSpeed, rb.velocity.y);
        rb.velocity = new Vector2(HorizontalSpeed, rb.velocity.y);
        
        //Jumping Physics
        if(Player_Variable.isJumping)
        {
            rb.velocity = new Vector2(HorizontalSpeed, JumpForce);
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
    }

    void Flipping()
    {
        isFacingLeft = !isFacingLeft;
        sr.flipX = isFacingLeft;
    }
}
