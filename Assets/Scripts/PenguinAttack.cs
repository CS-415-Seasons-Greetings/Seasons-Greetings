using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinAttack : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;

    public Transform groundCheck;
    public Transform snowballSpawn;
    public GameObject snowballPrefab;
    public float jumpMin;
    public float jumpMax;
    public bool grounded;
    public float jumpCD; // cooldown before the boss can take another action
    public float snowballCD;

    private Rigidbody2D rb2d;
    private Animator anim;
    private float snowballTime;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>(); // obtain the enemies Physics Collider and Animation Controller
        anim = GetComponent<Animator>();
        snowballTime = 0; // get initial time
    }
	
	// Update is called once per frame
	void Update () {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void FixedUpdate()
    {
        float currentTime = Time.realtimeSinceStartup;
        if (snowballTime + snowballCD < currentTime)
        {
            snowballTime = Time.realtimeSinceStartup;
            anim.Play("SnowballAtk"); // play the attack animation
            // make penguin shoot snowball
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("SnowballAtk"))
            {
                GameObject snowball = Instantiate(snowballPrefab, snowballSpawn.position, Quaternion.identity) as GameObject;
                snowball.GetComponent<Rigidbody2D>().velocity = new Vector2(1.5f, 0);
            }
            
        }
    }
}
