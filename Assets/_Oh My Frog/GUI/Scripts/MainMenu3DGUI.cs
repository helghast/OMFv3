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

    public ButtonStateX2[] arrayButtons = new ButtonStateX2[] { new ButtonStateX2("playButton"), new ButtonStateX2("shopButton"), new ButtonStateX2("logrosButton"), new ButtonStateX2("rankingButton") };

    public LayerMask layerMask;
    private LoadLevel_GUI levelLoader;
    //objecto del canvas
    public GameObject canvasMain;

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
        //crear instancia para facebook
        if(!FB.IsInitialized)
        {
            //iniciar el FB.init() de facebook
            Comp_Facebook_Feed.Initialize().CallFBInit();
        }

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
	}

    public void commonRayCast(Ray ray, out RaycastHit hit)
    {
        Animator anim;
        GameObject go;
        if(Physics.Raycast(ray, out hit, 100, layerMask))
        {
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
                go = canvasMain.GetComponent<Comp_MM_Panels>().panels[1];
                go.SetActive(true);
                foreach(Transform item in go.GetComponentsInChildren<Transform>())
                {
                    if(item.name == "Panel_StoreMain")
                    {
                        anim = item.GetComponent<Animator>();
                        anim.enabled = true;
                        anim.Rebind();
                    }
                }
                Debug.Log(hcname);
            }
            else if(hcname == "fbcollider")
            {
                Debug.Log(hcname);

                //facebook methods
                clickOrTouchSocialFBButton();

                //activar aniimacion Shake panel
                anim = hit.collider.transform.GetComponentInParent<Animator>();
                anim.enabled = true;
                anim.Rebind();
            }
            else if(hcname == "logroscollider")
            {
                arrayButtons[2].setClickedButtonState(true);

                Debug.Log(hcname);
            }
            else if(hcname == "rankingcollider")
            {
                arrayButtons[3].setClickedButtonState(true);

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
