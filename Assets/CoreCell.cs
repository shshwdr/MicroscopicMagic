using System;
using System.Collections;
using System.Collections.Generic;
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
            bloodOB.GetComponent<Blood>().GenerateNextPath(GetComponent<Cell>());
        }
    }
}
