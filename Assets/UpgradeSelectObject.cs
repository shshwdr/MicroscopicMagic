using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CellType{ShootCell, AoeCell, IncreaseDamageCell, IncreaseCountCell,PoisonCell,RotateCell,None}


public class UpgradeSelectObject : MonoBehaviour
{
    public List<CellCell> cellCells;
    public List<Transform> uiOBParents;
    public Camera uiCamera;
    public GameObject selectUI;

    List<string> descriptions = new List<string>()
    {
        "Shoot",
        "Aoe",
        "Increase Damage",
        "Increase Count",
        "Poison",
        "Rotate"
    };
    private void Start()
    {
       // Show(0);
    }

    public void Show(int level)
    {
        selectUI.SetActive(true);
        showOne(0);
        showOne(1);
        Time.timeScale = 0;
        Debug.Log("time scale is 0");
    }

    public void Hide(GameObject go)
    {
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

    void showOne(int i )
    {
        var rand = Random.Range(0, (int)CellType.None);
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
