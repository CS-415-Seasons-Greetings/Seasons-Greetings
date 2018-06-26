using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public bool hasDied;
    public int curHealth; // current health
    public int startingHealth; //starting health
    public Image[] HealthImages;
    public Sprite[] HealthSprites;

    private int MaxHeartAmount = 6;


	// Use this for initialization
	void Start () {
        hasDied = false;
        startingHealth = 3;
        curHealth = startingHealth;
        CheckHealthAmount();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.y < -4 || curHealth < 1)
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
        curHealth -= amount;
        CheckHealthAmount();
    }

    public void CheckHealthAmount()
    {
        for (int i = 0; i < MaxHeartAmount; i++)
        {
            if(curHealth <= i)
            {
                HealthImages[i].enabled = false;
            }
            else
            {
                HealthImages[i].enabled = true;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && GetComponent<PlayerControl>().grounded && !GetComponent<PlayerControl>().isAttacking)
        {
            TakeDamage(1); // deal 1 damage to the player
            //collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1); // possibly deal contact damage to enemies
            //Debug.Log(GetComponent<Enemy>().knockedBack); // causes some errors
            // if (GetComponent<PlayerControl>().grounded
            Vector2 enemypos = collision.gameObject.GetComponent<Transform>().position; // obtain the enemy position
            if (enemypos.x < GetComponent<Transform>().position.x)
            { // knockback to the right
                GetComponent<Rigidbody2D>().AddForce(new Vector2(500, 50)); // add force to the player when hit
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-250, 50));
            }
            else
            { // knockback to the left
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 50)); // add force to the player when hit
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(250, 50));
            }
        }
        if (collision.gameObject.tag == "Enemy" && GetComponent<PlayerControl>().isAttacking)
        {
            Vector2 enemypos = collision.gameObject.GetComponent<Transform>().position; // obtain the enemy position
            if (enemypos.x < GetComponent<Transform>().position.x)
            { // knockback enemy to the left
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-250, 50));
            }
            else
            { // knockback enemy to right
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(250, 50));
            }
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }
        
        if (collision.gameObject.tag == "DeathTouch")
        {
            Die();
        }
    }
}
