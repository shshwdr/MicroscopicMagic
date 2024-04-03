using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CellAbility : MonoBehaviour
{
    public virtual void TriggerAbility(Blood blood)
    {
        GetComponent<Cell>().renderer.transform.localScale = Vector3.one * 0.2f;
        GetComponent<Cell>().renderer.transform.DOScale(Vector3.one * 0.25f, 0.15f).SetLoops(2, LoopType.Yoyo);
        // GetComponent<Cell>().renderer?.transform.DOPunchScale(Vector3.one*0.2f*1.1f, 0.2f);
    }
}
