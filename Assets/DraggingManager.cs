using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggingManager : Singleton<DraggingManager>
{
    public GameObject draggingItem;
    public GameObject upgradeUI;

    public void stopDragging()
    {
        draggingItem = null;
    }
    void Update()
    {
        if (GameManager.Instance.isGameOver ==1)
        {
            return;
        }
        // if (UpgradeSelectObject.Instance.isShowing())
        // {
        //     return;
        // }
        //ignore UI
        // if (EventSystem.current.IsPointerOverGameObject())
        // {
        //     return;
        // }
        if (Input.GetMouseButtonDown(0))
        {
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // RaycastHit hit;
            // if (Physics.Raycast(ray, out hit))
            // {
            //     if (hit.collider.gameObject.tag == "Cell")
            //     {
            //         draggingItem = hit.collider.gameObject;
            //     }
            // }
            
            // Get the mouse position in screen space
            Vector3 mousePosition = Input.mousePosition;

// Create a 2D Ray from the main camera through the mouse position
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);

// Check if the raycast hits a collider tagged as "Cell"
            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Cell"))
                {
                    draggingItem = hit.collider.gameObject;
                    draggingItem.transform.parent = transform.parent;
                    
                    int layer = LayerMask.NameToLayer("UIOB");
                    if (hit.collider.gameObject.layer == layer)
                    {
                        
                        GameObject.FindObjectOfType<UpgradeSelectObject>().Hide(draggingItem);
                    }
                }
            }

            
        }

        if (Input.GetMouseButtonUp(0) && draggingItem!=null)
        {

            var selectedAttachment = placeToGameObject();
            var color = Color.white;
            color.a = 0;
            draggingItem.GetComponentInChildren<SpriteRenderer>().color = color;
            if (selectedAttachment != null)
            {
                draggingItem.GetComponent<Cell>().input.GetComponent<Attachment>().AttachedTo( selectedAttachment.transform,draggingItem.GetComponent<Cell>().transform);
                
                draggingItem = null;
                
                
                TutorialManager.Instance.finishTutorial("drag");
                TutorialManager.Instance.startTutorial("afterFirstDrag");
            }
            else
            {
                
                //draggingItem = null;
            }

            return;
        }

        // if (Input.GetMouseButton(0) && draggingItem != null)
        // {
        //     draggingItem.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // }
        if (Input.GetMouseButton(0) && draggingItem != null)
        {
            var position  = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            draggingItem.transform.position = position;
            
            //render different color
            var color = placeToGameObject() != null ? Color.green : Color.red;
            color.a = 0.5f;
            draggingItem.GetComponentInChildren<SpriteRenderer>().color = color;
               ;
        }

        if (draggingItem == null)
        {
            bool show = false;
            Vector3 mousePosition = Input.mousePosition;

// Create a 2D Ray from the main camera through the mouse position
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero);

// Check if the raycast hits a collider tagged as "Cell"
            if (hit)
            {
                if (hit.collider.GetComponent<Cell>()!=null)
                {
                    // draggingItem = hit.collider.gameObject;
                    // draggingItem.transform.parent = transform.parent;
                    // GameObject.FindObjectOfType<UpgradeSelectObject>().Hide(draggingItem);
                    //
                    show = true;
                    InfoManager.Instance.Show(hit.collider.GetComponent<Cell>().cellAbility);
                }
            }

            if (!show)
            {
                InfoManager.Instance.Hide();
            }
        }
    }

    GameObject placeToGameObject()
    {
        var distanceToAttach = 0.5f;
        GameObject selectedAttachment = null;
        var minDistance = 10000f;
        foreach (var outputAttachment in GameObject.FindGameObjectsWithTag("OutputAttachment"))
        {
            if (outputAttachment.transform.parent.parent == draggingItem.transform)
            {
                continue;
            }

            if (outputAttachment.transform.childCount > 0)
            {
                continue;
            }
            var distance = Vector2.Distance(outputAttachment.transform.position,
                draggingItem.GetComponent<Cell>().input.transform.position);
            if (distance < distanceToAttach && distance < minDistance)
            {
                selectedAttachment = outputAttachment;
                minDistance = distance;
            }
        }

        return selectedAttachment;
    }
}
