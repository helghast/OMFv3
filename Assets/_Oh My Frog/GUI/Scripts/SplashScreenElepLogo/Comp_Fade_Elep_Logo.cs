using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Comp_Fade_Elep_Logo : MonoBehaviour {

    public Image elepLogo;
    public float timeFade;
    public GameObject panelElepLogo;

    void Awake()
    {
        if(panelElepLogo == null)
        {
            panelElepLogo = GameObject.Find("PanelElep_Logo");
        }
        if(elepLogo == null)
        {
            elepLogo = GameObject.Find("ImageElepLogo").GetComponent<Image>();
        }
        //necesario ponerle en el awake a 0 si se quiere hacer un fadein inicial, sino no funciona.
        fadeLogo(0f, 0f);
        //elepLogo.CrossFadeAlpha(0f, 0f, false);
    }

	// Use this for initialization
	void Start () {
        fadeLogo(1f, timeFade);
	}
	
	// Update is called once per frame
	void Update () {
        if(timeFade > 0)
        {
            timeFade = timeFade - Time.deltaTime;
        }
        else
        {
            gameObject.GetComponent<Comp_LoadFromSplashScreen>().panelLoading.SetActive(true);
            panelElepLogo.SetActive(false);
        }
	}

    private void fadeLogo(float alpha, float time)
    {
        elepLogo.CrossFadeAlpha(alpha, time, false);
    }

    public float getTimeFade()
    {
        return timeFade;
    }
}
