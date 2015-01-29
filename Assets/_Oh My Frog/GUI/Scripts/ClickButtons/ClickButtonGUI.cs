using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClickButtonGUI : MonoBehaviour {

    //si se usa el SetActive ya no es necesario el CanvasGroups
    //public CanvasGroup[] listCanvasGroups = null;
    private string[] languagesArray = new string[] { "SPANISH", "ENGLISH", "FRENCH", "GERMAN", "ITALIAN" };
    public GameObject panelTotem, panelRedesSociales, panelAudioOptions;

    void Awake()
    {
        //para poder usar un objeto inactivo, iniciarlo como inactivo desde el awake, mejor que manualmente desde el editor
        panelAudioOptions.SetActive(false);
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void clickButton(Text textButton)
    {
        switch(textButton.text.ToLower())
        {
            case "options":
                openOrClosePanel(true, 0.5f);
                break;
            case "accept":
                openOrClosePanel(false, 1f);
                break;
        }
        Debug.Log(textButton.text);
    }

    private void openOrClosePanel(bool status, float alpha)
    {
        panelAudioOptions.SetActive(status);
        panelTotem.GetComponent<CanvasGroup>().alpha = alpha;
        panelTotem.GetComponent<CanvasGroup>().interactable = !status;
        panelRedesSociales.GetComponent<CanvasGroup>().alpha = alpha;
        panelRedesSociales.GetComponent<CanvasGroup>().interactable = !status;
    }

    public void changeLanguage(Button langButton)
    {
        int positionInArray;

        for (int i = 0; i < languagesArray.Length; i++)
		{
            if(languagesArray[i].Equals(langButton.GetComponentInChildren<Text>().text))
            {
                if(i == languagesArray.Length - 1)
                {
                    positionInArray = 0;
                }
                else
                {
                    positionInArray = i + 1;
                }
                langButton.GetComponentInChildren<Text>().text = languagesArray[positionInArray];
                break;
            }	 
	    }
    }
}
