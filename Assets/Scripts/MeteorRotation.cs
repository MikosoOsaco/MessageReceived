using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**MeteorRotation Class
//* Handles Meteor rotation for A E S T H E T I C
//* Contains a public float to customize the speed of rotation

public class MeteorRotation : MonoBehaviour {

    [Header("Movement Variables")]
    public float rotSpeed = 0.1f;
    //public float speed = 0.0f;
    //public Transform target;
    //public Transform respawn;
    //Vector3 direction;

    //Rigidbody2D rBody2D;

    void Start()
    {
        //rBody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //rBody2D.velocity = direction * speed * Time.fixedDeltaTime;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, rotSpeed));

        //if (speed > 0)
        //{
        //    direction = target.transform.position - transform.position;

        //    if (transform.position == target.transform.position)
        //    {
        //        transform.position = respawn.transform.position;
        //    }
        //}
    }
}
