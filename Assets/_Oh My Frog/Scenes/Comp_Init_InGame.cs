using UnityEngine;
using System.Collections;

public class Comp_Init_InGame : MonoBehaviour
{
    public GameObject Connectivity_Prefab;

	void Awake()
    {
        Debug.Log("Inicializando escena INGAME...");
        GameObject go_Connectivity = GameObject.Find("Connectivity_GO");
        if (go_Connectivity == null)
        {
            go_Connectivity = (GameObject)Instantiate(Connectivity_Prefab, Vector3.zero, Quaternion.identity);
            ConnectivityManager.InitializeConnectivity();
        }

      
        // 3) INICIALIZAR LOS ASSETS DEL IAP
        //IAP_Assets.Initialize();
        // 4) INICIALIZAR EL MANAGER DE CONECTIVIDAD <GameState, Rankings, Logros>
        //ConnectivityManager.InitializeConnectivity();

        // 1) SHOP MANAGER
        ShopManager.CreateManager(); // --> modificar para que pille todos los VirtualGoods y los CurrencyPacks y los meta en listas con todos los datos.
       
        // 2) INVENTORY MANAGER
        InventoryManager.CreateManager(); // --> mod esto
        InventoryManager.Instance.LoadInGameItems(); // --> mod esto

        // 5) INICIALIZAR MANAGER DE IAP
        //IAPManager.CreateManager(); 
        // 6)


        GameLogicManager.CreateManager();
        EnvironmentManager.CreateManager();
        //Provisional
        EnvironmentManager.Instance.loadLevel("Beach");

        //inicializar pool de enemys_obstacles
        /*Obstacle_Enemys_Manager.Instance.Initialize();
        Debug.Log("ELEP - Obstacle_Enemys_Manager - initialized");*/
	}
}
