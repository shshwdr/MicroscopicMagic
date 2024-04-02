using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HPObject
{
    public float moveSpeed;
   

    protected override void Die()
    {
        if (!isDead)
        {
            
            base.Die();
            GameObject.FindObjectOfType<LevelManager>().KillEnemy(1);
        }
    }

    public void Init()
    {
    }

    private void Update()
    {
         if (!isDead)
         {
             var targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
             transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
         }
    }
}
