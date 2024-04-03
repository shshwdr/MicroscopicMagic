using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    [HideInInspector] public static Dictionary<string, string> tutorialStrings = new Dictionary<string, string>()
    {
        {"drag", "Hey there! Meet Cecil, the Cancer Cell – yep, that's me! Tired of the same old routine in the pancreas, I've got big dreams of evolution and protection.\n\nBut uh-oh, here come General White Blood Cell and his gang of immune defenders! No worries, though! With my cancer cell buddies, we're on a quest to evolve and protect our turf.\n\nNow nudge the cell towards the bottom core(that's me!)"},
        //{ "drag", "Drag cell to core" },
        { "afterFirstDrag", "The blood would go from core to cell(s), activating whatever they've got. " },
        { "levelup1", "Oh, decisions, decisions! Pick a cell and slap it on any spot on the core or previous cell(highly recommend)" },
        { "afterLevelup1", "When blood moves back, it would trigger the ability again. " },
        { "levelup2", "These support cells? Yeah, they're the chill ones. No attacking, just throwing buffs at the next sucker who swings by" },
        { "afterLevelup2", "Now let's see how long can we survive. Remember you can drag cells around anytime you want." },
    };
HashSet<string> finishedTutorials = new HashSet<string>();
    private Dictionary<string, TutorialText> tutorialTexts;
    // Start is called before the first frame update
    void Start()
    {
        tutorialTexts = new Dictionary<string, TutorialText>();
        foreach (var text in GameObject.FindObjectsOfType<TutorialText>(true))
        {
            tutorialTexts[text.key] = text;
        }
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool hasFinishedTutorial(string key)
    {
        return finishedTutorials.Contains(key);
    }
    public void finishTutorial(string key)
    {
        if (key == "drag")
        {
            
            Time.timeScale = 1;
        }
        tutorialTexts[key].gameObject.SetActive(false);
        
    }

    public void startTutorial(string key)
    {
        if (finishedTutorials.Contains(key))
        {
            return;
        }
        tutorialTexts[key].gameObject.SetActive(true);
        finishedTutorials.Add(key);
    }
}
