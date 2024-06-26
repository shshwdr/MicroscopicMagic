using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CellType{ShootCell, AoeCell, IncreaseDamageCell, IncreaseCountCell,PoisonCell,RotateCell,None}


public class UpgradeSelectObject : Singleton<UpgradeSelectObject>
{
    public List<CellCell> cellCells;
    public List<Transform> uiOBParents;
    public Camera uiCamera;
    public GameObject selectUI;
    public List<string> Description => descriptions;
    private List<string> descriptions = new List<string>()
    {
        "Phage Cell\nAttack Cell\nShoot virus bullet to do damage to cells ",
        "Inflammatory Signaling Cell\nAttack Cell\nDo AOE damage on nearby area",
        "Enzyme Catalyst Cell\nSupport Cell\nIncrease damage of next attack cell",
        "Mitosis Cell\nSupport Cell\nIncrease attack count of next attack cells",
        "Immunosuppressive Cell\nSupport Cell\nNext attack cell inflict damage over time",
        "Flagellum Cell\nSupport Cell\nRotate next attack cell",
        "This is me, the core. Don't let White Blood Cell touch me"
    };
    private void Start()
    {
       // Show(0);
    }

    public bool isShowing()
    {
        return showing;
    }

    bool showing;
    private int level = 0;
    public void Show(int l)
    {
        showing = true;
        DraggingManager.Instance.stopDragging();
        if (l == 0)
        {
            TutorialManager.Instance.finishTutorial("afterFirstDrag");
            TutorialManager.Instance.startTutorial("levelup1");
        }else if (l == 1)
        {
            TutorialManager.Instance.finishTutorial("afterLevelup1");
            TutorialManager.Instance.startTutorial("levelup2");
        }
        else
        {
            
            TutorialManager.Instance.finishTutorial("afterLevelup2");
        }

        level = l;
        selectUI.SetActive(true);
        showOne(l,0);
        showOne(l,1);
        Time.timeScale = 0;
        Debug.Log("time scale is 0");
    }

    public void Hide(GameObject go)
    {
        showing = false;
        LevelManager.Instance.UpdateProgress();
        if (level == 0&&TutorialManager.Instance.hasFinishedTutorial("levelup1"))
        {
            TutorialManager.Instance.finishTutorial("levelup1");
            TutorialManager.Instance.startTutorial("afterLevelup1");
        }else if (level == 1 &&TutorialManager.Instance.hasFinishedTutorial("levelup2"))
        {
            TutorialManager.Instance.finishTutorial("levelup2");
            TutorialManager.Instance.startTutorial("afterLevelup2");
        }
        
        int layer = LayerMask.NameToLayer("Default");
        if ((go.layer == layer))
        {
            return;
        }
        
        go.transform.parent = transform.parent;
        selectUI.SetActive(false);
        
        
        if (layer == -1)
        {
            Debug.LogWarning($"Layer '{name}' not found.");
            return;
        }

        SetLayerRecursive(go, layer);
        foreach (var parent in uiOBParents)
        {
            if (parent.childCount > 0)
            {
                Destroy(parent.GetChild(0).gameObject);
            }
        }

        Time.timeScale = 1;
        Debug.Log("time scale is 1");
    }

    void showOne(int level,int i )
    {
        var rand = Random.Range(0, (int)CellType.None);
        if (this.level == 0)
        {
            if (i == 0)
            {
                rand = (int)CellType.ShootCell;
            }
            else
            {
                rand = (int)CellType.AoeCell;
            }
        }else if (this.level == 1)
        {
            
            if (i == 0)
            {
                rand = (int)CellType.IncreaseDamageCell;
            }
            else
            {
                rand = (int)CellType.IncreaseCountCell;
            }
        }
        var cellCell = cellCells[i];
        var trans = cellCell.cellTrans;
        cellCell.description.text = descriptions[rand];
        //convert UI position in trans.position to 2d world position
        var position = Camera.main.ScreenToWorldPoint(trans.position);
        
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(uiCamera, trans.position);

        // Convert screen point to world space
        Vector3 worldPoint = uiCamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, uiCamera.nearClipPlane));
        
        // Since we're interested in a 2D position, we ignore the z component
        position = new Vector2(worldPoint.x, worldPoint.y);
        //enum to string
        var randStr = System.Enum.GetName(typeof(CellType), rand);
        GameObject go = Instantiate(Resources.Load<GameObject>("Cells/"+randStr),uiOBParents[i]);
        //set layer of gameobject
        go.transform.localPosition = Vector2.zero;
        go.GetComponent<Cell>().cellAbility = rand;
        int layer = LayerMask.NameToLayer("UIOB");
        if (layer == -1)
        {
            Debug.LogWarning($"Layer '{name}' not found.");
            return;
        }

        SetLayerRecursive(go, layer);
        var tubeCount = Random.Range(0, 4);
        go.GetComponent<Cell>().updateTube(tubeCount);
    }
    
    private void SetLayerRecursive(GameObject obj, int newLayer)
    {
        // Set the layer of the current object
        obj.layer = newLayer;

        // Iterate through all children and call this method recursively
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursive(child.gameObject, newLayer);
        }
    }
}
