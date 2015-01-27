using UnityEngine;
using System.Collections;

public class Cameras_Manager : MonoBehaviour {

    public Camera[] cameras;

    //antes del start
    void Awake()
    {
        if(cameras.Length == 0)
        {
            Debug.LogError("array vacio de camaras");
        }
        else
        {
            cameras[0].enabled = true;
        }
    }

	// Use this for initialization
	void Start () {
               
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setup_Cameras()
    {
        cameras[0].enabled = !cameras[0].enabled;
    }
}
