using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

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
        /*switch(coinType) {
            case "Mangos":
                if(ConnectivityManager.EIAP.BuyVirtual(ConnectivityManager.EIAP.VirtualCurrencyPacks["1000 Mangos"].ID)) {
                    Debug.Log("Transaccion realizada");
                    ShopManager.CreateManager().MangosQuantity += coinValue;
                    currentCoinQuantity.text = ShopManager.CreateManager().MangosQuantity.ToString();
                } else {
                    Debug.Log("Transaccion no realizada");
                }               
                break;
            case "Cocteles":
                ShopManager.CreateManager().CoctelesQuantity += coinValue;
                currentCoinQuantity.text = ShopManager.CreateManager().CoctelesQuantity.ToString();
                break;
        }*/
        string namepack = coinValue + "_" + coinType;

        if(ConnectivityManager.EIAP.BuyVirtual(ConnectivityManager.EIAP.VirtualCurrencyPacks[namepack].ID)) {
            Debug.Log("Transaccion realizada");
            ShopManager.CreateManager().MangosQuantity += coinValue;
            currentCoinQuantity.text = ShopManager.CreateManager().MangosQuantity.ToString();
        } else {
            Debug.Log("Transaccion no realizada");
        }
        Debug.Log(namepack);
        
    }
}
