using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoreCell : MonoBehaviour
{
    public GameObject bloodPrefab;

    public float generateBloodTime = 1f;
     float generateBloodTimer = 0f;

    private void Update()
    {
        //generate blood every generateBloodTime
        generateBloodTimer += Time.deltaTime;
        if (generateBloodTimer >= generateBloodTime)
        {
            generateBloodTimer = 0f;
            var bloodOB = Instantiate(bloodPrefab, GetComponent<Cell>().input. transform.position, Quaternion.identity);
            bloodOB.GetComponent<Blood>().nextCell = GetComponent<Cell>();
            bloodOB.GetComponent<Blood>().GenerateNextPath(GetComponent<Cell>());
            
            
            GetComponent<Cell>().renderer.transform.localScale = Vector3.one * 0.2f;
            GetComponent<Cell>().renderer.transform.DOScale(Vector3.one * 0.25f, 0.15f).SetLoops(2, LoopType.Yoyo);
            //GetComponent<Cell>().renderer?.transform.DOPunchScale(Vector3.one*0.2f*1.2f, 0.2f);
        }
    }
}
