using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletCellAbility : AttackCellAbility
{
    public GameObject Bullet;

    public float attackRange = 5;

    // Start is called before the first frame update
    public override void TriggerAbility(Blood blood)
    {
        base.TriggerAbility(blood);
        //sort enemy by distance
        var enemies = new List<GameObject>();
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
//if enemy is out of screen,  ignore

            Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);

            // Check if the enemy's screen position is within the viewport
            if (IsPositionWithinViewport(screenPos))
            {
                Debug.Log($"{enemy.name} is in the screen.");
            }
            else
            {
                Debug.Log($"{enemy.name} is not in the screen.");
                continue;
            }
            enemies.Add(enemy);

            // var distance = Vector3.Distance(enemy.transform.position, transform.position);
            // if (distance < minDistance)
            // {
            //     minDistance = distance;
            //     closestEnemy = enemy;
            // }
        }
        enemies.Sort(((a, b) => Vector3.Distance(a.transform.position, transform.position).CompareTo(Vector3.Distance(b.transform.position, transform.position))));

        int shootCount = 1 + blood.getAbilityTypeCount(AssistAbilityType.increaseCount);

        for (int i = 0; i < math.min(shootCount, enemies.Count); i++)
        {
            
            var bullet = Instantiate(Bullet);
            bullet.transform.position = transform.position;
            var forward = enemies[i].transform.position - bullet.transform.position;
        
            bullet.GetComponent<Bullet>().Init(forward, attackValue(blood),blood);

            if (Vector3.Distance(enemies[i].transform.position, transform.position)>attackRange)
            {
                break;
            }
        }

        blood.clearAssistAbilities();
    }
    bool IsPositionWithinViewport(Vector3 screenPos)
    {
        // Converting screen position to viewport coordinates (range 0 to 1)
        float x = Mathf.Clamp01(screenPos.x / Screen.width);
        float y = Mathf.Clamp01(1 - screenPos.y / Screen.height); // Invert Y because Unity's origin is at the bottom left

        // Check if the point is within the viewport rectangle
        return (x >= 0f && x <= 1f) && (y >= 0f && y <= 1f);
    }
    
    
}
