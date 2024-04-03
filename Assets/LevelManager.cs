using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    
    public List<int> levelupCount = new List<int>() { 1,/*1,1,1,*/5, 10,10, 15,15, 20, 20, 30, 40, };

    public int currentLevel = 0;
    public int currentKillCount = 0;

    public void UpdateProgress()
    {
        
        InfoManager.Instance.UpdateProgress((float)currentKillCount / levelupCount[currentLevel]);
    }
    public void KillEnemy(int amount = 1)
    {
        currentKillCount += amount;
        GameManager.Instance.killedAmount++;
        UpdateProgress();
        if (currentKillCount >= levelupCount[currentLevel])
        {
            Levelup();
            if (currentLevel < levelupCount.Count - 1)
            {
                currentLevel++;
            }
            currentKillCount = 0;
            EnemyGenerator.Instance.UpdateAvailablePrefabs(currentLevel);
        }

    }

    void Levelup()
    {
        Debug.Log("Level up to " + currentLevel);
        GameObject.FindObjectOfType<UpgradeSelectObject>().Show(currentLevel);
    }
}
