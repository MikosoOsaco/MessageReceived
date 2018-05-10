using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//**GameManager Class
//* Handles all Game-related scirpting.
//* Score, Time, Level count, Transitions

public class GameManager : MonoBehaviour {

    //Level Variables
    [Header("Level Variables")]
    public GameObject[] levels;             //Public array of all levels
    private GameObject currentLevel;        //Currently selected level
    private bool levelActive = false;       //Bool check if a level is already active
    public int levelCount = 0;             //Int to track the current level
    private int levelScore = 0;
    private int score = 0;                  //Total score
    public float levelTimer = 180.0f;
    private float timer = 0.0f;            //Timer per level
    private bool gameOver = false;         //Game over check

    public int timesFired = 0;             //Negavtive one so you dont lose points for first try

    public GameObject gameOverUI;                    // UI for when player wins
    public GameObject winUI;                        // UI for when player loses
    public GameObject uiManager;                    // player UI
    public Text timerText;
    public Text scoreText;

    void Start () {
        gameOverUI.SetActive(false);
        winUI.SetActive(false);
        NextLevel();
    }

	void Update () {
        if (!gameOver)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                LevelFail();
            }
        }
    }

    public int GetScore()
    {
        return score;
    }

    public float GetTimer()
    {
        return timer;
    }

    public void LevelClear()
    {
        
        
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.RoundToInt(timer % 60f);

        string formatedSeconds = seconds.ToString();

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }

        levelScore += minutes * 100;
        levelScore -= GetComponent<UIManager>().GetHintTimes() * 100;
        if (timesFired > 0)
        {
            levelScore -= timesFired * 50;
        }

        if (levelScore <= 0)
        {
            levelScore = 0;
        }

        timerText.text = "Time Remaining:\n" + minutes.ToString("0") + ":" + seconds.ToString("00");
        scoreText.text = "Score:\n" + levelScore.ToString();
        gameOver = true;
        winUI.SetActive(true);
        uiManager.SetActive(false);

        score += levelScore;
    }

    public void LevelFail()
    {
        gameOver = true;
        gameOverUI.SetActive(true);
        uiManager.SetActive(false);
    }

    public void NextLevel()
    {
        if (levelCount != levels.Length)
        {
            if (levelActive)
                DestroyCurrentLevel();

            //Generate a new map based the levelCount
            currentLevel = Instantiate(levels[levelCount]);
            
            levelActive = true;
            levelCount++;            
            gameOver = false;
            winUI.SetActive(false);
            uiManager.SetActive(true);
            timer = levelTimer;
            timesFired = 0;
            GetComponent<UIManager>().SetHintTimes(0);
        } 
        else
        {
            SceneManager.LoadScene("MenuScene");
        }
        //TO DO
        //When the levelCount reaches the array limit, initiate a victory screen
    }

    void DestroyCurrentLevel()
    {
        Destroy(currentLevel);
        levelActive = false;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void Retry()
    {
        DestroyCurrentLevel();
        //Generate a new map based the levelCount
        currentLevel = Instantiate(levels[levelCount]);
        uiManager.SetActive(true);
        gameOverUI.SetActive(false);
        gameOver = false;
        timer = levelTimer;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void AddScore(int newScore)
    {
        levelScore += newScore;
    }

    public void UpdateText(string riddle, string answer)
    {
        GetComponent<UIManager>().ClearHint();
        gameObject.GetComponent<UIManager>().ChangeLevelText(riddle, answer);
        GetComponent<UIManager>().HideAnswer();
    }
}
