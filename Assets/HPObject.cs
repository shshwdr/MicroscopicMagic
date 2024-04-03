using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum EffectType{poison,frozen}
public class HPObject : MonoBehaviour
{
    public GameObject damageNumber;
    public int maxHP = 10;
    protected int currentHP = 0;
    [HideInInspector]
    public bool isDead = false;

    private Dictionary<EffectType, int> effects = new Dictionary<EffectType, int>();
    
    public float poisonTime = 1;
    public float poisonTimer = 0;

    public float damageNumberPopupTime = 0.2f;
    public float damageNumberPopupTimer = 0;

    public GameObject poisionGO;
    public GameObject deathGO;
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
        //gameObject.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.2f);
        
        gameObject.transform.localScale = Vector3.one * 0.5f;
        gameObject.transform.DOScale(Vector3.one * 0.4f, 0.15f).SetLoops(2, LoopType.Yoyo);
        storeDamageNumber(damage);
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }
    protected virtual void Update()
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
        
        damageNumberPopupTimer += Time.deltaTime;
        if (damages.Count>0 &&  damageNumberPopupTimer >= damageNumberPopupTime)
        {
            showDamageNumber();
            damageNumberPopupTimer = 0;
        }
    }


     protected virtual void Die()
     {
         isDead = true;
         if (deathGO)
         {
             Instantiate(deathGO,transform.position,transform.rotation);
         }
         Destroy(gameObject);
         if (damages.Count > 0)
         {
             
             showDamageNumber();
         }
         
     }

     private List<int> damages = new  List<int>();
     void storeDamageNumber(int damage)
     {
         damages.Add(damage);
     }

     private float jumpHeight = 0.5f;
     void showDamageNumber()
     {
         var go = Instantiate(damageNumber,transform.position,quaternion.identity);
         go.transform.DOLocalJump(transform.position+new Vector3(Random.Range(-jumpHeight,jumpHeight), jumpHeight, 0), 1, 1, 0.4f);
         go.GetComponentInChildren<Text>().text = damages[0].ToString();
         Destroy(go,0.5f);
         damages.RemoveAt(0);
     }
}
