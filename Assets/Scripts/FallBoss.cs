using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBoss : MonoBehaviour
{
    public Transform MiniPumpkinSpawnRain;
    public GameObject MiniPumpkinPrefab;
    public Transform MiniPumpkinSpawnClose;
    public float SpawnCDClose;
    public float SpawnCDRain;
    public GameObject target; //the enemy's target
    public float moveSpeed = 1f; //move speed
    private bool facingRight = true;
    private Rigidbody2D rb2d;
    private float SpawnTime;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // obtain the enemies Physics Collider and Animation Controller
        target = GameObject.FindGameObjectWithTag("Player");
        SpawnCDClose = 3;
        SpawnCDRain = 16;

    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        float currentTime = Time.time;
        if (SpawnTime + SpawnCDClose < currentTime)
        {
            SpawnTime = Time.time;
            GameObject MiniPumpkinClose = Instantiate(MiniPumpkinPrefab, MiniPumpkinSpawnClose.position, Quaternion.identity) as GameObject;
            MiniPumpkinClose.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 0);
        }
        if (SpawnTime + SpawnCDRain < currentTime)
        {
            SpawnTime = Time.time;
            GameObject MiniPumpkinRain = Instantiate(MiniPumpkinPrefab, MiniPumpkinSpawnRain.position, Quaternion.identity) as GameObject;
            MiniPumpkinRain.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 0);
        }
    }


}
