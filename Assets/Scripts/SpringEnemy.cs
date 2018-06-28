using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringEnemy : MonoBehaviour
{
    [HideInInspector] private bool facingRight = true;

    public GameObject target; //the enemy's target
    public float moveSpeed = 1f; //move speed
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        if(target.transform.position.x < transform.position.x)
        {
            if(facingRight)
            {
                Flip();
            }
          
        }
        if (target.transform.position.x > transform.position.x)
        {
            if (!facingRight)
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