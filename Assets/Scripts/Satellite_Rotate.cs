using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite_Rotate : MonoBehaviour {

    private Vector2 direction;
    public float rotSpeed = 0.5f;

    //public value so that we can switch each objects character from the editor
    public string value;

    public TextMesh textMesh;

    public bool visited = false;

    // Use this for initialization
    void Start () {
        textMesh.text = value;
        direction = transform.forward;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Beam") == null)
        {
            visited = false;
        }

        transform.Rotate(new Vector3(0.0f, 0.0f, rotSpeed));
        SetDirection(transform.up);
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    public Quaternion GetRotate()
    {
        return transform.rotation;
    }
}
