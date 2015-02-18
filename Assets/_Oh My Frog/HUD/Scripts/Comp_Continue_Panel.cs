using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Comp_Continue_Panel : MonoBehaviour {

    public Button button_consume_mangos;
    public Transform panel_Continue;

    void Awake() {
        panel_Continue.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if(ShopManager.CreateManager().MangosQuantity < 100) {
            button_consume_mangos.interactable = false;
            
        } else {
            button_consume_mangos.interactable = true;
        }
	}

    public void buttonEventClick() {
        Debug.Log(button_consume_mangos.GetComponentInChildren<Text>().text);
        ShopManager.CreateManager().MangosQuantity -= 100;
        panel_Continue.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void buyMangos() {
        ShopManager.CreateManager().MangosQuantity += 100;
        Debug.Log(ShopManager.CreateManager().MangosQuantity);
    }
}
