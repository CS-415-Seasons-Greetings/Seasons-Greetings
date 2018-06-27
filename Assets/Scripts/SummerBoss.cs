using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerBoss : MonoBehaviour {
    [HideInInspector] public bool facingRight = true;

    public Transform groundCheck;
    public Transform pitchforkTip;
    public GameObject fireballPrefab;
    public float jumpMin;
    public float jumpMax;
    public bool grounded;
    public float jumpCD; // cooldown before the boss can take another action
    public float fireCD;

    private Rigidbody2D rb2d;
    private Animator anim;
    private float fireballTime;
    private float jumpTime;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // obtain the enemies Physics Collider and Animation Controller
        anim = GetComponent<SpriteRenderer>().GetComponent<Animator>();
    }

    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void FixedUpdate()
    {
        float currentTime = Time.realtimeSinceStartup;
        if (jumpTime + jumpCD < currentTime)
        {
            jumpTime = Time.realtimeSinceStartup; // put jump on cooldown
            if (grounded)
            {
                rb2d.AddForce(new Vector2(0f, Random.Range(jumpMin, jumpMax))); // add jump force to the sprite
            }
        }

        if (fireballTime + fireCD < currentTime)
        {
            fireballTime = Time.realtimeSinceStartup;
            anim.Play("Attack"); // play the attack animation
            // make boss shoot fireball here
            GameObject fireball = Instantiate(fireballPrefab, pitchforkTip.position, Quaternion.identity) as GameObject;
            fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.5f, 0);
        }
    }
}
