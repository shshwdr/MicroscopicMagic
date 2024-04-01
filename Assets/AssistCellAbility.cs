using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Flags]
public enum AssistAbilityType{increaseCount,increaseDamage, poison, freeze}
public class AssistCellAbility : CellAbility
{
    public AssistAbilityType assistAbility;
    public override void TriggerAbility(Blood blood)
    {
        blood.addAssistAbility(assistAbility);
    }
}
