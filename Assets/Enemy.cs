using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : HPObject
{
    public float moveSpeed;
    public GameObject renderer;

    void Start()
    {
        renderer.transform.DOScale(new Vector3(0.15f, 0.2f, 1), 1f).SetLoops(-1,LoopType.Yoyo);
        
        currentHP = maxHP;
    }
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

   protected override void Update()
    {
        base.Update();
         if (!isDead)
         {
             var targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
             transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
         }
    }

   void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("game over");
            GameManager.Instance.GameOver();
        }
    }
}
