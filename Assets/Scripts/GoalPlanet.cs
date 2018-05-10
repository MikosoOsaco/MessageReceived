using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//**GoalPlanet Class
//* Holds the goal string to complete the level
//* Checks the Beam's string to the goal string and determines if the level can be completed
//* Calls on the GameManager to intialize next level

public class GoalPlanet : MonoBehaviour {

    //Goal Planet Variables
    [Header("Goal Planet Variables")]
    private string goalValue;            //The final string to be compared with the beam
    public GameObject level;

    //Game Manager Variables
    private GameObject gameManager;     //Instance of the active GameManager
    private bool beamEntered;           //Check to see if beam has entered
    private string beamValue;           //Reference to beam's value

    void Start () {
        //Grab an instance of the GameManager
        gameManager = GameObject.FindWithTag("GameManager");
        goalValue = level.GetComponent<Level>().answer;
    }
	
	void Update ()
    {
		if(beamEntered)
        {
            CheckValue(beamValue);
        }
	}

    void CheckValue(string value_)
    {
        //If the value of the Beam is equal to the goalValue then call the GameManager's NextLevel method
        if (value_ == goalValue)
        {
            if (GameObject.FindWithTag("Beam").GetComponent<Beam>().GetFinish())
            {
                gameManager.GetComponent<GameManager>().AddScore(goalValue.Length * 100);
                gameManager.GetComponent<GameManager>().LevelClear();
                //Debug.Log("Next Level!");
                //Debug.Log(gameManager.GetComponent<GameManager>().levelCount);
                beamEntered = false;
            }
        }
        else
        {
            if (GameObject.FindWithTag("Beam").GetComponent<Beam>().GetFinish())
            {
                Destroy(GameObject.FindWithTag("Beam").gameObject);
                beamEntered = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //If the Beam collides with the Goal planet, grab it's value
        if(col.gameObject.tag == "Beam")
        {
            beamEntered = true;
            beamValue = col.GetComponent<Beam>().GetValue();
        }
    }
}
