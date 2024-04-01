using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPObject : MonoBehaviour
{
    public int maxHP = 10;
    protected int currentHP = 0;
    [HideInInspector]
    public bool isDead = false;

     protected virtual void   Start()
     {
         currentHP = maxHP;
     }

     protected virtual void Die()
     {
         isDead = true;
         Destroy(gameObject);
     }
}
