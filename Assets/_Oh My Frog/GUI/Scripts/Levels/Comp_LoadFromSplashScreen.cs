﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using Facebook;
using System.Linq;

public class Comp_LoadFromSplashScreen : MonoBehaviour {

    public CanvasGroup panelSplashScreen = null;
    public CanvasGroup panelTouchScreen = null;
    public Text loadingText = null;
    public Button buttonTouchScreen = null;
    public Slider loadingProgressBar;
    //no se puede acceder/hacer un find de un objeto que esta SetActive(false).
    //para encontrarlo es mejor tenerlo inicialmente activado y en el awake buscarlo y desactivarlo
    public GameObject panelLoading = null;
    public GameObject panelTouch = null;

    public float timeLoading;
    private float ratio;

    void Awake()
    {
        //por si se nos olvida asignarlos manualmente
        if(panelLoading == null)
        {
            panelLoading = GameObject.Find("PanelLoading");
        }
        if(panelTouch == null)
        {
            panelTouch = GameObject.Find("PanelTouchBG");
        }
        if(panelSplashScreen == null)
        {
            panelSplashScreen = GameObject.Find("PanelLoading").GetComponent<CanvasGroup>();
        }
        if(panelTouchScreen == null)
        {
            panelTouchScreen = GameObject.Find("PanelTouchBG").GetComponent<CanvasGroup>();
        }
        if(loadingText == null)
        {
            loadingText = panelSplashScreen.GetComponentInChildren<Text>();
        }
        if(buttonTouchScreen == null)
        {
            buttonTouchScreen = panelTouchScreen.GetComponentInChildren<Button>();
        }
        if(loadingProgressBar == null)
        {
            loadingProgressBar = GameObject.Find("LoadingProgressBar").GetComponent<Slider>();
        }
        
        panelLoading.SetActive(false);
        panelTouch.SetActive(false);
        //a que velocidad se llena la progressbar depende del tiempo y del tamaño de esta
        ratio = loadingProgressBar.maxValue / timeLoading;
    }

	// Use this for initialization
	void Start () {        

	}
	
	// Update is called once per frame
	void Update () {
        //solo si el panelloading esta activo
        if(panelLoading.activeSelf)
        {
            if(timeLoading > 0)
            {
                timeRemainingTooltip();
                fillLoadingProgressBar();
            }
            else
            {
                changeAlphaSplashScreen();
            } 
        }        
	}

    /* para evitar usar dos escenas, se usa una sola y se cambia el alpha e interactividad de ambos paneles */
    private void changeAlphaSplashScreen()
    {
        //mejor activar/desactivar ambos objetos panel, en vez de controlar interactable y alpha
        panelLoading.SetActive(false);
        //panelTouch.SetActive(true);
        //ir directamente al finalizar el relleno de la progressbar, al mainmenu
        //ya no se pasa por el panelTouchScreen
        loadFromSplashScreen();
    }

    /* metodo llamado desde el Boton de la UI cuando es clickado/toucheado */
    public void loadFromSplashScreen()
    {
        //esto peta el unity un rato
        //buttonTouchScreen.onClick.AddListener(() => { Application.LoadLevel("MainMenu"); });

        //esto no peta. Llamar desde el nuevo UI
        Application.LoadLevel("MainMenu");
    }

    //llamado por el onclick de los botones del segundo panel
    public void buttonsActions(Button buttonText)
    {
        if(buttonText == null)
        {
            Debug.Log("Falta asignar el texto en el onclick del boton");
        }
        else
        {
            Debug.Log(buttonText.name);
        }
    }

    //para imprimir los segundos restantes del proceso de carga
    private void timeRemainingTooltip()
    {
        timeLoading = timeLoading - Time.deltaTime;
        loadingText.text = "Loading..." + (int) timeLoading + "s";
    }

    //para rellenar la progressbar segun el tiempo indicado
    public void fillLoadingProgressBar()
    {
        loadingProgressBar.value = Mathf.MoveTowards(loadingProgressBar.value, loadingProgressBar.maxValue, Time.deltaTime * ratio);
    }

    public void openvideoURL()
    {
        Application.OpenURL("https://www.youtube.com/watch?x-yt-ts=1421914688&feature=player_detailpage&x-yt-cl=84503534&v=6-aelWFdJXw");
        //proonly
        //Handheld.PlayFullScreenMovie("Clara.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
    }

    public void makePostToFacebook()
    {
        Application.OpenURL("https://www.facebook.com/ohmyfrog.surfrescue");
        //FacebookManager.Instance().PostToFacebook();
    }
}
