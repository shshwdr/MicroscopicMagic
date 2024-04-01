using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment : MonoBehaviour
{
    //attach parent under other
    public void AttachedTo(Transform other,Transform parent)
    {
        var attachVector = other.position - transform.position;
        parent.position = parent.position + attachVector;
        //move parent under the other
        parent.parent = other;
    }
}
