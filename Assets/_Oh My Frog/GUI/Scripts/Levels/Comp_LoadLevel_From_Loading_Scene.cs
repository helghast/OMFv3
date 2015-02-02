using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Comp_LoadLevel_From_Loading_Scene : MonoBehaviour {

    public float waitSeconds;
    private float tempSeconds;
    public Slider loadingProgressBar;
    public Text loadingText = null;
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
        ratio = loadingProgressBar.maxValue / waitSeconds;
    }
	// Use this for initialization
	void Start () {
        if(waitSeconds == 0)
        {
            Debug.LogError("Faltan los segundos en LoadLevel_From_Loading_Scenes.cs de LoadingScene MainCamera");            
        }
        else
        {
            tempSeconds = waitSeconds;
            Invoke("loadLevelFromPlayerPrefs", waitSeconds);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(tempSeconds > 0f)
        {
            timeRemainingTooltip();
            fillLoadingProgressBar();
        }
	}

    //para imprimir los segundos restantes del proceso de carga
    private void timeRemainingTooltip()
    {
        tempSeconds = tempSeconds - Time.deltaTime;
        loadingText.text = "Loading..." + (int) tempSeconds + "s";
    }

    //para rellenar la progressbar segun el tiempo indicado
    public void fillLoadingProgressBar()
    {
        loadingProgressBar.value = Mathf.MoveTowards(loadingProgressBar.value, loadingProgressBar.maxValue, Time.deltaTime * ratio);
    }

    /*si se llama a este metodo dandole al play a la escena Loading dara error porque el playerprefs no se ha modificado desde la escena Loading.*/
    private void loadLevelFromPlayerPrefs()
    {
        Application.LoadLevel(PlayerPrefs.GetString("level"));
        
    }
}
