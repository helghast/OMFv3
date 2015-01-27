using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IndividualItem : MonoBehaviour {

	private Button itemButton;
    private Text title;

	// Use this for initialization
    void Start()
    {
        itemButton = gameObject.GetComponentInChildren<Button>();
        title = gameObject.GetComponentInChildren<Text>();
        if(itemButton == null || title == null)
        {
            Debug.LogError("null button o null texttitle");
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
