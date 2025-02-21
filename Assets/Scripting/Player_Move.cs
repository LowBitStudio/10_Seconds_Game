using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    //Component
    private Rigidbody2D rb;
    [HideInInspector] public Vector2 dir;
    //Physics
    public float MoveSpeed = 0;
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
    }

    void Update()
    {
        //Get the Input
        dir.x = Input.GetAxisRaw("Horizontal"); 
    }

    void FixedUpdate()
    {
        //Animation activation
        if(IsGround() && rb.velocity.magnitude >= 0.1)
        {
            Anim.Play("Walk");
        }
        if(IsGround() && rb.velocity.magnitude == 0)
        {
            Anim.Play("Idle");
        }
        if(!IsGround())
        {
            Anim.Play("Jump");
        }

        //Running Physics
        Vector2 Movement = new Vector2(dir.x * MoveSpeed, rb.velocity.y);
        rb.velocity = Movement;
        //Gravity Physics
        rb.AddForce(Vector2.down * GravityForce, ForceMode2D.Force);
        //Jump Movement
        if(Input.GetKey(KeyCode.Space) && IsGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
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

    void Flipping()
    {
        isFacingLeft = !isFacingLeft;

        if(!isFacingLeft)
        {
            transform.localScale = new Vector3(1, 1, 1);
        } 
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
