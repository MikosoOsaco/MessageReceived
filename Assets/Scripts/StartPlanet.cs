using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**StartPlanet Class
//* Handles the Beam object
//* Get's input from the player and initialized the Beam

public class StartPlanet : MonoBehaviour {

    public GameObject beam;             //Instance of the beam
    public Transform firePosition;      //Position the beam will be fired from

    //Game Manager Variables
    private GameObject gameManager;     //Instance of the active GameManager

	void Start () {
        //Grab an instance of the GameManager (Will be used to send the fireCount to the GameManager)
        gameManager = GameObject.FindWithTag("GameManager");
        
    }
	
	void Update () {
        //Test to fire the Beam
        if (Input.GetKeyDown(KeyCode.Space))
            FireBeam();
	}

    public void FireBeam()
    {
        //Checks to see if a Beam exists, if not then instantiate it (Prevent more than one being fired)
        //Will need to be changed later to use the Beam's "isFired" bool instead (For when we are using more than one beam in a level)
        GameObject testBeam = GameObject.FindWithTag("Beam");
        if (testBeam == null)
        {
            GameObject temp = Instantiate(beam, firePosition.position, transform.rotation, transform);
            Vector2 dir = firePosition.position - transform.position;
            temp.GetComponent<Beam>().SetDirection(dir);
            gameManager.GetComponent<GameManager>().timesFired++;
        }
    }
}
