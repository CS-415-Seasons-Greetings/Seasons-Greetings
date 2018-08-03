using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{

    public int displayScore;
    Text scoreText;

    // Use this for initialization
    void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        displayScore = Collectibles.finalScore + Collectibles.currentScore;
        scoreText.text = "Collectibles: " + displayScore + "/44";
    }
}
