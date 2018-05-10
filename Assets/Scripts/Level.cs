using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    [Header("Question")]
    [TextArea(5, 10)]
    public string riddle;
    [Header("Answer (ALL CAPS)")]
    public string answer;
    [Header("Spawn Points (Put dummy satellites in here)")]
    public GameObject[] spawnPoints;
    [Header("Satellite Types (The order is important)")]
    public GameObject[] satelliteTypes;

	// Use this for initialization
	void Start () {

        if (GameObject.FindWithTag("GameManager") != null)
        {
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().UpdateText(riddle, answer);
        }
        else
        {
            Debug.Log("Level could not find GameMananger");
        }
        
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject tempS;
            int type = spawnPoints[i].GetComponent<DummySatellite>().satelliteType;
            tempS = Instantiate(satelliteTypes[type], spawnPoints[i].transform.position, spawnPoints[i].transform.rotation, transform);
            tempS.GetComponent<SatelliteValue>().value = spawnPoints[i].GetComponent<DummySatellite>().value;
            Destroy(spawnPoints[i]);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
