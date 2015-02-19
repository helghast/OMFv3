using UnityEngine;
using System.Collections;

public class Comp_Init_MainMenu : MonoBehaviour
{
    public Transform panel_GameOver;

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

        //mirar si se ha vuelto del modo ingame y se debe mostrar el GameOver panel.
        checkGameOver();        
	}

    void Start()
    {
        // 1) INICIALIZAR MANAGER DE IAP
        //IAPManager.CreateManager();
    }

    public void checkGameOver() {
        if(PlayerPrefs.HasKey("gameover")) {
            //Debug.Log(PlayerPrefs.GetString("gameover"));
            panel_GameOver.gameObject.SetActive(true);
            panel_GameOver.gameObject.GetComponent<Comp_GameOver>().gameOverStatus = true;
            panel_GameOver.gameObject.GetComponent<Comp_GameOver>().showStructContent();
            //justo despues de cargar el panel gameover, borrar la key gameover del playerprefs, para evitar que siga existiendo al reiniciar el play en unity.
            PlayerPrefs.DeleteKey("gameover");
        }
    }
}
