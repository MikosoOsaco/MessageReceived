using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public float moveSpeed = 20.0f;

    public GameObject menuScreen;
    public GameObject howToPlayScreen;
    public GameObject creditsScreen;

    public GameObject menuPos;

    bool menuFromHowToPlay = false;
    bool menuFromCredits = false;
    bool onHowToPlay = false;
    bool onCredits = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (onHowToPlay)
        {
            if (howToPlayScreen.transform.position.x < menuPos.transform.position.x - 0.1)
            {
                menuScreen.transform.position += Vector3.right * Time.deltaTime * moveSpeed;
                howToPlayScreen.transform.position += Vector3.right * Time.deltaTime * moveSpeed;
                creditsScreen.transform.position += Vector3.right * Time.deltaTime * moveSpeed;
            }
            else
            {
                howToPlayScreen.transform.position = menuPos.transform.position;
            }
        }
        if (onCredits)
        {
            if (creditsScreen.transform.position.x > menuPos.transform.position.x + 0.1)
            {
                menuScreen.transform.position -= Vector3.right * Time.deltaTime * moveSpeed;
                howToPlayScreen.transform.position -= Vector3.right * Time.deltaTime * moveSpeed;
                creditsScreen.transform.position -= Vector3.right * Time.deltaTime * moveSpeed;
            }
            else
            {
                creditsScreen.transform.position = menuPos.transform.position;
            }
        }
        if(menuFromHowToPlay)
        {
            if (menuScreen.transform.position.x > menuPos.transform.position.x + 0.1)
            {
                menuScreen.transform.position -= Vector3.right * Time.deltaTime * moveSpeed;
                howToPlayScreen.transform.position -= Vector3.right * Time.deltaTime * moveSpeed;
                creditsScreen.transform.position -= Vector3.right * Time.deltaTime * moveSpeed;
            }
            else
            {
                menuScreen.transform.position = menuPos.transform.position;
            }
        }
        if (menuFromCredits)
        {
            if (menuScreen.transform.position.x < menuPos.transform.position.x - 0.1)
            {
                menuScreen.transform.position += Vector3.right * Time.deltaTime * moveSpeed;
                howToPlayScreen.transform.position += Vector3.right * Time.deltaTime * moveSpeed;
                creditsScreen.transform.position += Vector3.right * Time.deltaTime * moveSpeed;
            }
            else
            {
                menuScreen.transform.position = menuPos.transform.position;
            }
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void HowToPlay()
    {
        menuFromHowToPlay = false;
        menuFromCredits = false;
        onCredits = false;
        onHowToPlay = true;
    }

    public void Credits()
    {
        menuFromHowToPlay = false;
        menuFromCredits = false;
        onHowToPlay = false;
        onCredits = true;
    }

    public void ReturnFromHowToPlay()
    {
        onHowToPlay = false;
        onCredits = false;
        menuFromCredits = false;
        menuFromHowToPlay = true;        
    }

    public void ReturnFromCredits()
    {
        onHowToPlay = false;
        onCredits = false;
        menuFromHowToPlay = false;
        menuFromCredits = true;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
