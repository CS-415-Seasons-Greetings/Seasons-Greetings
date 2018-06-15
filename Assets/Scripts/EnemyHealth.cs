using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health;
    public int maxHealth;

    void Start()
    {
        health = 1;    
    }

    // Update is called once per frame
    void Update () {
		if(gameObject.transform.position.y < -7)
        {
            Destroy(gameObject);
        }


	}

    public void TakeDamage(int amount)
    {
        health -= amount;
        if( health < 1)
        {
            Destroy(gameObject);
        }

    }
}
