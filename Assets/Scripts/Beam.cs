using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    // Public Beam Properties
    [Header("Beam Properties")]
    public float speed = 400.0f;

    // Private Beam Properties
    // Direction
    Vector3 direction = Vector3.up;
    // Word value
    public string value = "";
    // Check if fired
    bool fired = false;
    // Check if reached goal
    bool hitFinish = false;
    bool finish = false;
    bool hitWall = false;
    // Reference to satellite
    SatelliteValue tempS;
    // Components
    Rigidbody2D rBody2D;
    SpriteRenderer spriteRenderer;
    // Shrink and growing variables
    Vector3 previousScale = Vector3.zero;
    bool shrink = false;
    bool grow = false;
    bool start = true;
    float resizeSpeed = 15.0f;

    // Use this for initialization
    void Start()
    {
        previousScale = transform.localScale;
        transform.localScale = Vector3.zero;
        fired = true;
        rBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rBody2D.velocity = direction * speed * Time.fixedDeltaTime;
    }

    void Update()
    {
        UpdateBeam();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Satellite")
        {
            if (!grow)
            {
                HitSatellite(other.gameObject.GetComponent<SatelliteValue>());
            }
        }

        if (other.gameObject.tag == "Wall")
        {
            HitWall();
        }

        if (other.gameObject.tag == "Finish")
        {
            HitFinish();
        }
    }

    void UpdateBeam()
    {
        if (shrink)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale -= new Vector3(resizeSpeed, resizeSpeed, resizeSpeed) * Time.deltaTime;
            }
            else
            {
                if (!tempS.visited)
                {
                    shrink = false;
                    grow = true;
                    transform.rotation = tempS.transform.rotation;
                    transform.position = tempS.transform.position;
                }
                else
                {
                    fired = false;
                    Destroy(gameObject);
                }
            }
        }
        else if (grow)
        {
            if (transform.localScale.x < previousScale.x)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                transform.localScale += new Vector3(resizeSpeed, resizeSpeed, resizeSpeed) * Time.deltaTime;
                transform.position = tempS.transform.position;
                direction = tempS.direction;
                tempS.visited = true;
            }
            else
            {
                value += tempS.value;
                transform.localScale = previousScale;
                grow = false;
            }
        }
        else if (start)
        {
            if (transform.localScale.x < previousScale.x)
            {
                transform.localScale += new Vector3(resizeSpeed, resizeSpeed, resizeSpeed) * Time.deltaTime;
            }
            else
            {
                transform.localScale = previousScale;
                start = false;
            }
        }
        else if (hitFinish)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale -= new Vector3(resizeSpeed, resizeSpeed, resizeSpeed) * Time.deltaTime;
            }
            else
            {
                spriteRenderer.enabled = false;
                direction = Vector3.zero;
                finish = true;
            }
        }
        else if (hitWall)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale -= new Vector3(resizeSpeed, resizeSpeed, resizeSpeed) * Time.deltaTime;
            }
            else
            {
                fired = false;
                Destroy(gameObject);
            }
        }
        else
        {

        }
    }

    void HitSatellite(SatelliteValue s)
    {
        shrink = true;
        tempS = s;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void HitFinish()
    {
        hitFinish = true;
    }

    void HitWall()
    {
        hitWall = true;
    }

    IEnumerator Resize()
    {
        while (transform.localScale.x > 0)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.2f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public Vector3 GetDirection() { return direction; }

    public void SetDirection(Vector3 dir) { direction = dir; }

    public string GetValue() { return value; }

    public bool GetFired() { return fired; }

    public bool GetFinish() { return finish; }
}