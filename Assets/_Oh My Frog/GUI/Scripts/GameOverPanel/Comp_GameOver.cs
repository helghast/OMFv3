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
    private Text[] tempTextArray = null;
    private GameObject go = null;
    private GameObject total = null;
    private int sumValues = 0;
    private float timer = 5f;

    public string[] scoreElements = new string[] { "Mangos", "Frogs", "Total Distance" };
    //los dictionarys no son serializables y por tanto no se ven en el editor, aunque sean publicos
    //public Dictionary<string, int> mapScoreElements = new Dictionary<string, int>();

    //las structs si se ven en el editor si se serializan.
    [System.Serializable]
    public struct NamedScore
    {
        //public para que se vean en el editor
        public string skey;
        public int svalue;
        //constructor de la struct
        public NamedScore(string k, int v)
        {
            skey = k;
            svalue = v;
        }

        //getter
        public string Key
        {
            get { return skey; }
            set { skey = value; }
        }
        //setter
        public int Value
        {
            get { return svalue; }
            set { svalue = value; }
        }
    }
    public NamedScore[] structScoreElements = null;

    void Awake()
    {
        structScoreElements = new NamedScore[scoreElements.Length];
        //llenar la struct de puntuaciones
        //fillStruct();

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
        fillStruct();
        fillScorePanel();
    }

    // Update is called once per frame
    void Update()
    {
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

    //cuando se clicka en el boton de prueba para ver el gameover panel
    public void showStructContent()
    {
        if(structScoreElements.Length == 0)
        {
            Debug.Log("Vacio");
        }
        else
        {
            foreach(NamedScore item in structScoreElements)
            {
                Debug.Log(item.Key + " " + item.Value);
            }
        }
        isGameOverButtonClicked = true;
    }

    //cuando se clicka sobre cualquiera de los botones de close/return del panel gameover
    public void closePanel()
    {
        isGameOverButtonClicked = false;
        timer = 5f;
        panelWhatToDo.SetActive(false);
    }

    public void fillScorePanel()
    {
        //por la cantidad de elementos de la struct
        for(int i = 0; i < structScoreElements.Length; i++)
        {
            //instanciamos a partir del prefab
            go = (GameObject) Instantiate(entryScorePrefab);
            //colgarlos del panel padre a rellenar
            go.transform.SetParent(panelScores.transform, false);

            tempTextArray = go.GetComponentsInChildren<Text>();
            //solo hay 2, el nombre(0) y el value(0)
            tempTextArray[0].name = structScoreElements[i].Key;
            tempTextArray[0].GetComponent<Text>().text = structScoreElements[i].Key;
            tempTextArray[1].name = structScoreElements[i].Value.ToString();
            tempTextArray[1].GetComponent<Text>().text = structScoreElements[i].Value.ToString();
            sumValues = sumValues + structScoreElements[i].Value;
        }
        //para sumar el total
        total = (GameObject) Instantiate(entryScorePrefab);
        total.transform.SetParent(panelScores.transform, false);
        tempTextArray = total.GetComponentsInChildren<Text>();
        tempTextArray[0].name = "Total";
        tempTextArray[0].GetComponent<Text>().text = "Total";
        tempTextArray[1].name = sumValues.ToString();
        tempTextArray[1].GetComponent<Text>().text = sumValues.ToString();
        tempTextArray[0].GetComponent<Text>().resizeTextForBestFit = true;
        tempTextArray[1].GetComponent<Text>().resizeTextForBestFit = true;
    }

    public void fillStruct()
    {
        for(int i = 0; i < structScoreElements.Length; i++)
        {
            structScoreElements[i] = new NamedScore(scoreElements[i], 100 * (i + 1));
        }
    }

    public void fillPaneltitle_WTD()
    {
        //llenar tambien los text de la ventana que aparecera en xsegs
        //tambien tiene 2 texts
        tempTextArray = panelWTDTitle.GetComponentsInChildren<Text>();
        tempTextArray[0].GetComponent<Text>().text = "New Score";
        tempTextArray[1].GetComponent<Text>().text = sumValues.ToString();
    }
}
