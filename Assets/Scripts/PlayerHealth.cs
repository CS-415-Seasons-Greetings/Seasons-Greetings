using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public bool hasDied;
    public int health; // health is set in the editor

	// Use this for initialization
	void Start () {
        hasDied = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (gameObject.transform.position.y < -4 || health < 1)
        {
            Die();
        }
	}

    void Die()
    {
        hasDied = true;
        SceneManager.LoadScene("Level design");
        
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
            //collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            //Debug.Log(GetComponent<Enemy>().knockedBack);
           // if (GetComponent<PlayerControl>().grounded)
            //{
                Debug.Log("Knockback");
            GetComponent<Rigidbody2D>().AddForce(new Vector2(500, 50));
            
            //}
        }
    }

}
