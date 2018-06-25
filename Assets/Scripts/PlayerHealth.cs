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
        //SceneManager.LoadScene("Level design");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reloads the current scene when the player dies
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && GetComponent<PlayerControl>().grounded && !GetComponent<PlayerControl>().isAttacking)
        {
            TakeDamage(1); // deal 1 damage to the player
            //collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1); // possibly deal contact damage to enemies
            //Debug.Log(GetComponent<Enemy>().knockedBack); // causes some errors
            // if (GetComponent<PlayerControl>().grounded
            Debug.Log("Knockback");
            Vector2 enemypos = collision.gameObject.GetComponent<Transform>().position; // obtain the enemy position
            if (enemypos.x < GetComponent<Transform>().position.x)
            { // knockback to the right
                GetComponent<Rigidbody2D>().AddForce(new Vector2(500, 50)); // add force to the player when hit
            }
            else
            { // knockback to the left
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 50)); // add force to the player when hit
            }
        }
        if (collision.gameObject.tag == "Enemy" && GetComponent<PlayerControl>().isAttacking)
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }
}
