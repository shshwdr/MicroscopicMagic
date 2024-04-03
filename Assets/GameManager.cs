using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject gameoverOB;
    public Text gameoverText;
    public int killedAmount = 0;
    [HideInInspector]
    public int isGameOver = 0; 
    public void GameOver()
    {
        gameoverOB.SetActive(true);
        gameoverText.text =
            $"We have killed {killedAmount} cells and reached level {LevelManager.Instance.currentLevel}, good job";
        Time.timeScale = 0;
        isGameOver = 1;
    }

    public void finishGameOver()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    // Update is called once per frame
    void Update()
    {
        //restart level when press r
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
