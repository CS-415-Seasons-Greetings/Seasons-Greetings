using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterBoss : MonoBehaviour
{

    public GameObject target; //the enemy's target
    public float TargetCD;
    public float updateTime;
    public float moveSpeed;
    public float chargeTime;
    public float attackCD;
    public Vector3 targetOldPosition;
    public bool inAttackRange;

    public void Start()
    {
        moveSpeed = 2f;
        target = GameObject.FindGameObjectWithTag("Player");
        targetOldPosition = target.transform.position;
        TargetCD = 2;
    }
    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;
        
        if (inAttackRange)
        {
            //transform.position = Vector2.MoveTowards(transform.position, targetOldPosition, moveSpeed * Time.deltaTime);
            if(target.transform.position.x >= 179)
            {
                transform.position = Vector2.Lerp(transform.position, targetOldPosition, .01f);
            }

            if (chargeTime + attackCD < currentTime)
            {
                chargeTime = Time.realtimeSinceStartup; // put charge on cooldown
                //transform.position = Vector2.MoveTowards(transform.position, targetOldPosition, moveSpeed * Time.deltaTime);
                transform.position = Vector2.Lerp(transform.position, targetOldPosition, .01f );
            }
            if (TargetCD + updateTime < currentTime)
            {
                updateTime = Time.time;
                targetOldPosition = target.transform.position;
 

            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        inAttackRange = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        inAttackRange = false;
    }


}
