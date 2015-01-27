using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using assets._Oh_My_Frog.XMLParser;
using System.IO;

public partial class GameLogicManager
{
    //-----------------------------------------------
    //  STATIC ATRIBUTES
    //-----------------------------------------------
    private static GameLogicManager instance;
    public static GameLogicManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static int coins;
    private static int frogs;
    private static int papayas;
    private static int meters;
    private static float float_meters;
    private static float game_speed;
    private static float meters_multiplier;

    private const string DEFAULT_SURFBOARD = "prop_surfboard";
    private const string DEFAULT_HAT = "prop_Pirata_hat";

    //-----------------------------------------------
    //  NON-STATIC ATRIBUTES
    //-----------------------------------------------
    public sKappaVisualData KappaVisualData;
    private Comp_Shop comp_shop;
    private Comp_GameLogic_Manager comp_gameLogic;
    private Comp_UI_Counter comp_meter_counter;
    private Comp_Coin_Manager comp_Coin_Manager;
    private Comp_Frog_Manager comp_Frog_Manager;
    private Comp_Kappa_Controller comp_Kappa_Controller;

    //-----------------------------------------------
    //  CONSTRUCTOR
    //-----------------------------------------------
    private GameLogicManager()
    {
        frogs = 0;
        meters = 0;
        coins = 0;
    }

    //-----------------------------------------------
    //  STATIC METHODS
    //-----------------------------------------------
    public static void CreateManager()
    {
        if (instance == null)
        {
            instance = new GameLogicManager();
            Instance.Initialize();
        }
    }
   
    public void Initialize()
    {
        meters = 0;
        comp_gameLogic = GameObject.Find("GameLogic_Manager").GetComponent<Comp_GameLogic_Manager>();
        comp_meter_counter = GameObject.Find("UI_Meter_Counter").GetComponent<Comp_UI_Counter>();

        KappaVisualData.Surfboard = InventoryManager.Instance.getKappaItemByName(DEFAULT_SURFBOARD);
        KappaVisualData.DressSet.upperItem = InventoryManager.Instance.getKappaItemByName(DEFAULT_HAT);

        comp_Coin_Manager = GameObject.Find("GameLogic_Manager").GetComponent<Comp_Coin_Manager>();
        comp_Frog_Manager = GameObject.Find("GameLogic_Manager").GetComponent<Comp_Frog_Manager>();

        comp_Kappa_Controller = GameObject.Find("Player").GetComponent<Comp_Kappa_Controller>();

        Debug.Log("Inicializado GameManager");
        
        // Load Local Data
        loadMangos();
    }

    //-----------------------------------------------
    //  PROPERTIES
    //-----------------------------------------------
    public int Coins { get { return coins; }}
    public int Meters { get { return meters; }}
    public float MetersMultiplier
    {
        get { return meters_multiplier; }
        set { meters_multiplier = value; }
    }

    public float GameSpeed
    {
        get { return game_speed; }
        set { game_speed = value; }
    }

    //-----------------------------------------------
    //  METHODS
    //-----------------------------------------------
    public void SetCoins(int new_coins)
    { 
        coins = new_coins;
        comp_gameLogic.updateMangoCounter(coins);
    }
    public void AddCoins(int coins_to_add)
    { 
        coins += coins_to_add;
        comp_gameLogic.updateMangoCounter(coins);
    }

    public void SetFrogs(int new_frogs)
    {
        frogs = new_frogs;
        comp_gameLogic.updateFrogCounter(frogs);
    }
    public void AddFrogs(int frogs_to_add)
    {
        frogs += frogs_to_add;
        comp_gameLogic.updateFrogCounter(frogs);
    }

    public void SetPapayas(int new_papayas)
    {
        papayas = new_papayas;
        comp_gameLogic.updateMangoCounter(papayas);
    }
    public void AddPapayas(int papayas_to_add)
    {
        papayas += papayas_to_add;
        comp_gameLogic.updateFrogCounter(papayas);
    }

    public void SetMetersMultiplier(float new_meters_multiplier) { meters_multiplier = new_meters_multiplier; }

    public void AddMeters(float meters_to_add)
    {
        float_meters += meters_to_add * GameSpeed * MetersMultiplier;
        meters = (int)float_meters;
        comp_meter_counter.setCounter(meters);
    }

    public void SetMeters(int new_meters)
    {
        meters = new_meters;
        comp_meter_counter.setCounter(new_meters);
    }

    public void SetCompShop(Comp_Shop _comp_shop) { comp_shop = _comp_shop;}

    public void EnableKappaVisualData()
    {
        KappaVisualData.Surfboard.renderer.enabled = true;
        KappaVisualData.DressSet.upperItem.renderer.enabled = true;
    }

    public void DisableKappaVisualData()
    {
        KappaVisualData.Surfboard.renderer.enabled = false;
        KappaVisualData.DressSet.upperItem.renderer.enabled = false;
    }

    public void SpawnCoinGroup(string name, Vector3 position)
    {
        comp_Coin_Manager.SpawnCoinGroup(name, position);
    }

    public Vector3 GetPlayerPosition()
    {
        return comp_Kappa_Controller.transform.position;
    }

    public Vector3 GetResetPositionLimit()
    {
        return comp_Kappa_Controller.RESET_TRANSFORM_LIMIT.position;
    }

    public void SpawnFrog(string name, Vector3 position)
    {
        comp_Frog_Manager.SpawnFrog(name, position);
    }

    public float getKappaSpeed()
    {
        return comp_Kappa_Controller.LAND_SPEED;
    }

    //-------------------------------------------------------------------------------
    // SAVE/LOAD DATA FUNCTIONS
    //-------------------------------------------------------------------------------
#pragma region SAVE_DATA_METHODS
    
    public void saveMangos()
    {
        PlayerPrefs.SetInt("Mangos", coins);
    }

    public void saveMangosNow()
    {
        PlayerPrefs.SetInt("Mangos", coins);
        PlayerPrefs.Save();
    }

    public void loadMangos()
    {
        coins = PlayerPrefs.GetInt("Mangos", 0);
    }
       

#pragma endregion
}