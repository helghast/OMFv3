using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PurchaseCoins : MonoBehaviour {

    public int coinValue = 0;
    public string coinType;
    public Text currentCoinQuantity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void comprar() {
        switch(coinType) {
            case "Mangos":
                ShopManager.CreateManager().MangosQuantity += coinValue;
                currentCoinQuantity.text = ShopManager.CreateManager().MangosQuantity.ToString();
                break;
            case "Cocteles":
                /*ShopManager.CreateManager().CoctelesQuantity += coinValue;
                currentCoinQuantity.text = ShopManager.CreateManager().CoctelesQuantity.ToString();*/
                break;
        }
        Debug.Log(coinValue);
        
    }
}
