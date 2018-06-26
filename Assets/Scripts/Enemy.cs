using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [HideInInspector] public bool facingRight = true;

    public float moveForce;
    public float maxSpeed;
    public bool isMoving;
    public float leftBound;
    public float rightBound;
    public float h;

    private Rigidbody2D rb2d;
    private Animator anim;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // obtain the enemies Physics Collider and Animation Controller
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        h = facingRight ? 1f : -1f;
        if (isMoving)
        {
            if (h * rb2d.velocity.x < maxSpeed)
            { // ensure the speed is based on the direction
                rb2d.AddForce(Vector2.right * moveForce * h);
            }

            if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            { // check to see if character hasn't broken max speed
                rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x), rb2d.velocity.y); // preserve direction with Mathf.Sign()
            }

            if (transform.position.x >= rightBound && facingRight)
            {
                Flip();
            }

            if (transform.position.x <= leftBound && !facingRight)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}