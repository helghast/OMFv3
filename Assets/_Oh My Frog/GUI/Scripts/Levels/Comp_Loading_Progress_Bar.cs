using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Comp_Loading_Progress_Bar : MonoBehaviour {

    public Slider loadingProgressBar;

    void Awake()
    {
        if(loadingProgressBar == null)
        {
            loadingProgressBar = GameObject.Find("LoadingProgressBar").GetComponent<Slider>();
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        loadingProgressBar.value = Mathf.MoveTowards(loadingProgressBar.value, 100.0f, 0.5f);
	}
}
