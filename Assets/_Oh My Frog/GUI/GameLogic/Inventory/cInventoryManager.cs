using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager
{
    //-----------------------------------------------
    //  STATIC ATRIBUTES
    //-----------------------------------------------
    private static InventoryManager instance;
    public static InventoryManager Instance
    {
        get
        {
            return instance;
        }
    }
    //-----------------------------------------------
    //  CONSTRUCTOR INFO
    //-----------------------------------------------
    private InventoryManager()
    {
        
    }

    //-----------------------------------------------
    //  ATRIBUTES
    //-----------------------------------------------
    //public ScriptableObject_Shop shop;
    
    // Mapa de todos los GameObjects de todas las tablas de surf disponibles para Kappa (a través de InAppPurchase) (skins)
    public Dictionary<string, GameObject> map_KappaSkins;
	
	// consumables --> bombas, imanes, ...
	
	// upgrades --> 

    // JACOBO ---> desde esta var accedes a List<cUI_Item> que contiene todos los items de Shop
    // Lista de todos los UI-Items disponibles en el juego 
    //public List<cUI_Item> UI_List_All_Items;

    //-----------------------------------------------
    //  FUNCTIONS
    //-----------------------------------------------
    public static void CreateManager()
    {
        if (instance == null)
        {
            instance = new InventoryManager();
            Instance.Initialize();
        }
    }

    public void Initialize()
    {
        //shop = GameObject.Find("Shop").GetComponent<Comp_Shop>().shop;
        //UI_List_All_Items = shop.UI_Items;

        map_KappaSkins = new Dictionary<string, GameObject>();
        for (int i = 0; i < ShopManager.Instance.UI_List_All_Items.Count; ++i)
        {
            //Debug.Log("Item: " + ShopManager.Instance.UI_List_All_Items[i].name);
        }
    }

    public void LoadInGameItems()
    {
        for (int i = 0; i < ShopManager.Instance.shop.Surfboards.Count; ++i)
        {
            GameObject item = (GameObject)GameObject.Find(ShopManager.Instance.shop.Surfboards[i].name);
            //Debug.Log("Item: " + ShopManager.Instance.shop.Surfboards[i].name);

            map_KappaSkins[item.name] = item;
            //Debug.Log(item.name);
        }

        for (int i = 0; i < ShopManager.Instance.shop.ItemSets.Count; ++i)
        {
            GameObject item = (GameObject)GameObject.Find(ShopManager.Instance.shop.ItemSets[i].name);
            map_KappaSkins[item.name] = item;
            //Debug.Log(item.name);
        }
    }

    public void setKappaItemVisibleByName(string name, bool visible)
    {
        if (ShopManager.Instance.shop)
        {
            map_KappaSkins[name].renderer.enabled = visible;
        }
    }

    public GameObject getKappaItemByName(string name)
    {
        return map_KappaSkins[name];
    }
}

