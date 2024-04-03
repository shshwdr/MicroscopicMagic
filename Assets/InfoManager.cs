using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : Singleton<InfoManager>
{
    public Text text;
    public GameObject go;
    public Image progressImage;

    public void UpdateProgress(float value)
    {
        progressImage.fillAmount = value;
    }
    public void Show(int cellAbility)
    {
        go.SetActive(true);
        text.text = UpgradeSelectObject.Instance.Description[cellAbility];
    }

    public void Hide()
    {
        
        go.SetActive(false);
    }
}
