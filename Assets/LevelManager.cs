using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
     List<int> levelupCount = new List<int>() { 1,/*1,1,1,*/5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

    public int currentLevel = 0;
    public int currentKillCount = 0;
    
    public void KillEnemy(int amount = 1)
    {
        currentKillCount += amount;
        if (currentKillCount >= levelupCount[currentLevel])
        {
            currentLevel++;
            currentKillCount = 0;
            Levelup();
        }

    }

    void Levelup()
    {
        Debug.Log("Level up to " + currentLevel);
        GameObject.FindObjectOfType<UpgradeSelectObject>().Show(currentLevel);
    }
}
