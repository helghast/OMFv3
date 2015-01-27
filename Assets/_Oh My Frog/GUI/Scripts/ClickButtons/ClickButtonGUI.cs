using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClickButtonGUI : MonoBehaviour {

    public CanvasGroup[] listCanvasGroups = null;

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void clickButton(Text textButton)
    {
        textButton = textButton.GetComponent<Text>();
        switch(textButton.text.ToLower())
        {
            case "options":
                //cams_manager.GetComponent<Groups_Manager>().openOrClosePanel(true, 1f, 2); 
                openOrClosePanel(true, 1f, 2);
                break;
            case "accept":
                //cams_manager.GetComponent<Groups_Manager>().openOrClosePanel(false, 0f, 2);
                openOrClosePanel(false, 0f, 2);
                break;
            default:
                Debug.Log(textButton.text.ToString());
                break;
        }
    }

    private void openOrClosePanel(bool status, float alphaPanel, int panelToShowOrNot)
    {
        //por cada elemento de la lista
        for(int i = 0; i < listCanvasGroups.Length; i++)
        {
            //si i = a panel a mostrar
            if(i == panelToShowOrNot)
            {
                //activar interacciones, alpah y bloquearraycasts
                listCanvasGroups[i].interactable = status;
                listCanvasGroups[i].alpha = alphaPanel;
                listCanvasGroups[i].blocksRaycasts = status;
            }
            else
            {
                //si no, evitar interacciones y raycasts
                listCanvasGroups[i].interactable = !status;
                //listCanvasGroups[i].alpha = 0.5f;
                listCanvasGroups[i].blocksRaycasts = !status;
            }
        }
        //solo para info
        if(status)
        {
            Debug.Log("abrir panel de audiosettings");
        }
        else
        {
            Debug.Log("cerrar panel de audiosettings");
        }
    }
}
