﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Soomla;
using Soomla.Profile;

public class MainMenu3DGUI : MonoBehaviour {
    [System.Serializable]
    public struct ButtonStateX2 {
        public Transform clicked;
        public Transform unclicked;
        public string name;

        //para usar el constructor con parametros hay que hacer una llamada al constructor por defecto con this
        public ButtonStateX2(string name)
            : this() {
            this.name = name;
        }

        public void setClickedButtonState(bool state) {
            clicked.gameObject.SetActive(state);
            unclicked.gameObject.SetActive(!state);
        }
    }

    public ButtonStateX2[] arrayButtons = new ButtonStateX2[] { new ButtonStateX2("playButton"), new ButtonStateX2("shopButton"), new ButtonStateX2("logrosButton"), new ButtonStateX2("optionsButton") };

    public LayerMask layerMask;
    //private LoadLevel_GUI levelLoader;
    //objecto del canvas
    public GameObject canvasMain;
    public GameObject panelOptions;
    public GameObject newShopPanel;
    public Text textCoins;
    private float temptime = 0f;

    //escenario y kappa y dummyspoints
    public Transform transform_kappa;
    public Transform transform_env;
    public Transform point_OutEnv;
    public Transform point_InEnv;
    public Transform point_Kappa_Inside_Env;
    public Transform point_Kappa_Out_Env;

    //booleano que nos sirve para saber si hay un UI delante del escenario 3D o no. Para desactivar raycast o no.
    public bool UI2DStatus = false;

    //before start
    void Awake() {
        
    }

    // Use this for initialization
    void Start() {
        //importante tener el canvas main. buscarlo si nos olvidamos indicarlo en el editor
        if(canvasMain == null) {
            canvasMain = GameObject.Find("Canvas-MainMenu");
        }
        //buscar el panel de opciones si no se ha indicado
        if(panelOptions == null) {
            panelOptions = GameObject.Find("PanelAudioOptions");
        }
        //buscar el panel de la nueva shop si no se ha indicado
        if(newShopPanel == null) {
            newShopPanel = GameObject.Find("New_Shop_Panel");
        }
        //crear instancia para facebook
        if(!FB.IsInitialized) {
            //iniciar el FB.init() de facebook
            Comp_Facebook_Feed.Initialize().CallFBInit();
        }
        //poner a inactivos los siguientes paneles para evitar que se vean dentro de la camara en diferentes resoluciones
        panelOptions.SetActive(false);
        newShopPanel.SetActive(false);

        //levelLoader = GetComponent<LoadLevel_GUI>();

        //poner botones clickados a false
        for(int i = 0; i < arrayButtons.Length; i++) {
            arrayButtons[i].setClickedButtonState(false);
        }
    }

    // Update is called once per frame
    void Update() {
        if(Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began) {
                //no hay ningun panel UI delante del escenario 3D? si es que no, pues lanzar Ray
                if(!UI2DStatus) {
                    Ray ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, transform.position.z));

                    RaycastHit hit;
                    commonRayCast(ray, out hit);
                }

            }
        } else {
            if(Input.GetMouseButtonDown(0)) {
                //no hay ningun panel UI delante del escenario 3D? si es que no, pues lanzar Ray
                if(!UI2DStatus) {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit;
                    Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 2);

                    commonRayCast(ray, out hit);
                }
            }
        }

        //forma rapida de desactivar los botones despues de ser hitteados
        if(temptime > 0) {
            temptime = temptime - Time.deltaTime;
        } else {
            for(int i = 0; i < arrayButtons.Length; i++) {
                arrayButtons[i].setClickedButtonState(false);
            }
        }
    }

    public void commonRayCast(Ray ray, out RaycastHit hit) {
        Animator anim;
        GameObject go;
        if(Physics.Raycast(ray, out hit, 100, layerMask)) {
            temptime = 1f;
            string hcname = hit.collider.name.ToLower();
            if(hcname == "playcollider") {
                arrayButtons[0].setClickedButtonState(true);

                //evitar usar los ray
                UI2DStatus = true;

                //animar entrada panel seleccion mapa
                go = canvasMain.GetComponent<Comp_MM_Panels>().panels[2];
                go.SetActive(true);
                anim = go.GetComponent<Animator>();
                anim.enabled = true;
                anim.Rebind();
                //levelLoader.loadLevel("InGame");
            } else if(hcname == "shopcollider") {
                arrayButtons[1].setClickedButtonState(true);

                //evitar usar los ray
                UI2DStatus = true;

                //desplazar escenario fuera de la vista de la camara y mover capa a la region del panel visible
                moveAllForShop();

                //buscar, activar y animar shop panel                
                newShopPanel.SetActive(true);
                newShopPanel.GetComponent<Animator>().enabled = true;
                newShopPanel.GetComponent<Animator>().Rebind();
                //newShopPanel.GetComponentInChildren<Comp_RenderCamOnUI>().kappa_camera.gameObject.SetActive(true);

                //mostrar coins actuales disponibles
                textCoins.text = ShopManager.CreateManager().MangosQuantity.ToString();

                //la lista de items por defecto que se carga al abrir la shop es Items.
                GameObject.Find("New_Shop_Panel").GetComponent<CreateScrollableList>().populateList("Items");
                Debug.Log(hcname);
            } else if(hcname == "fbcollider") {
                Debug.Log(hcname);

                //evitar usar los ray
                //UI2DStatus = true;

                //activar aniimacion Shake panel
                anim = hit.collider.transform.GetComponentInParent<Animator>();
                anim.enabled = true;
                anim.Rebind();

                //esperar 1seg antes de abrir el panel de facebook
                Invoke("clickOrTouchSocialFBButton", 1f);
            } else if(hcname == "logroscollider") {
                arrayButtons[2].setClickedButtonState(true);

                //evitar usar los ray
                //UI2DStatus = true;

                Debug.Log(hcname);
            } else if(hcname == "optionscollider") {
                arrayButtons[3].setClickedButtonState(true);

                //evitar usar los ray
                UI2DStatus = true;

                panelOptions.SetActive(true);
                Debug.Log(hcname);
            }
        }
    }

    public void clickOrTouchSocialFBButton() {
        //usar soomla para gestionar la conexion a FB
        //tryLoginFB();
        makeFBFeed();
        
        //logear y crear feed
        //Comp_Facebook_Feed.Initialize().CallFBLogin();
    }

    //para usar desde los botones close de los paneles UI, por ejemplo.
    public void statusRayCast(bool status) {
        UI2DStatus = status;
    }

    public void moveAllForShop() {
        transform_kappa.position = point_Kappa_Out_Env.position;
        transform_env.position = point_OutEnv.position;
    }

    public void returnAllToEnvPosition() {
        transform_kappa.position = new Vector3(-3.897404f, 0.07999998f, 4.144251f);
        transform_kappa.FindChild("KAPPA_RIG").rotation = Quaternion.Euler(270,180,0);
        transform_env.position = point_InEnv.position;
    }

    //intenta logear a facebook mediante soomla
    public void tryLoginFB() {
        if(!isloggedFB()) {
            SoomlaProfile.Login(Provider.FACEBOOK);
        } else {
            Debug.LogWarning("Ya esta logeado en FB");
        }        
    }

    //intenta deslogear de facebook mediante soomla
    public void tryLogoutFB() {
        if(isloggedFB()) {
            SoomlaProfile.Logout(Provider.FACEBOOK);
        } else {
            Debug.LogWarning("No esta logeado en FB");
        }
    }

    //comprueba si esta logeado en facebook mediante soomla
    public bool isloggedFB() {
        return SoomlaProfile.IsLoggedIn(Provider.FACEBOOK);
    }

    //intenta crear un feed/mensaje en el muro del usuario logeado en facebook, usando soomla
    public void makeFBFeed() {
        if(isloggedFB()) {
            SoomlaProfile.UpdateStory(
                Provider.FACEBOOK,
                "Check out this great game: OMF",
                "OMF: SurfRescue!",
                "OMF is amazing!",
                "omf game from electroplasmatic games",
                "https://www.facebook.com/ohmyfrog.surfrescue",
                "https://fbcdn-profile-a.akamaihd.net/hprofile-ak-xap1/v/t1.0-1/p160x160/10665697_304545143067074_11542050484542634_n.jpg?oh=00b8a665b3f03bd5f4b2a00ab4c2bed2&oe=554BBF35&__gda__=1435501571_ea7f4a4e8eeeb811ceff4a73a31ce8e1",
                string.Empty,
                null);
        } else {
            Debug.LogWarning("No esta logeado en FB");
            tryLoginFB();
        }
    }
}
