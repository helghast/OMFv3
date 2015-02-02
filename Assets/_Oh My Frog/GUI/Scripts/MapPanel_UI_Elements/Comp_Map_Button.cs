using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Comp_Map_Button : MonoBehaviour {

    public Text textToShow;
    public Image imageStage;
    public Button stageStartButton;

	// Use this for initialization
	void Start () {
        //al inicio el boton que permite iniciar un stage no es clickable
        stageStartButton.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

    //al clickar sobre un stage del mapa, activar el boton de inicar el stage
    public void getDataButtons(Text textbutton)
    {
        textToShow.text = textbutton.text + "\nName stage\nBoss:X\nBestScore:1000";
        stageStartButton.interactable = true;
        stageStartButton.GetComponentInChildren<Text>().text = textbutton.text;

        //activar el componente para animar el color del boton
        stageStartButton.GetComponent<Animator>().enabled = true;
        /*switch(textbutton.text)
        {
            case "Stage1":
                enableStartStageButton("InGame");
                break;
            case "Stage2":
            case "Stage3":
            case "Stage4":
            case "Stage5":
                enableStartStageButton(textbutton.text);
                break;
        }*/
        enableStartStageButton(textbutton.text);
    }

    public void enableStartStageButton(string gameplay)
    {
        //UnityAction action = () => { LoadLevel_GUI.loadLevel(gameplay); };

        //stageStartButton.onClick.AddListener(() => LoadLevel_GUI.loadLevel(gameplay));
        //stageStartButton.onClick.AddListener(action);
        //stageStartButton.onClick.RemoveListener(action);
        stageStartButton.GetComponentInChildren<Text>().text = gameplay;
    }
}
