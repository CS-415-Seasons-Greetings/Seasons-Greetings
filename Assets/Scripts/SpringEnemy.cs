using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringEnemy : MonoBehaviour
{

    public GameObject target; //the enemy's target
    public float moveSpeed = 0.01f; //move speed
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {

        /* Vector3 targetDir = target.transform.position - transform.position;
         float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
         Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
         transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180);
         transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
         */
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);

    }
}