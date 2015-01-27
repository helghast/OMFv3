using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCount : MonoBehaviour {

    public Text text;
    public float setSeconds;

	// Use this for initialization
	void Start () {
        if(text == null)
        {
            Debug.LogError("falta el text mostrar el tiempo en loading");
        }
        else
        {
            InvokeRepeating("countDownSeconds", 5, 1);
        }
	}
	
	// Update is called once per frame
	void Update () {
              
	}

    private void countDownSeconds()
    {
        text.text = "Loading...";    
    }
}
