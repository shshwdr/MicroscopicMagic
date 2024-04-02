using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType{poison,frozen}
public class HPObject : MonoBehaviour
{
    public int maxHP = 10;
    protected int currentHP = 0;
    [HideInInspector]
    public bool isDead = false;

    private Dictionary<EffectType, int> effects = new Dictionary<EffectType, int>();
    
    public float poisonTime = 1;
    public float poisonTimer = 0;

    public GameObject poisionGO;
    public void ApplyEffect(EffectType type, int charge)
    {
        if (effects.ContainsKey(type))
        {
            effects[type] += charge;
        }
        else
        {
            effects.Add(type, charge);
        }

        switch (type)
        {
             case EffectType.poison:
                 poisionGO.SetActive(true);
                 break;
        }
        
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }
    private void Update()
    {
        //poison
        if (effects.ContainsKey(EffectType.poison))
        {
            poisonTimer += Time.deltaTime;
            if (poisonTimer >= poisonTime)
            {
                TakeDamage(effects[EffectType.poison]);
                poisonTimer = 0;
            }
        }
    }

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
