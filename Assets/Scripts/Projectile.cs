using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float lifetime; // how long the fireball will exist until its destroyed

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 0.001f); // destroy this once a collision occurs
    }
}
