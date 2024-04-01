using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HPObject
{
    public float moveSpeed;
    public void TakeDamage(int damage)
    {
         currentHP -= damage;
         if (currentHP <= 0)
         {
             Die();
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
