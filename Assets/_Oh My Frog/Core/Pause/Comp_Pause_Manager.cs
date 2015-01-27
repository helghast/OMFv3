using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Comp_Pause_Manager : MonoBehaviour {

    private bool isPaused;
    public Transform panel_Pause_Menu_Transform;

    void Awake()
    {
        isPaused = false;
        //si no le indicamos un canvaspanel, que lo busque.
        if(panel_Pause_Menu_Transform == null)
        {
            panel_Pause_Menu_Transform = GameObject.Find("Panel_Pause_Menu").GetComponent<Transform>();
        }
        panel_Pause_Menu_Transform.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void change_Pause_Status()
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        panel_Pause_Menu_Transform.gameObject.SetActive(isPaused);
    }

    public void retryLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
        change_Pause_Status();
    }

    public void returnMainMenu()
    {
        //volver al mainmenu da problemas pues parece que no vuelve a llamar a algunos objetos necesarios
        Application.LoadLevel("MainMenu");
    }
}
