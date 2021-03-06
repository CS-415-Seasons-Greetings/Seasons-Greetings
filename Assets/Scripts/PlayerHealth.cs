﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public bool hasDied;
    static int curHealth; // current health
    public int startingHealth; //starting health
    public float iframeDuration = 2.0f;
    public Image[] HealthImages;
    public Sprite[] HealthSprites;

    public float timeHurt; // time player got hurt

    // Use this for initialization
    void Start() {
        hasDied = false;
        startingHealth = 3;
        curHealth = startingHealth;
        CheckHealthAmount();
    // Updated upstream
        timeHurt = Time.realtimeSinceStartup;
	}
	
    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -15)
        {
            Collectibles.currentScore = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Die()
    {
        hasDied = true;
        Collectibles.currentScore = 0;
        SceneManager.LoadScene("Death Screen");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reloads the current scene when the player dies
    }

    public void TakeDamage(int amount)
    {
        curHealth -= amount;
        if (curHealth <= 0)
        {
            Die();
        }
        else
        {
            CheckHealthAmount();
        }
    }

    public void AddHealth(int amount)
    {
        curHealth += amount;
    }

    public void CheckHealthAmount()
    {
        for (int i = 0; i < startingHealth; i++)
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

    /**
     * Handle All instances where the player will take or deal damage
     * 
     */
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
                && GetComponent<PlayerControl>().grounded && !GetComponent<PlayerControl>().isAttacking && Time.realtimeSinceStartup > timeHurt + iframeDuration)
        {
            TakeDamage(1); // deal 1 damage to the player
            timeHurt = Time.realtimeSinceStartup; // engage iframes
            Vector2 enemypos = collision.gameObject.GetComponent<Transform>().position; // obtain the enemy position
            if (enemypos.x < GetComponent<Transform>().position.x)
            { // knockback to the right
                GetComponent<Rigidbody2D>().AddForce(new Vector2(500, 50)); // add force to the player when hit

                if (collision.gameObject.tag == "Enemy") // no knockback on bosses
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-250, 50));
                }
            }
            else
            { // knockback to the left
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 50)); // add force to the player when hit

                if (collision.gameObject.tag == "Enemy") // no knockback on bosses
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(250, 50));
                }
            }
        }
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss") && GetComponent<PlayerControl>().isAttacking)
        {
            Vector2 enemypos = collision.gameObject.GetComponent<Transform>().position; // obtain the enemy position
            if (collision.gameObject.tag == "Enemy")
            {
                if (enemypos.x < GetComponent<Transform>().position.x)
                { // knockback enemy to the left
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-250, 50));
                }
                else
                { // knockback enemy to right
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(250, 50));
                }
            }
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }

        if (collision.gameObject.tag == "DeathTouch")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //Die();
        }

        if (collision.gameObject.tag == "Spikes" && Time.realtimeSinceStartup > timeHurt + iframeDuration)
        {
            TakeDamage(1); // take damage from spikes and engage iframes
            timeHurt = Time.realtimeSinceStartup; // engage iframes
        }

        
        if (collision.gameObject.layer.ToString() == "Projectile")
        {
            Destroy(collision.gameObject); // destroy the object if its a fireball
        }
        
    }
}
