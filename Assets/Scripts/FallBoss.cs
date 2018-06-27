using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBoss : MonoBehaviour
{

    [HideInInspector] public bool facingRight = true;

    //public float moveForce;
    //public float maxSpeed;
    //public bool isMoving;
    //public float leftBound;
    //public float rightBound;
    //public float h;
    public float distance;
    public float speed;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool movingRight = true;

    public Transform groundDetection;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // obtain the enemies Physics Collider and Animation Controller
        anim = GetComponent<Animator>();
        distance = 1;
        speed = 1;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfoRight = Physics2D.Raycast(groundDetection.position, new Vector2(1, -1), distance);
        RaycastHit2D groundInfoLeft = Physics2D.Raycast(groundDetection.position, new Vector2(-1, -1), distance);
        if (groundInfoRight.collider == false || groundInfoLeft.collider == false || groundDetection.position.x > 160 || groundDetection.position.x < 149) 
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);

                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}
