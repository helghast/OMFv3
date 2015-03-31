using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Achievements.Android;
//using Soomla.Store;

public class Comp_Debug : MonoBehaviour
{
    public bool EntradaTactil;
    public bool in_app_testing;
    public bool achievement_testing;
    public Transform player;
    private int incremental_counter = 5;
    private Comp_IAP_Materials comp_IAP_Materials;
    private const string PURCHASED = "(purchased)";
    private const string NOT_PURCHASED = "(not_purchased)";
    private string red_purchase_state;
    private string green_purchase_state;
    private string blue_purchase_state;
    
    private bool logged_state;
    private bool first_achievement_state;
    private bool debug_on;

    // FPS COUNTER
    private float fps;
    private int fps_list_index;
    private List<float> fps_list;

    private Queue<string> debug_info_queue;
    private List<string> debug_info_list;
    private byte[] aux_byteArray;

    void Start()
    {
        achievement_testing = false;
        in_app_testing = false;

        logged_state = false;
        first_achievement_state = false;

        red_purchase_state = NOT_PURCHASED;
        green_purchase_state = NOT_PURCHASED;
        blue_purchase_state = NOT_PURCHASED;

        //comp_IAP_Materials = GameObject.Find("IAP_Materials").GetComponent<Comp_IAP_Materials>();

        //redefineProductPurchaseState();

        fps_list = new List<float>(20);
        for (int i = 0; i < 20; ++i)
        {
            fps_list.Add(0);
            fps_list[i] = 0;
        }

        debug_info_queue = new Queue<string>();
        debug_info_list = new List<string>();

        StartCoroutine(recalcAvgFPSCouroutine());
        fps_list_index = 0;

        EntradaTactil = true;

        // RESETEAMOS MANGOS PARA TESTEAR COMPRAS
        //ConnectivityManager.EIAP.ResetPlayerMangos(0);
        // RESETEAMOS EL ESTADO DE RED MATERIAL PARA TESTEAR COMPRAS (Es un LifetimeGood, por eso lo reseteo al inicio cada vez)
        //ConnectivityManager.EIAP.VirtualGoods["Red_material"].ResetBalance(0);
    }

    // Esto es una guarrada, lo pongo solo para testeo. Cuando implementemos esto habrña que hacerlo bien.

    private void redefineProductPurchaseState()
    {
        if (IAPManager.Instance == null)
        {
            Debug.Log("NULLACO");
        }
        for (int i = 0; i < IAPManager.Instance.VirtualGoods.Count; ++i)
        {
            if (IAPManager.Instance.VirtualGoods[i].GetBalance() == 1)
            {
                IAPManager.Instance.VirtualGoods[i].purchased = true;
            }
        }
    }

    void Update()
    {
        fps_list[fps_list_index] = 1 / Time.deltaTime;
        fps_list_index++;
        if (fps_list_index == 20)
            fps_list_index = 0;
    }

    void OnGUI()
    {
        /*drawTestHUD();
        if (!debug_on)
            return;

        drawDebugMenuOptions();
        drawInAppPurchaseTest();
        drawSocialTest();
        drawKappaItemsTest();
        drawDebugInfo();*/
    }

    public void addDebugMessage(string msg)
    {
        if (debug_info_list.Count > 5)
        {
            debug_info_list.RemoveAt(0);
            debug_info_list.Add(msg);
        }
        else
        {
            debug_info_list.Add(msg);
        }
    }

    public void drawDebugInfo()
    {
        GUILayout.BeginArea(new Rect(10, Screen.height - 200, Screen.width - 20, 190));
        GUILayout.BeginVertical("box");
        GUILayout.Label("DEBUG");

        for (int i = 0; i < debug_info_list.Count; ++i)
        {
            GUILayout.Label("Debug: " + debug_info_list[i]);
            GUILayout.Space(1);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    public void drawTestHUD()
    {
        GUILayout.BeginArea(new Rect(10, Screen.height - 100, 160, 90));
        GUILayout.BeginVertical("box");
        GUILayout.Label("DEBUG DATA");

        GUILayout.Label("FPS: " + ((int)(fps)).ToString());
        GUILayout.Space(10);
        debug_on = GUILayout.Toggle(debug_on, "Enable debug");
        GUILayout.Space(10);

        GUILayout.EndVertical();
        GUILayout.EndArea();

    }
    cCloudGameState gameState;
    public void drawKappaItemsTest()
    {
        GUILayout.BeginArea(new Rect(10, 200, 200, 250));
        GUILayout.BeginVertical("box");
        GUILayout.Label("Showing Items TESTING");

        if (GUILayout.Button("Fill cloud Data"))
        {
            ConnectivityManager.FillGameState();
            aux_byteArray = ConnectivityManager.SerializeGameState(ConnectivityManager.ECloudGameState);
        }

        if (GUILayout.Button("Deserialize cloud Data"))
        {
            gameState = ConnectivityManager.DeserializeGameStateByteArray(aux_byteArray);
            addDebugMessage("total_mangos: " + gameState.total_mangos);
            addDebugMessage("total_meters: " + gameState.total_meters);
            addDebugMessage("total_frogs: " + gameState.total_frogs);
            addDebugMessage("total_cocktails: " + gameState.total_cocktails);
            addDebugMessage("object[1]: " + gameState.list_CloudItems[1].local_id);
            /*
            List<cUI_Item> aux = ShopManager.Instance.UI_List_All_Items;
            for (int i = 0; i < aux.Count; ++i)
            {
                Debug.Log("Loaded item: " + aux[i].name);
            }*/
        }

        if (GUILayout.Button("Save Game2Cloud"))
        {
            ConnectivityManager.SaveCurrentGameState();
        }

        /*
        if (GUILayout.Button("Show surfboard"))
        {
            InventoryManager.Instance.setKappaItemVisibleByName("prop_surfboard", true);
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Hide surfboard"))
        {
            InventoryManager.Instance.setKappaItemVisibleByName("prop_surfboard", false);
        }

        if (GUILayout.Button("Enable visualSet"))
        {
            GameLogicManager.Instance.EnableKappaVisualData();
        }

        if (GUILayout.Button("Disable visualSet"))
        {
            GameLogicManager.Instance.DisableKappaVisualData();
        }

        if (GUILayout.Button("List items"))
        {
            List<cUI_Item> aux = ShopManager.Instance.UI_List_All_Items;
            for (int i = 0; i < aux.Count; ++i)
            {
                Debug.Log("Loaded item: " + aux[i].name);
            }
        } */

        GUILayout.Space(5);
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    public void drawInAppPurchaseTest()
    {
        if (!in_app_testing)
            return;

        achievement_testing = false;

        GUILayout.BeginArea(new Rect(Screen.width - 210, 10, 200, Screen.height));
        GUILayout.BeginVertical("box");
        GUILayout.Label("InAppPurchase TESTING");

        //virtuales, ni comprados
        if (GUILayout.Button("Give Player 25 Mangos"))
        {
            Debug.Log("Current mangos: " + ConnectivityManager.EIAP.VirtualCurrencies["mango"].GetBalance());
            ConnectivityManager.EIAP.GivePlayerNMangos(25);
            Debug.Log("Current mangos: " + ConnectivityManager.EIAP.VirtualCurrencies["mango"].GetBalance());
        }

        //comprados
        if (GUILayout.Button("Buy 1000 Mangos"))
        {
            Debug.Log("Current mangos: " + ConnectivityManager.EIAP.VirtualCurrencies["mango"].GetBalance());
            ConnectivityManager.EIAP.BuyVirtual(ConnectivityManager.EIAP.VirtualCurrencyPacks["1000 Mangos"].ID);
            //
            Debug.Log("Current mangos: " + ConnectivityManager.EIAP.VirtualCurrencies["mango"].GetBalance());
        }

        if (GUILayout.Button("Buy 10000 Mangos"))
        {
            ConnectivityManager.EIAP.BuyVirtual(ConnectivityManager.EIAP.VirtualGoods["10000 Mangos"].ID);
        }

        if (GUILayout.Button("Red Purchase (25 mangos)"))
        {
            Debug.Log("Current mangos: " + ConnectivityManager.EIAP.VirtualCurrencies["mango"].GetBalance());
            ConnectivityManager.EIAP.BuyVirtual(ConnectivityManager.EIAP.VirtualGoods["Red_material"].ID);
            //
            Debug.Log("Current mangos: " + ConnectivityManager.EIAP.VirtualCurrencies["mango"].GetBalance());
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Green Purchase (" + ConnectivityManager.EIAP.VirtualGoods["Green_material"].purchased.ToString() + ")"))
        {
            ConnectivityManager.EIAP.BuyVirtual(ConnectivityManager.EIAP.VirtualGoods["Green_material"].ID);
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Blue Purchase (" + ConnectivityManager.EIAP.VirtualGoods["Blue_material"].purchased.ToString() + ")"))
        {
            ConnectivityManager.EIAP.BuyVirtual(ConnectivityManager.EIAP.VirtualGoods["Blue_material"].ID);
        }
        GUILayout.Space(5);
        GUILayout.Space(20);

        if (GUILayout.Button("Equip RED"))
        {
            bool canEquip = IAPManager.Instance.CheckItem(ConnectivityManager.EIAP.VirtualGoods["Red_material"].ID);
            if (canEquip)
            {
                player.renderer.material = comp_IAP_Materials.red_material;
            }
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Equip GREEN"))
        {
            bool canEquip = IAPManager.Instance.CheckItem(ConnectivityManager.EIAP.VirtualGoods["Green_material"].ID);
            if (canEquip)
            {
                player.renderer.material = comp_IAP_Materials.green_material;
            }
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Equip BLUE"))
        {
            bool canEquip = IAPManager.Instance.CheckItem(ConnectivityManager.EIAP.VirtualGoods["Blue_material"].ID);
            if (canEquip)
            {
                player.renderer.material = comp_IAP_Materials.blue_material;
            }
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Equip YELLOW"))
        {
            player.renderer.material = comp_IAP_Materials.yellow_material;
        }
        GUILayout.Space(5);

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    public void drawDebugMenuOptions()
    {
        GUILayout.BeginArea(new Rect(10, 110, 200, 300));
        GUILayout.BeginVertical("box");

        if (GUILayout.Button("Add coinGroup"))
        {
            GameLogicManager.Instance.SpawnCoinGroup("circle", GameLogicManager.Instance.GetPlayerPosition());
        }

        if (GUILayout.Button("Save data"))
        {
            GameLogicManager.Instance.saveMangosNow();
        }

        GUILayout.Space(10);

        EntradaTactil = GUILayout.Toggle(EntradaTactil, "Enable touch");
        GUILayout.Space(10);
        if (EntradaTactil)
        {
            achievement_testing = false;
            in_app_testing = true;
        }

        achievement_testing = GUILayout.Toggle(achievement_testing, "Achievements");
        if (achievement_testing)
        {
            in_app_testing = false;
        }

        in_app_testing = GUILayout.Toggle(in_app_testing, "InAppPurchase");
        if (in_app_testing)
        {
            achievement_testing = false;
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    public void drawSocialTest()
    {
        if (!achievement_testing)
            return;

        GUILayout.BeginArea(new Rect(Screen.width - 210, 10, 200, Screen.height));
        GUILayout.BeginVertical("box");
        GUILayout.Label("Achievement system TESTING");
        GUILayout.Space(10);
        
        if (GUILayout.Button("Log in"))
        {
            if (ConnectivityManager.SocialAuthenticate())
            {
                logged_state = true;
            }
        }
        GUILayout.Space(5);

        if (GUILayout.Button("First achievement"))
        {
            if (ConnectivityManager.ReportAchievement(Achievements_Android.ACHIEVEMENT_ONE, 100))
            {
                first_achievement_state = true;
            }
        }
        GUILayout.Space(5);

        if (GUILayout.Button(incremental_counter + " times for Inc.achievement!"))
        {
            if (ConnectivityManager.ReportIncrementalAchievement(Achievements_Android.ACHIEVEMENT_TWO_INCREMENTAL, 5))
            {
                --incremental_counter;
            }
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Third achievement"))
        {
            if (ConnectivityManager.ReportAchievement(Achievements_Android.ACHIEVEMENT_THREE, 100))
            {
                //first_achievement_state = true;
            }
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Fourth achievement"))
        {
            if (ConnectivityManager.ReportAchievement(Achievements_Android.ACHIEVEMENT_FOUR, 100))
            {
                //first_achievement_state = true;
            }
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Fifth achievement"))
        {
            if (ConnectivityManager.ReportAchievement(Achievements_Android.ACHIEVEMENT_FIVE, 100))
            {
                //first_achievement_state = true;
            }
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Post 1000 points to LB"))
        {
            ConnectivityManager.ReportScore(1000, Achievements_Android.SCORE_BLACKBOARD);
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Show Achievements"))
        {
            ConnectivityManager.ShowAchievements();
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Show Scores"))
        {
            ConnectivityManager.ShowLeaderBoard();
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Get coins from DB"))
        {
            ConnectivityManager.SocialAuthenticate();
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Add 30 coins"))
        {
            ConnectivityManager.SocialAuthenticate();
        }
        GUILayout.Space(5);


        if (GUILayout.Button("Add 50 coins"))
        {
            ConnectivityManager.SocialAuthenticate();
        }
        GUILayout.Space(5);

        if (GUILayout.Button("Logout"))
        {
            ConnectivityManager.SocialAuthenticate();
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void recalcAvgFPS()
    {
        int i = 0;
        float fps_sum = 0;
        for (i = 0; i < fps_list.Count; ++i)
        {
            if (fps_list[i] == 0)
                break;

            fps_sum += fps_list[i];

        }
        fps = fps_sum / (float)i;
    }

    private IEnumerator recalcAvgFPSCouroutine()
    {
        yield return new WaitForSeconds(2);
        recalcAvgFPS();
        StartCoroutine(recalcAvgFPSCouroutine());
    }
}
