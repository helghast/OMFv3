using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;
using System;

public class cIAP
{
    private IAP_Assets ourIAPAssets;
    private cIAP_Parser parser;
    public List<VirtualGood> ListVirtualGoods = null;

    public cIAP(string xml_filename)
    {
        ourIAPAssets = new IAP_Assets();

        parser = new cIAP_Parser();
        StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;
        loadIAPData(xml_filename);
        SoomlaStore.Initialize(ourIAPAssets);    
    }

    public void loadIAPData(string filename)
    {
        parser.setIAPAssetsContainer(ourIAPAssets);
        TextAsset textAsset = (TextAsset)Resources.Load("IAP/XML/" + filename);
        parser.xmlParseFile(textAsset);
    }

    public Dictionary<string, VirtualGood> VirtualGoods
    {
        get
        {
            return ourIAPAssets.Map_VirtualGoods;
        }
    }

    public Dictionary<string, VirtualCurrencyPack> VirtualCurrencyPacks
    {
        get
        {
            return ourIAPAssets.Map_VirtualCurrencyPacks;
        }
    }

    public Dictionary<string, VirtualCurrency> VirtualCurrencies
    {
        get
        {
            return ourIAPAssets.Map_VirtualCurrencies;
        }
    }

    public void GivePlayerNCurrencies(string currencyName, int amount)
    {

    }

    public void GivePlayerNMangos(int amount)
    {
        StoreInventory.GiveItem(VirtualCurrencies["mango"].ID, amount);
    }

    public void ResetPlayerMangos(int reset_value)
    {
        VirtualCurrencies["mango"].ResetBalance(reset_value);
    }


    /*
    public void Initialize()
    {
        StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;
    }*/
    /*
    public void InitializeSoomlaStore()
    {
        SoomlaStore.Initialize(ourIAPAssets);        
    }*/

    //-----------------------------------------------
    //  FUNCTIONS
    //-----------------------------------------------
    public void onSoomlaStoreInitialized()
    {
        // this is the plugin
        ListVirtualGoods = StoreInfo.Goods;
        Debug.Log("Soomla Store initialized with: " + VirtualGoods.Count + " items");
        VirtualGood[] vgs = new VirtualGood[VirtualGoods.Count];
        VirtualGoods.Values.CopyTo(vgs, 0);
        for (int i = 0; i < VirtualGoods.Count; ++i)
        {
            Debug.Log("Name: " + vgs[i].Name + " --- Item ID: " + vgs[i].ID);
        }
    }

    public List<VirtualGood> GetVirtualGoodsList()
    {
        return ListVirtualGoods;
    }

    public bool BuyVirtual(string itemID)
    {
        try
        {
            // this is the plugin
            StoreInventory.BuyItem(itemID);
            Debug.Log("UNITY/SOOMLA/BUY_ITEM: BUY SUCCESS!");
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("UNITY/SOOMLA/BUY_ITEM:  " + e.Message);
            return false;
        }
    }

    public bool BuyItem(string itemID)
    {
        try
        {
            // this is the plugin
            StoreInventory.BuyItem(itemID);
            return true;
        }
        catch (Exception e)
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
