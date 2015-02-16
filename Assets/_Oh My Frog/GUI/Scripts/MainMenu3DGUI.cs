using UnityEngine;
using System.Collections;
using System;

public class MainMenu3DGUI : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonStateX2
    {
        public Transform clicked;
        public Transform unclicked;
        public string name;

        //para usar el constructor con parametros hay que hacer una llamada al constructor por defecto con this
        public ButtonStateX2(string name) : this()
        {
            this.name = name;
        }

        public void setClickedButtonState(bool state)
        {
            clicked.gameObject.SetActive(state);
            unclicked.gameObject.SetActive(!state);
        }
    }

    public ButtonStateX2[] arrayButtons = new ButtonStateX2[] { new ButtonStateX2("playButton"), new ButtonStateX2("shopButton"), new ButtonStateX2("logrosButton"), new ButtonStateX2("optionsButton") };

    public LayerMask layerMask;
    private LoadLevel_GUI levelLoader;
    //objecto del canvas
    public GameObject canvasMain;
    public GameObject panelOptions;
    public GameObject newShopPanel;
    private float temptime = 0f;

    //before start
    void Awake()
    {

    }

	// Use this for initialization
	void Start ()
    {
        //importante tener el canvas main. buscarlo si nos olvidamos indicarlo en el editor
        if(canvasMain == null)
        {
            canvasMain = GameObject.Find("Canvas-MainMenu");
        }
        //buscar el panel de opciones si no se ha indicado
        if(panelOptions == null)
        {
            panelOptions = GameObject.Find("PanelAudioOptions");            
        }
        //buscar el panel de la nueva shop si no se ha indicado
        if(newShopPanel == null)
        {
            newShopPanel = GameObject.Find("New_Shop_Panel");
        }
        //crear instancia para facebook
        if(!FB.IsInitialized)
        {
            //iniciar el FB.init() de facebook
            Comp_Facebook_Feed.Initialize().CallFBInit();
        }
        //poner a inactivos los siguientes paneles para evitar que se vean dentro de la camara en diferentes resoluciones
        panelOptions.SetActive(false);
        newShopPanel.SetActive(false);

        levelLoader = GetComponent<LoadLevel_GUI>();

        //poner botones clickados a false
        for(int i = 0; i < arrayButtons.Length; i++)
        {
            arrayButtons[i].setClickedButtonState(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, transform.position.z));

                RaycastHit hit;
                commonRayCast(ray, out hit);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 2);

                commonRayCast(ray, out hit);
            }
        }
        
        //forma rapida de desactivar los botones despues de ser hitteados
        if(temptime > 0)
        {
            temptime = temptime - Time.deltaTime;
        }
        else
        {
            for(int i = 0; i < arrayButtons.Length; i++)
            {
                arrayButtons[i].setClickedButtonState(false);
            }
        }
	}

    public void commonRayCast(Ray ray, out RaycastHit hit)
    {
        Animator anim;
        GameObject go;
        if(Physics.Raycast(ray, out hit, 100, layerMask))
        {
            temptime = 1f;
            string hcname = hit.collider.name.ToLower();
            if(hcname == "playcollider")
            {
                arrayButtons[0].setClickedButtonState(true);

                //animar entrada panel seleccion mapa
                go = canvasMain.GetComponent<Comp_MM_Panels>().panels[2];
                go.SetActive(true);
                anim = go.GetComponent<Animator>();
                anim.enabled = true;
                anim.Rebind();
                //levelLoader.loadLevel("InGame");
            }
            else if(hcname == "shopcollider")
            {
                arrayButtons[1].setClickedButtonState(true);

                //buscar, activar y animar shop panel                
                newShopPanel.SetActive(true);
                newShopPanel.GetComponent<Animator>().enabled = true;
                newShopPanel.GetComponent<Animator>().Rebind();
                //go = canvasMain.GetComponent<Comp_MM_Panels>().panels[1];
                //go.SetActive(true);
                /*foreach(Transform item in go.GetComponentsInChildren<Transform>())
                {
                    if(item.name == "Panel_StoreMain")
                    {
                        anim = item.GetComponent<Animator>();
                        anim.enabled = true;
                        anim.Rebind();
                    }
                }*/
                //la lista de items por defecto que se carga al abrir la shop es Items.
                GameObject.Find("New_Shop_Panel").GetComponent<CreateScrollableList>().populateList("Items");
                Debug.Log(hcname);
            }
            else if(hcname == "fbcollider")
            {
                Debug.Log(hcname);                

                //activar aniimacion Shake panel
                anim = hit.collider.transform.GetComponentInParent<Animator>();
                anim.enabled = true;
                anim.Rebind();                

                //esperar 1seg antes de abrir el panel de facebook
                Invoke("clickOrTouchSocialFBButton", 1f);
            }
            else if(hcname == "logroscollider")
            {
                arrayButtons[2].setClickedButtonState(true);

                Debug.Log(hcname);
            }
            else if(hcname == "optionscollider")
            {
                arrayButtons[3].setClickedButtonState(true);
                panelOptions.SetActive(true);
                Debug.Log(hcname);
            }
        }
    }

    public void clickOrTouchSocialFBButton()
    {
        //logear y crear feed
        Comp_Facebook_Feed.Initialize().CallFBLogin();
    }    
}
