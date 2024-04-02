using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    
    public string key;

    private void Awake()
    {
        gameObject.SetActive(key == "drag");
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = TutorialManager.tutorialStrings[key];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
