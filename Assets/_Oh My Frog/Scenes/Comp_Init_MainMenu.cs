using UnityEngine;
using System.Collections;

public class Comp_Init_MainMenu : MonoBehaviour
{
	void Awake ()
    {
        ConnectivityManager.InitializeConnectivity();

        // 1) SHOP MANAGER        
        ShopManager.CreateManager();
        // 2) INVENTORY MANAGER
        //InventoryManager.CreateManager();
        // 3) INICIALIZAR LOS ASSETS DEL IAP
        //IAP_Assets.Initialize();
        // 4) INICIALIZAR EL MANAGER DE CONECTIVIDAD <GameState, Rankings, Logros>
        //ConnectivityManager.InitiliazeCloudServices();
	}

    void Start()
    {
        // 1) INICIALIZAR MANAGER DE IAP
        //IAPManager.CreateManager();
    }
}
