using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AoeCellAbility : AttackCellAbility
{
    public float radius = 2;
    public GameObject aoeVisualization;

    public override void TriggerAbility(Blood blood)
    {
        base.TriggerAbility(blood);
        var enemyCount = 0;
        //do damage to circle with radius 
        var enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        var firstPassEnemis = new List<GameObject>();
        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= radius)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackValue(blood));
                enemyCount++;
                firstPassEnemis.Add(enemy);
            }
        }

        foreach (var e in firstPassEnemis)
        {
            enemies.Remove(e);
        }

        if (enemyCount > 0)
        {
            
            var visualization =  Instantiate(aoeVisualization);
            visualization.transform.position = transform.position;
            visualization.transform.localScale = Vector3.one * radius*2;
            
            
            
            int increaseCount = blood.getAbilityTypeCount(AssistAbilityType.increaseCount);

            if (increaseCount > 0)
            {
                splitAttack(increaseCount, enemies, blood);
            }
            
        }

        blood.clearAssistAbilities();
    }

    void splitAttack(int increaseCount, List<GameObject> enemies, Blood blood)
    {
        
        
        var selectedEnemies = new List<GameObject>();
        for (int j = 0; j < enemies.Count; j++)
        {
            var enemy = enemies[j];
                    
            var distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= radius*2)
            {
                selectedEnemies.Add(enemy);
            }
        }
        
        for (int i = 0; i < increaseCount; i++)
        {

            if (selectedEnemies.Count == 0)
            {
                break;
            }
            var pickedEnemy = selectedEnemies.PickItem();
            
            foreach (var enemy in selectedEnemies)
            {
                var distance = Vector3.Distance(enemy.transform.position, transform.position);
                if (distance <= radius)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(attackValue(blood));
                    selectedEnemies.Remove(enemy);
                }
            }
                
                
            var visualization =  Instantiate(aoeVisualization);
            visualization.transform.position = pickedEnemy.transform.position;
            visualization.transform.localScale = Vector3.one * radius*2;
        }
    }
    
}
