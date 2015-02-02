using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Comp_Stage_Button_Status : MonoBehaviour {

    public bool statusStage;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Button>().interactable = statusStage;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
