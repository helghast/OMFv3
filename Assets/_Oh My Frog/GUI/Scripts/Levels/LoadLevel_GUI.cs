using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadLevel_GUI : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void loadLevel(string levelName)
    {
        if(levelName.Equals("InGame"))
        {
            //guardar en playerprefs el nombre final de la scena
            PlayerPrefs.SetString("level", levelName);
            //por defecto se abre el loading scene
            Application.LoadLevel("Loading");
        }
        /*else
        {
            //Application.LoadLevel(levelName);
        }   */     
    }

    public void loadLevel(Text text)
    {
        loadLevel(text.text);
    }
}
