using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Vector3 velocity;
    public float leftBound;
    public float rightBound;
    public float topBound;
    public float bottomBound;
    public int directionFlip;
    public double motionOff;
    private bool isMoving;
    private Rigidbody2D rb2d;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        isMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {


            transform.position += (velocity * Time.fixedDeltaTime * directionFlip);
            if (transform.position.x != motionOff)
            {
                if (transform.position.x >= rightBound)
                {
                    directionFlip = -1;
                }

                if (transform.position.x <= leftBound)
                {
                    directionFlip = 1;
                }
            }
            if (transform.position.y != motionOff)
            {
                if (transform.position.y >= topBound)
                {
                    directionFlip = -1;
                }

                if (transform.position.y <= bottomBound)
                {
                    directionFlip = 1;
                }
            }

        }
    }
}
