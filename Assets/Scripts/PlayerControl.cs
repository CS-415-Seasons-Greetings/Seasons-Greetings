﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;

    public float moveForce = 150f;
    public float maxSpeed = 2f;
    public float jumpForce = 120;
    public Transform groundCheck;
    public AudioClip JumpSound;
    public AudioSource PlayerSource;
    public bool grounded = false; // jumping works when a ground collision can be detected

    private Animator anim;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ground detection works when the ground tranform object attached to the player is stuck in an object in the ground layer
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetKeyDown("space") && grounded == true) 
        {
            jump = true; // jump if the jump button is pressed and the character isn't grounded
        }
    }

    /**
     * Controls Physics Updates
     * 
     */
    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(h));

        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("Player-Attack"))
        {
            if (Input.GetButton("Horizontal"))
            {
                anim.ResetTrigger("Player-idle");
                anim.Play("Player-Walk-Cycle");
            }
            else if (!Input.GetKey("x") || !Input.GetKey("j"))
            {
                anim.ResetTrigger("Player-Walk-Cycle");
                anim.Play("Player-idle");
            }
            if (Input.GetKey("x") || Input.GetKey("j"))
            {
                anim.Play("Player-Attack");
            }
        }

        if (h * rb2d.velocity.x < maxSpeed)
        { // ensure the speed is based on the direction
            rb2d.AddForce(Vector2.right * h * moveForce);
        }

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
        { // check to see if character hasn't broken max speed
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y); // preserve direction with Mathf.Sign()
        }

        if (h > 0 && !facingRight)
        {
            Flip();
        }
        else if (h < 0 && facingRight)
        {
            Flip();
        }
        if (jump)
        {
            PlayJumpSound();
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce)); // add jump force to the sprite
            jump = false;
        }
    }

    void PlayJumpSound()
    {
        PlayerSource.clip = JumpSound;
        PlayerSource.Play();
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
