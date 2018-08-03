using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBossSpawns : MonoBehaviour {

    public GameObject target; //the enemy's target
    public GameObject explosionPrefab;
    public float TargetCD;
    public float updateTime;
    public float moveSpeed = 1f;
    public bool collided;
    public float collisionRadius;
    public Vector3 targetOldPosition;
    public LayerMask groundLayer;
    private Rigidbody2D rb2d;

    

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // obtain the enemies Physics Collider and Animation Controller
        target = GameObject.FindGameObjectWithTag("Player");
        targetOldPosition = target.transform.position;
        collided = false;
        collisionRadius = 1f;

    }

    void Update()
    {
        collided = Physics2D.OverlapCircle(transform.position, collisionRadius, groundLayer);
        float currentTime = Time.time;
        transform.position = Vector2.MoveTowards(transform.position, targetOldPosition, moveSpeed * Time.deltaTime);
        if(transform.position.Equals(targetOldPosition) || collided || transform.position.x < 110)
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
            Destroy(explosion, 2.2f);
        }
        if (TargetCD + updateTime < currentTime)
        {
            updateTime = Time.time;
            targetOldPosition = target.transform.position;

        }
    }

 
}
