using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Comp_LoadFromSplashScreen : MonoBehaviour {

    public CanvasGroup panelSplashScreen = null;
    public CanvasGroup panelTouchScreen = null;
    public Text loadingText = null;
    public Button buttonTouchScreen = null;
    public Slider loadingProgressBar;
    //no se puede acceder/hacer un find de un objeto que esta SetActive(false).
    //para encontrarlo es mejor tenerlo inicialmente activado y en el awake buscarlo y desactivarlo
    private GameObject panelLoading = null;
    private GameObject panelTouch = null;

    public float timeLoading;
    private float ratio;

    void Awake()
    {
        //por si se nos olvida asignarlos manualmente
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
        panelLoading = GameObject.Find("PanelLoading");
        panelTouch = GameObject.Find("PanelTouchBG");
        panelTouch.SetActive(false);
        //a que velocidad se llena la progressbar depende del tiempo y del tamaño de esta
        ratio = loadingProgressBar.maxValue / timeLoading;
    }

	// Use this for initialization
	void Start () {        

	}
	
	// Update is called once per frame
	void Update () {
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

    /* para evitar usar dos escenas, se usa una sola y se cambia el alpha e interactividad de ambos paneles */
    private void changeAlphaSplashScreen()
    {
        //mejor activar/desactivar ambos objetos panel
        panelLoading.SetActive(false);
        panelTouch.SetActive(true);
        /*panelSplashScreen.alpha = 0f;
        panelSplashScreen.interactable = false;
        panelSplashScreen.blocksRaycasts = false;
        panelTouchScreen.alpha = 1f;
        panelTouchScreen.interactable = true;
        panelTouchScreen.blocksRaycasts = true;*/
        //loadFromSplashScreen();
    }

    /* metodo llamado desde el Boton de la UI cuando es clickado/toucheado */
    public void loadFromSplashScreen()
    {
        //esto peta el unity un rato
        //buttonTouchScreen.onClick.AddListener(() => { Application.LoadLevel("MainMenu"); });
        //esto no peta
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
}
