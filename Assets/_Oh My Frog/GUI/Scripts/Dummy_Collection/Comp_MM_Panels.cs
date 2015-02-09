using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Comp_MM_Panels : MonoBehaviour {

    public List<GameObject> panels = new List<GameObject>();

	// Use this for initialization
	void Start () {
        if(panels.Count != 0)
        {
            for(int i = 0; i < panels.Count; i++)
            {
                Debug.Log(panels[i].name);
            }
        }
        else
        {
            Debug.LogWarning("Falta indicar los paneles del canvas");
        }        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
