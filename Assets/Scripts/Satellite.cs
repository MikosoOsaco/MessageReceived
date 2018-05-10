using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Satellite : MonoBehaviour {

    private Vector2 direction;
    private float rotation;
    
    public SpriteRenderer sate; //Satellite sprite
    public SpriteRenderer aim;

    public Sprite originalSprite;
    public Sprite hoverSprite;

    //public value so that we can switch each objects character from the editor
    public string value;

    Slider slider;
    Image sliderPanel;

    public TextMesh textMesh;

    public bool isClicked;
    private bool canInteract, isDrag;

	// Use this for initialization
	void Start () {
        
        slider = GameObject.FindWithTag("Canvas").transform.GetChild(1).GetComponent<Slider>();
        sliderPanel = GameObject.FindWithTag("Canvas").transform.GetChild(0).GetComponent<Image>();
        
        sate = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        aim = gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>();
        aim.enabled = false;

        isClicked = false;
        canInteract = true;

        direction = transform.forward;
        rotation = 0f;

        value = GetComponent<SatelliteValue>().value;
        textMesh.text = value;
        GetComponent<SatelliteValue>().direction = direction;
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<SatelliteValue>().direction = direction;
        slider = GameObject.FindWithTag("Canvas").transform.GetChild(1).GetComponent<Slider>();
        sliderPanel = GameObject.FindWithTag("Canvas").transform.GetChild(0).GetComponent<Image>();

        if (GameObject.FindGameObjectWithTag("Beam") == null)
        {
            GetComponent<SatelliteValue>().visited = false;
        }

        if (GameObject.FindGameObjectWithTag("Beam") != null)
        {
            if (GameObject.FindGameObjectWithTag("Beam").GetComponent<Beam>().GetFired())
            {
                slider.gameObject.SetActive(false);
                sliderPanel.gameObject.SetActive(false);
                GameObject[] satellites = GameObject.FindGameObjectsWithTag("Satellite");

                for (int i = 0; i < satellites.Length; i++)
                {
                    satellites[i].GetComponent<Satellite>().isClicked = false;
                    satellites[i].GetComponent<Satellite>().sate.sprite = originalSprite;
                    satellites[i].GetComponent<Satellite>().aim.enabled = false;
                }

                //GameObject.Find("Finish").GetComponent<GoalPlanet>().isClicked = false;
                //GameObject.Find("Finish").GetComponent<GoalPlanet>().aim.enabled = false;

                //GameObject.Find("Start").GetComponent<StartPlanet>().isClicked = false;
                //GameObject.Find("Start").GetComponent<StartPlanet>().aim.enabled = false;
            }
        }
        else
        {
            #region Mobile Inputs

            //has a touch been registered
            if (Input.touches.Length > 0 && canInteract == true)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    if (isClicked == true && !slider.gameObject.GetComponent<Collider2D>().OverlapPoint(Input.touches[0].position))
                    {
                        slider.gameObject.SetActive(false);
                        sliderPanel.gameObject.SetActive(false);
                        GameObject[] satellites = GameObject.FindGameObjectsWithTag("Satellite");

                        for (int i = 0; i < satellites.Length; i++)
                        {
                            satellites[i].GetComponent<Satellite>().isClicked = false;
                            satellites[i].GetComponent<Satellite>().sate.sprite = originalSprite;
                            satellites[i].GetComponent<Satellite>().aim.enabled = false;
                        }

                        //GameObject.Find("Finish").GetComponent<GoalPlanet>().isClicked = false;
                        //GameObject.Find("Finish").GetComponent<GoalPlanet>().aim.enabled = false;

                        //GameObject.Find("Start").GetComponent<StartPlanet>().isClicked = false;
                        //GameObject.Find("Start").GetComponent<StartPlanet>().aim.enabled = false;
                    }
                }
            }
            #endregion

            if (isClicked == true)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, slider.value - 180f);
            }

            SetDirection(transform.up);
        }
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

    public void OnClick()
    {
        if (isClicked == true)
        {
            isClicked = false;
            slider.gameObject.SetActive(false);
            sliderPanel.gameObject.SetActive(false);
            sate.sprite = originalSprite; //Reset colour when deselected
            aim.enabled = false;
        }
        else
        {
            GameObject[] satellites = GameObject.FindGameObjectsWithTag("Satellite");
            
            for(int i = 0; i < satellites.Length; i++)
            {
                satellites[i].GetComponent<Satellite>().isClicked = false;
                satellites[i].GetComponent<Satellite>().sate.sprite = originalSprite;
                satellites[i].GetComponent<Satellite>().aim.enabled = false;
            }

            //GameObject.Find("Finish").GetComponent<GoalPlanet>().isClicked = false;
            //GameObject.Find("Finish").GetComponent<GoalPlanet>().aim.enabled = false;

            //GameObject.Find("Start").GetComponent<StartPlanet>().isClicked = false;
            //GameObject.Find("Start").GetComponent<StartPlanet>().aim.enabled = false;

            if (transform.rotation.eulerAngles.z > 180)
            {
                float temp = transform.rotation.eulerAngles.z - 180;
                slider.value = Mathf.Abs(temp);
            }
            else
            {
                slider.value = Mathf.Abs(180f + (transform.rotation.eulerAngles.z));
            }
            sate.sprite = hoverSprite; //Alter the sprite to show it is selected
            aim.enabled = true;

            slider.gameObject.SetActive(true);
            sliderPanel.gameObject.SetActive(true);
            isClicked = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
