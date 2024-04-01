using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FadeAndDestroy : MonoBehaviour
{
    public  float fadeTime = 1f;
    public float fadeTimer = 0f;

    private void Start()
    {
        var render = GetComponent<SpriteRenderer>();
        var targetColor = render.color;
        targetColor.a = 0;
        DOTween.To(()=> render.color, x=> render.color = x, targetColor, fadeTime);

       
    }

    private void Update()
    {
         fadeTimer += Time.deltaTime;
         
         if (fadeTimer >= fadeTime)
        {
            Destroy(gameObject);
        }
    }
}
