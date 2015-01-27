using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Comp_LoadingScreen_ProgressBar : MonoBehaviour {

    public Slider loadingProgressBar;
    public Text loadingText = null;
    public float timeLoading;
    private float ratio;

    void Awake()
    {
        if(loadingProgressBar == null)
        {
            loadingProgressBar = GameObject.Find("LoadingProgressBar").GetComponent<Slider>();
        }
        if(loadingText == null)
        {
            loadingText = GameObject.Find("LoadingText").GetComponent<Text>();
        }
        //a que velocidad se llena la progressbar depende del tiempo y del tamaño de esta
        ratio = loadingProgressBar.maxValue / timeLoading;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(timeLoading > 0f)
        {
            timeRemainingTooltip();
            fillLoadingProgressBar();
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
