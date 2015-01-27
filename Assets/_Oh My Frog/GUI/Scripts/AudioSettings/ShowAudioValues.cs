using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowAudioValues : MonoBehaviour {

    public Text textbox = null;
    public Slider slider = null;

    void Awake()
    {
       
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(slider != null && textbox != null)
        {
            showValue();
        }
        else
        {
            Debug.LogError("Falta un slider o un textbox");
        }
	}

    private void showValue(){
        textbox.text = slider.value.ToString();
    }
}
