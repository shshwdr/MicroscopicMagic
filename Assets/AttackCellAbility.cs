using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class AttackCellAbility : CellAbility
{
    
    public int attack = 10;
    public float currentRotate = 0;
    private int direction = 1;
    public float rotateTime = 10;
    public float rotateDegree = 30;
    
    protected int attackValue(Blood blood)
    {
        return attack * (1 + blood.getAbilityTypeCount(AssistAbilityType.increaseDamage));
    }

    public override void TriggerAbility(Blood blood)
    {
        //if blood has rotate, try rotate
        if (blood.getAbilityTypeCount(AssistAbilityType.rotate) > 0)
        {
            currentRotate += direction;
            if (currentRotate == 2)
            {
                direction = -1;
            }

            if (currentRotate == -2)
            {
                direction = 1;
            }
            transform.DORotate(new Vector3(0, 0, rotateDegree*currentRotate), rotateTime);
        }
    }
}
