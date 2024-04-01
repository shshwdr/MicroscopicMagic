using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public float minX;
    public float maxX;
    public GameObject enemyPrefab;
    public float generateEnemyTime = 1f;
    public  float generateEnemyTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        minX = -10;
        maxX = 10;
    }

    // Update is called once per frame
    void Update()
    {
         generateEnemyTimer += Time.deltaTime;
         if (generateEnemyTimer >= generateEnemyTime)
        {
            generateEnemyTimer = 0f;
            var enemyOB = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            var position = transform.position;
            position.x = Random.Range(minX, maxX);
            enemyOB.transform.position = position;
            enemyOB.GetComponent<Enemy>().Init();
        }
    }
}
