using UnityEngine;
using System.Collections;

public class Comp_Kappa_Skins : MonoBehaviour {

    public GameObject[] arraySkins;

    void Awake() {
        for(int i = 0; i < arraySkins.Length; i++) {
            arraySkins[i].SetActive(false);
        }   
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
