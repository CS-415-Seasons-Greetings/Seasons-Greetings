using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour {

    public static int finalScore;
    public static int currentScore;


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            currentScore++;
            Destroy(gameObject);
        }
    }
}

