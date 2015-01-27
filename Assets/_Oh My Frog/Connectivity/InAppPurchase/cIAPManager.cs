using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Soomla.Store;

public class IAPManager
{
    //-----------------------------------------------
    //  STATIC ATRIBUTES
    //-----------------------------------------------
    private static IAPManager instance;
    public static IAPManager Instance
    {
        get
        {
            return instance;
        }
    }

    //-----------------------------------------------
    //  CONSTRUCTOR INFO
    //-----------------------------------------------
    private IAPManager()
    {

    }

    public static void CreateManager()
    {
        if (instance == null)
        {
            instance = new IAPManager();
            Instance.Initialize();
        }
    }
    
    public void Initialize()
    {
        StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;
        SoomlaStore.Initialize(new IAP_Assets());
        //Debug.Log("List of Goods: " + StoreInfo.Goods.Count);
    }

    //-----------------------------------------------
    //  ATRIBUTES
    //-----------------------------------------------
    public List<VirtualGood> VirtualGoods = null;

    //-----------------------------------------------
    //  FUNCTIONS
    //-----------------------------------------------
    public void onSoomlaStoreInitialized()
    {
        // this is the plugin
        VirtualGoods = StoreInfo.Goods;
        Debug.Log("Soomla Store initialized with: " + VirtualGoods.Count + " items");
    }

    public List<VirtualGood> GetVirtualGoods()
    {
        return VirtualGoods;
    }

    public bool BuyItem(string itemID)
    {
        try
        {
            // this is the plugin
            StoreInventory.BuyItem(itemID);
            return true;
        }
        catch(Exception e)
        {
            Debug.Log("UNITY/SOOMLA/BUY_ITEM:  " + e.Message);
            return false;
        }
    }

    public bool CheckItem(string itemID)
    {
        try
        {
            if (StoreInventory.GetItemBalance(itemID) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Debug.Log("UNITY/SOOMLA/CHECK_ITEM:  " + e.Message);
            return false;
        }
    }
}
