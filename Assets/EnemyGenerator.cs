using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : Singleton<EnemyGenerator>
{
    public float minX;
    public float maxX;
    public List<GameObject> enemyPrefabs;
    public List<int> enemyShowLevel;
    public List<GameObject> currentAvailablePrefabs;
    public float generateEnemyTime = 1f;
    public  float generateEnemyTimer = 0f;
    
    public List<Transform> spawnPointsStart;
    public List<Transform> spawnPointsEnd;

    public List<int> spawnPointsEnableLevel;
    private List<int> enabledSpawnPoints = new List<int>(){0};

    public int  spawnTimeDecreaseStart = 1;

    public float spawnTimeDecreaseStep = 0.1f;

    public float minSpawnTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        generateEnemyTimer = generateEnemyTime;
        minX = -10;
        maxX = 10;
        currentAvailablePrefabs.Add(enemyPrefabs[0]);
        
    }
    
    public  void UpdateAvailablePrefabs(int level )
    {
        
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            if (enemyShowLevel[i] <= level && !currentAvailablePrefabs.Contains(enemyPrefabs[i]))
            {
                currentAvailablePrefabs.Add(enemyPrefabs[i]);
            }
        }

        for (int i = 0; i < spawnPointsEnableLevel.Count; i++)
        {
            if (spawnPointsEnableLevel[i] <= level && !enabledSpawnPoints.Contains(i))
            {
                enabledSpawnPoints.Add(i);
            }
        }
        

        if (level >= spawnTimeDecreaseStart)
        {
            generateEnemyTime -= spawnTimeDecreaseStep;
            generateEnemyTime = math.max(generateEnemyTime, minSpawnTime);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //random between two Vector
        
        
        
         generateEnemyTimer += Time.deltaTime;
         if (generateEnemyTimer >= generateEnemyTime)
         {

             var positonsID =  enabledSpawnPoints.PickItem();
             var start = spawnPointsStart[positonsID];
             var end = spawnPointsEnd[positonsID];
             
             Vector3 randomVector = Vector3.Lerp(start.position, end.position, Random.value);
            
            generateEnemyTimer = 0f;
            //random quaternion
            var Quaternio = Quaternion.Euler(0, 0, Random.Range(0, 360));
            var enemyOB = Instantiate(currentAvailablePrefabs.PickItem(), randomVector, Quaternio);

            var position = transform.position;
            position.x = Random.Range(minX, maxX);
            enemyOB.transform.position = position;
            enemyOB.GetComponent<Enemy>().Init();
        }
    }
}
