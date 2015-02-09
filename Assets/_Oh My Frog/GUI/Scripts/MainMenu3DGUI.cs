﻿using UnityEngine;
using System.Collections;
using System;

public class MainMenu3DGUI : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonStateX2
    {
        public Transform clicked;
        public Transform unclicked;

        public void setClickedButtonState(bool state)
        {
            clicked.gameObject.SetActive(state);
            unclicked.gameObject.SetActive(!state);
        }
    }

    public ButtonStateX2 playButton;
    public LayerMask layerMask;
    private LoadLevel_GUI levelLoader;

    //before start
    void Awake()
    {
        
    }

	// Use this for initialization
	void Start ()
    {
        //crear instancia para facebook
        if(!FB.IsInitialized)
        {
            //iniciar el FB.init() de facebook
            Comp_Facebook_Feed.Initialize().CallFBInit();
        }

        levelLoader = GetComponent<LoadLevel_GUI>();
        playButton.setClickedButtonState(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, transform.position.z));

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, layerMask))
                {
                    Debug.Log("HITTED!");
                    if (hit.collider.name == "playCollider")
                    {
                        playButton.setClickedButtonState(true);
                        levelLoader.loadLevel("InGame");
                    }
                    else if (hit.collider.name == "buyCollider")
                    {
                        Debug.Log("Shop!");
                    }
                    else if(hit.collider.name == "FBCollider")
                    {
                        Debug.Log("FB!");
                        //facebook methods
                        clickOrTouchSocialFBButton();
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 2);

                if (Physics.Raycast(ray, out hit, 100, layerMask))
                {
                    if (hit.collider.name == "playCollider")
                    {
                        playButton.setClickedButtonState(true);
                        levelLoader.loadLevel("InGame");

                    }
                    else if(hit.collider.name == "buyCollider")
                    {
                        Debug.Log("Shop!");
                    }
                    else if(hit.collider.name == "FBCollider")
                    {
                        Debug.Log("FB!");
                        //facebook methods
                        clickOrTouchSocialFBButton();
                    }
                }
            }
        }
	}

    public void clickOrTouchSocialFBButton()
    {
        //logear y crear feed
        Comp_Facebook_Feed.Initialize().CallFBLogin();
    }    
}
