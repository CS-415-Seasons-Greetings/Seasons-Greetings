using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [HideInInspector] public bool facingRight = true;
    private bool dirRight = true;
    public float speed = 2.0f;
    public bool knockedBack;

    void Update()
    {

        if (dirRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }

        if (transform.position.x >= 9)
        {
            dirRight = false;
            Flip();
        }

        if (transform.position.x <= 3)
        {
            dirRight = true;
            Flip();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().AddForce(new Vector2(100000000, 100000000000));
        knockedBack = true;
    }
}