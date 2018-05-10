using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Text scoreTXT;
    public Text timerTXT;
    public Text riddleTXT;
    public Text hintTXT;

    private float timer;

    private GameObject riddlePanel;
    private GameObject topPanel;
    private GameObject bottomPanel;
    private GameObject hintPanel;
    private GameObject menuPanel;

    private bool isPanelUp;

    private string currentHint;
    private bool askHint;
    private int hintTimes = 0;


    // Use this for initialization
    void Start () {
        riddlePanel = GameObject.Find("RiddlePanel");
        riddlePanel.SetActive(false);
        

        topPanel = GameObject.Find("TopPanel");
        bottomPanel = GameObject.Find("BottomPanel");


        menuPanel = GameObject.Find("MenuPanel");
        menuPanel.SetActive(false);

        hintPanel = GameObject.Find("HintPanel");
        hintPanel.SetActive(false);
        isPanelUp = false;
        askHint = false;

        //HideAnswer();
    }
	
	// Update is called once per frame
	void Update () {
        scoreTXT.text = "Score: " + gameObject.GetComponent<GameManager>().GetScore();
        timer = gameObject.GetComponent<GameManager>().GetTimer();

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.RoundToInt(timer % 60f);

        string formatedSeconds = seconds.ToString();

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }

        timerTXT.text = "Timer: " + minutes.ToString("0") + ":" + seconds.ToString("00");
        

        if (askHint)
        {
            askHint = false;

            int charIndex = 0;

            for (int i = 0; i < currentHint.Length; i++)
            {
                if(hintTXT.text[i] == '_')
                {
                    charIndex = i;
                    break;
                }
            }
            
            string test = currentHint[charIndex].ToString();
            int j = hintTXT.text.IndexOf("_");
            while (j != -1)
            {
                hintTXT.text = hintTXT.text.Substring(0, j) + test + hintTXT.text.Substring(j + 1);
                j = -1;
            }
        }
    }

    public void ChangeLevelText(string currRiddle, string currHint)
    {
        riddleTXT.text = currRiddle;
        currentHint = currHint;
    }

    public void OnHintClick()
    {
        if (!isPanelUp)
        {
            isPanelUp = true;
            hintPanel.SetActive(true);
        }
    }

    public void OnHintClosed()
    {
        if (isPanelUp)
        {
            isPanelUp = false;
            hintPanel.SetActive(false);
        }
    }

    public void OnHintAccept()
    {
        if (isPanelUp)
        {
            askHint = true;
            //isPanelUp = false;
            //hintPanel.SetActive(false);
            hintTimes++;
        }
    }

    public void OnRiddleToggle()
    {
        if(!isPanelUp)
        {
            if (riddlePanel.activeInHierarchy)
            {
                riddlePanel.SetActive(false);
            }
            else
            {
                riddlePanel.SetActive(true);
            }
        }
    }

    public void OnMenuClick()
    {
        if (!isPanelUp)
        {
            isPanelUp = true;
            menuPanel.SetActive(true);
        }
    }

    public void OnMenuResume()
    {
        if (isPanelUp)
        {
            isPanelUp = false;
            menuPanel.SetActive(false);
        }
    }

    public void OnMenuExit()
    {
        if (isPanelUp)
        {
            isPanelUp = false;
            menuPanel.SetActive(false);
            SceneManager.LoadScene("MenuScene");
        }
    }

    public int GetHintTimes()
    {
        return hintTimes;
    }

    public void SetHintTimes(int newValue)
    {
        hintTimes = newValue;
    }

    public void HideAnswer()
    {
        Debug.Log(currentHint);
        for (int i = 0; i < currentHint.Length; i++)
        {
            hintTXT.text += "_";
        }
    }

    public void ClearHint()
    {
        hintTXT.text = "";
        currentHint = "";
    }
}
