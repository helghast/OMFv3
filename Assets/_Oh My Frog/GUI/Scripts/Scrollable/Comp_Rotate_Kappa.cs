using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Comp_Rotate_Kappa : MonoBehaviour, IEventSystemHandler {

    public Transform kappaTransform;
    public Text buttonText;
    public int direction;
    private bool rotate;
    private float maxSpeed = 10f;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if(rotate) {
            rotateDirection();
        }
	}

    public void rotateDirection() {
        kappaTransform.RotateAround(kappaTransform.transform.position, Vector3.up, (maxSpeed * direction));
    }

    public void pressed(BaseEventData eventData) {
        rotate = true;
    }

    public void notPressed(BaseEventData eventData) {
        rotate = false;
    }
}
