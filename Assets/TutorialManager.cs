using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    [HideInInspector] public static Dictionary<string, string> tutorialStrings = new Dictionary<string, string>()
    {
        { "drag", "Drag cell to core" },
    };

    private Dictionary<string, TutorialText> tutorialTexts;
    // Start is called before the first frame update
    void Start()
    {
        tutorialTexts = new Dictionary<string, TutorialText>();
        foreach (var text in GameObject.FindObjectsOfType<TutorialText>())
        {
            tutorialTexts[text.key] = text;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void finishTutorial(string key)
    {
        tutorialTexts[key].gameObject.SetActive(false);
        
    }

    public void startTutorial(string key)
    {
        
        tutorialTexts[key].gameObject.SetActive(true);
    }
}
