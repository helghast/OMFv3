using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Comp_Rotate_Kappa : MonoBehaviour {

    public Transform kappaTransform;
    public Text buttonText;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void rotateDirection() {
        switch(buttonText.text) {
            case "<":
                kappaTransform.Rotate(new Vector3(0, 5, 0), Space.Self);
                break;
            case ">":
                kappaTransform.Rotate(new Vector3(0, -5, 0), Space.Self);
                break;
        }
    }
}
