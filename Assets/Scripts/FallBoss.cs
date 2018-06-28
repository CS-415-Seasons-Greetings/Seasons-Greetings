using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBoss : MonoBehaviour
{
    public Transform FlowerSpawn;
    public GameObject FlowerDudePrefab;
    public float SpawnCD;

    private Rigidbody2D rb2d;
    private float SpawnTime;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // obtain the enemies Physics Collider and Animation Controller
    }

    void Update()
    {
        
       
    }

    private void FixedUpdate()
    {
        float currentTime = Time.realtimeSinceStartup;
        if (SpawnTime + SpawnCD < currentTime)
        {
            SpawnTime = Time.realtimeSinceStartup;
            GameObject FlowerDude = Instantiate(FlowerDudePrefab, FlowerSpawn.position, Quaternion.identity) as GameObject;
            FlowerDude.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.5f, 0);
        }
    }

}
