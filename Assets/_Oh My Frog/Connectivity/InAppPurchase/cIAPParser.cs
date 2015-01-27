using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;
using System;

public enum ePROCESSING_STATE
{
    CURRENCIES = 0,
    CURRENCY_PACKS,
    VIRTUAL_GOODS
}

public class cIAP_Parser : BaseXMLParser
{
    private IAP_Assets ourIAPAssets;
    private ePROCESSING_STATE eProcessingState;
    public cIAP_Parser() : base() 
    {
    }

    public void setIAPAssetsContainer(IAP_Assets ourIAPAssets)
    {
        this.ourIAPAssets = ourIAPAssets;
    }

    public override void onStartElement(string elem, Dictionary<string, string> atts)
    {
        if (elem == "Currency")
        {
            //eProcessingState = ePROCESSING_STATE.CURRENCIES;
            processCurrency(atts);
        }
        else if (elem == "CurrencyPack")
        {
            //eProcessingState = ePROCESSING_STATE.CURRENCY_PACKS;
            processCurrencyPack(atts);
        }
        else if (elem == "LifetimeVG")
        {
            //eProcessingState = ePROCESSING_STATE.VIRTUAL_GOODS;
            processLifetimeVirtualGood(atts);
        }
        else if (elem == "EquippableVG")
        {
            processEquippableVirtualGood(atts);
        }

        else if (elem == "SingleUseVG")
        {
            processSingleUseVirtualGood(atts);
        }
        else if (elem == "UpgradeVG")
        {
            processSingleUseVirtualGood(atts);
        }
    }

    private void processCurrency(Dictionary<string, string> atts)
    {
        string name = atts["name"];
        VirtualCurrency currency = new VirtualCurrency(name,
                                                       atts["desc"],
                                                       atts["localID"]);
        currency.imageIconPath = atts["spriteIcon"];
        ourIAPAssets.Map_VirtualCurrencies[name] = currency;
    }

    private void processCurrencyPack(Dictionary<string, string> atts)
    {
        string name = atts["name"];
        VirtualCurrencyPack currencyPack = new VirtualCurrencyPack(name,
                                                               atts["desc"],
                                                               atts["localID"],
                                                               Convert.ToInt32(atts["amount"]),
                                                               atts["currencyID"],
                                                               new PurchaseWithMarket(atts["cloudID"], Convert.ToDouble(atts["cost"]))
                                                               );
        currencyPack.imageIconPath = atts["spriteIcon"];
        ourIAPAssets.Map_VirtualCurrencyPacks[name] = currencyPack;
    }

    private void processLifetimeVirtualGood(Dictionary<string, string> atts)
    {
        string name = atts["name"];
        string desc = atts["desc"];
        string localID = atts["localID"];
        string currencyID = atts["currencyID"];
        LifetimeVG lifetimeVG = new LifetimeVG(name,
                                               desc,
                                               localID,
                                               new PurchaseWithVirtualItem(currencyID, Convert.ToInt32(atts["currencyCost"]))
                                               );
        lifetimeVG.imageIconPath = atts["spriteIcon"];
        ourIAPAssets.Map_VirtualGoods[name] = lifetimeVG;
        
    }

    private void processSingleUseVirtualGood(Dictionary<string, string> atts)
    {
        string name = atts["name"];
        SingleUseVG singleUseVG = new SingleUseVG(name,
                                                  atts["desc"],
                                                  atts["localID"],
                                                  new PurchaseWithVirtualItem(atts["localID"], Convert.ToInt32(atts["currencyCost"]))
                                                  );
        singleUseVG.imageIconPath = atts["spriteIcon"];
        ourIAPAssets.Map_VirtualGoods[name] = singleUseVG;
    }

    private void processEquippableVirtualGood(Dictionary<string, string> atts)
    {
        string name = atts["name"];
        EquippableVG equippableVG = new EquippableVG(EquippableVG.EquippingModel.LOCAL,
                                                     name,
                                                     atts["desc"],
                                                     atts["localID"],
                                                     new PurchaseWithVirtualItem(atts["localID"], Convert.ToInt32(atts["currencyCost"]))
                                                     );
        equippableVG.imageIconPath = atts["spriteIcon"];
        ourIAPAssets.Map_VirtualGoods[name] = equippableVG;
    }

    private void processUpgradeVirtualGood(Dictionary<string, string> atts)
    {
        string name = atts["name"];
        UpgradeVG upgradeVG = new UpgradeVG(atts["localID"],
                                           atts["prevItem"],
                                           atts["nextItem"],
                                           name,
                                           atts["desc"],
                                           atts["localID"], 
                                           new PurchaseWithVirtualItem(atts["localID"], Convert.ToInt32(atts["currencyCost"]))
                                           );

        upgradeVG.imageIconPath = atts["spriteIcon"];
        ourIAPAssets.Map_VirtualGoods[name] = upgradeVG;
    }

    public override void onEndElement(string elem)
    {
       
    }
	
}
