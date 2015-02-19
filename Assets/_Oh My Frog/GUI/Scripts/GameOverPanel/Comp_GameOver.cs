using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Comp_GameOver : MonoBehaviour
{
    private bool isGameOverButtonClicked = false;

    public GameObject panelScores = null;
    public GameObject entryScorePrefab = null;
    public GameObject panelWhatToDo = null;
    public GameObject panelWTDTitle = null;
    public Text buttonClose = null;

    //temporales
    private int sumValues = 0;
    private float timer = 5f;

    //lista para guardar la cantidad de rows a generar
    private List<GameObject> entrys = null;
    //lista de ejemplo. En verdad se cogeran las puntuaciones de otro sitio (playerprefs?)
    public string[] scoreElements = new string[] { "Mangos", "Frogs", "Total Distance" };
    //los dictionarys no son serializables y por tanto no se ven en el editor, aunque sean publicos
    public Dictionary<string, int> mapScoreElements = null;

    void Awake()
    {
        entrys = new List<GameObject>();

        //si no se indican en el editor los Text y el Panel que los contiene, buscarlos a mano
        if(panelScores == null)
        {
            panelScores = GameObject.Find("Panel_Score_List");
        }
        if(panelWhatToDo == null)
        {
            panelWhatToDo = GameObject.Find("Panel_WhatToDo");
        }
        if(panelWTDTitle == null)
        {
            panelWTDTitle = GameObject.Find("Panel_WTD_Title");
        }

        //desactivar panelwhattodo para poderlo manejar al gusto
        panelWhatToDo.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //isgameoverbuttonclicked se pondra a true desde el init_go cuando se vuelva del ingame mode
        if(isGameOverButtonClicked == true)
        {
            timer = timer - Time.deltaTime;
            
            if(timer < 0f)
            {
                panelWhatToDo.SetActive(true);
                fillPaneltitle_WTD();
            }
            else
            {
                buttonClose.text = ((int) timer).ToString();
            }
        }
    }

    //para modificar el private bool desde otras clases
    public bool gameOverStatus {
        get {
            return isGameOverButtonClicked;
        }
        set {
            isGameOverButtonClicked = value;
        }
    }

    //cuando se clicka en el boton de prueba para ver el gameover panel
    public void showStructContent()
    {
        //activar animacion del panel
        
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<Animator>().Rebind();
        

        //rellenar panel
        fillMap();
        fillScorePanelMap();

        if(mapScoreElements.Count == 0)
        {
            Debug.Log("Mapa de scores vacio. Quizas deba indicar una lista de elementos de score");
        }

        isGameOverButtonClicked = true;
    }

    //cuando se clicka sobre cualquiera de los botones de close/return del panel gameover
    public void closePanel()
    {
        //destruir y/o poner a null objetos y listas y arrays. 
        //poner a valores iniciales los floats e ints.
        isGameOverButtonClicked = false;
        timer = 5f;
        sumValues = 0;
        foreach(GameObject item in entrys)
        {
            Destroy(item);
        }
        entrys.Clear();
        panelWhatToDo.SetActive(false);
    }

    //metodo llamado cuando se ha de mostrar el panel de scores.
    public void fillMap()
    {
        mapScoreElements = new Dictionary<string, int>();
        for(int i = 0; i < scoreElements.Length; i++)
        {
            mapScoreElements.Add(scoreElements[i], 100 * (i + 1));
        }
    }

    //metodo llamado cuando se ha de mostrar la tabla de escores. Se rehace cada vez que se abre dicho panel
    public void fillScorePanelMap()
    {
        Text[] tempTextArray = null;
        GameObject go = null;
        foreach(KeyValuePair<string, int> item in mapScoreElements)
        {
            go = (GameObject) Instantiate(entryScorePrefab);
            go.transform.SetParent(panelScores.transform, false);
            tempTextArray = go.GetComponentsInChildren<Text>();
            tempTextArray[0].name = item.Key;
            tempTextArray[0].GetComponent<Text>().text = item.Key;
            tempTextArray[1].name = item.Value.ToString();
            tempTextArray[1].GetComponent<Text>().text = item.Value.ToString();
            sumValues = sumValues + item.Value;
            entrys.Add(go);
        }

        //para sumar el total
        go = (GameObject) Instantiate(entryScorePrefab);
        go.transform.SetParent(panelScores.transform, false);
        tempTextArray = go.GetComponentsInChildren<Text>();
        tempTextArray[0].name = "Total";
        tempTextArray[0].GetComponent<Text>().text = "Total";
        tempTextArray[1].name = sumValues.ToString();
        tempTextArray[1].GetComponent<Text>().text = sumValues.ToString();
        tempTextArray[0].GetComponent<Text>().resizeTextForBestFit = true;
        tempTextArray[1].GetComponent<Text>().resizeTextForBestFit = true;
        //guardar total en la list
        entrys.Add(go);
    }

    //metodo para llenar el titulo del popup al cabo de xsegs
    public void fillPaneltitle_WTD()
    {
        //llenar tambien los text de la ventana que aparecera en xsegs
        //tambien tiene 2 texts
        Text[] tempTextArray = panelWTDTitle.GetComponentsInChildren<Text>();
        tempTextArray[0].GetComponent<Text>().text = "New Score";
        tempTextArray[1].GetComponent<Text>().text = sumValues.ToString();
    }
}
