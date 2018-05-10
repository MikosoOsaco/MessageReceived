using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**StarAnimation Class
//* Randomizes which animation will play first, to give variety to the stars

public class StarAnimation : MonoBehaviour {

    Animator anim;     //Grab the nimator of the star

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        int rand = Random.Range(0, 4);
        anim.SetInteger("Init", rand);

        transform.Rotate(new Vector3(0.0f, 0.0f, 25.0f));
	}
}
