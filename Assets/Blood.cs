using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    private HashSet<Cell> cells = new HashSet<Cell>();
    public Cell nextCell;
    public float moveSpeed = 1f;
    float proximityThreshold = 0.01f; // Adjust this value according to your needs
    [HideInInspector] public List<AssistAbilityType> assistAbilities = new List<AssistAbilityType>();

    public int getAbilityTypeCount(AssistAbilityType type)
    {
        int res = 0;
        foreach (var ability in assistAbilities)
        {
            if (ability == type)
            {
                res++;
            }
        }

        return res;
    }

    public void clearAssistAbilities()
    {
        assistAbilities.Clear();
    }
    public void GenerateNextPath(Cell cell)
    {
        foreach (var tube in cell.tubes)
        {
            if (tube.output.transform.childCount > 0)
            {
                var outputCell = tube.output.transform.GetChild(0).GetComponent<Cell>();
                if (!cells.Contains(outputCell))
                {
                    cells.Add(outputCell);
                    nextCell = outputCell;
                    return;
                }

            }
        }
        
        if (cell.GetComponent<CoreCell>())
        {
            Destroy(gameObject);
            return;
        }

        if (cell.transform.parent != null)
        {
            var parentOutput = cell.transform.parent;
            nextCell = parentOutput.parent.parent.GetComponent<Cell>();
            if (nextCell == null)
            {
                Destroy((gameObject));
            }
        }
    }


    public void addAssistAbility(AssistAbilityType ability)
    {
        assistAbilities.Add(ability);
    }
    private void Update()
    {
        //if next cell has value, move to next cell
        if (nextCell != null)
        {

            var targetPosition = nextCell.input.transform.position;
            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, targetPosition) <= proximityThreshold)
            {
                // The current transform is close enough to the next cell's position
            //}
            //if (transform.position == nextCell.transform.position)
            //{
            nextCell.GetComponent<CellAbility>()?.TriggerAbility(this);
                GenerateNextPath(nextCell);
            }
        }
    }
}
