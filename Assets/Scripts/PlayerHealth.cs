using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public bool hasDied;
    public int health;

	// Use this for initialization
	void Start () {
        hasDied = false;
        health = 3;
	}
	
	// Update is called once per frame
	void Update () {

        if (gameObject.transform.position.y < -4)
        {
            Die();
        }
        if(health < 1)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }

}
