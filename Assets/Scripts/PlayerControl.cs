using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    /*[HideInInspector]*/
    public bool isAttacking = false;

    public float moveForce;
    public float maxSpeed;
    public float jumpForce;
    public Transform groundCheck;
    public AudioClip JumpSound;
    public AudioSource PlayerSource;
    public bool grounded = false; // jumping works when a ground collision can be detected
    public BoxCollider2D attackHitbox;
    public bool pause;

    private Animator anim;
    private Rigidbody2D rb2d;
    private Vector2 hitboxCoords = new Vector2(0.1f, 0.1f);
    private Vector2 hideHitBox = new Vector2(1000f, -1000f);
    private bool canDoubleJump; // determines if the player can double jump
    private bool doubleJump; // determines if the player has second jump available

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        if (SceneManager.GetActiveScene().name.Equals("Winter"))
        {
            canDoubleJump = true;
            doubleJump = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ground detection works when the ground tranform object attached to the player is stuck in an object in the ground layer
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (grounded)
        {
            doubleJump = true;
        }

        if (Input.GetKeyDown("space") && grounded)
        {
            jump = true; // jump if the jump button is pressed and the character isn't grounded
        }
        if (Input.GetKeyDown("space") && canDoubleJump && doubleJump && !grounded)
        {
            jump = true; // allow for double jump
            doubleJump = false;
        }

        if (Input.GetKey("p")) //Pause
        {
            if (pause == false)
            {
                Time.timeScale = 0.0f;
                pause = true;
            }
            else
            {
                if (pause == true)
                {
                    Time.timeScale = 1.0f;
                    pause = false;
                }
            }
        }
        if (Input.GetKey("t"))//Slow motion
        {
            Time.timeScale = 0.3f;
        }
        if (Input.GetKey("r"))//Normal Speed
        {
            Time.timeScale = 1.0f;
        }
        if(Input.GetKey("y"))
        {
            Time.timeScale = 2.0f;
        }
    } 

    /**
     * Controls Physics Updates
     * 
     */
    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        //anim.SetFloat("Speed", Mathf.Abs(h));

        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("Player-Attack"))
        {
            if (attackHitbox.offset != hideHitBox)
            {
                attackHitbox.offset = hideHitBox; // move attack hitbox away
                isAttacking = false;
            }
            if (Input.GetButton("Horizontal"))
            {
                //anim.ResetTrigger("Player-idle"); // play walk cycle
                anim.Play("Player-Walk-Cycle");
            }
            else if (!Input.GetKey("x") || !Input.GetKey("j"))
            {
                //anim.ResetTrigger("Player-Walk-Cycle"); // play idle animation
                anim.Play("Player-idle");
            }
            if (Input.GetKey("x") || Input.GetKey("j"))
            {
                anim.Play("Player-Attack"); // play attack animation and throw out hitbox
                isAttacking = true;
                attackHitbox.offset = hitboxCoords;

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
            //anim.SetTrigger("Jump");
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
