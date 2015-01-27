using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

/*
 * Clase auxiliar que permite obtener un objeto "comprable" a partir de una llave de entrada (a través de un MAP).
 * - El Map se rellena a partir del objeto Shop (ScriptableObject).
 * 
 * 
 * 
 * 
 */

public class IAP_Assets : IStoreAssets //--> IStoreAssets es el plugin
{
    // Tipos de moneda virtual (mangos, gemas...)
    public Dictionary<string, VirtualCurrency> Map_VirtualCurrencies;
    // Packs de monedas
    public Dictionary<string, VirtualCurrencyPack> Map_VirtualCurrencyPacks;

    // Consumibles
    public Dictionary<string, VirtualGood> Map_SingleUseVirtualGoods;
    // Son eternos, solo se pueden comprar una vez.
    public Dictionary<string, VirtualGood> Map_LifetimeVirtualGoods;
    // Son eternos, solo se pueden comprar una vez y se pueden equipar.
    public Dictionary<string, VirtualGood> Map_EquippableVirtualGoods;

    public Dictionary<string, VirtualGood> Map_UpgradeableVirtualGoods;

    
    // Global de todos los VirtualGoods
    public Dictionary<string, VirtualGood> Map_VirtualGoods;

    public IAP_Assets()
        : base()
    {
        Initialize();
    }

    public int GetVersion()
    {
        return 0;
    }

    public VirtualCurrency[] GetCurrencies()
    {
        VirtualCurrency[] currencies = new VirtualCurrency[Map_VirtualCurrencies.Count];
        Map_VirtualCurrencies.Values.CopyTo(currencies, 0);

        return currencies;
    }

    public VirtualCurrencyPack[] GetCurrencyPacks()
    {
        VirtualCurrencyPack[] currencyPacks = new VirtualCurrencyPack[Map_VirtualCurrencyPacks.Count];
        Map_VirtualCurrencyPacks.Values.CopyTo(currencyPacks, 0);

        return currencyPacks;
    }

    public VirtualCategory[] GetCategories()
    {
        return new VirtualCategory[]
        {
        };
    }

    public VirtualGood[] GetLifetimeVirtualGoods()
    {
        LifetimeVG[] goods = new LifetimeVG[Map_LifetimeVirtualGoods.Count];
        Map_LifetimeVirtualGoods.Values.CopyTo(goods, 0);

        return goods;
    }

    public VirtualGood[] GetSingleUseVirtualGoods()
    {
        SingleUseVG[] goods = new SingleUseVG[Map_SingleUseVirtualGoods.Count];
        Map_SingleUseVirtualGoods.Values.CopyTo(goods, 0);

        return goods;
    }

    public VirtualGood[] GetEquippableVirtualGoods()
    {
        EquippableVG[] goods = new EquippableVG[Map_EquippableVirtualGoods.Count];
        Map_EquippableVirtualGoods.Values.CopyTo(goods, 0);

        return goods;
    }

    public VirtualGood[] GetUpgradeableVirtualGoods()
    {
        UpgradeVG[] goods = new UpgradeVG[Map_UpgradeableVirtualGoods.Count];
        Map_UpgradeableVirtualGoods.Values.CopyTo(goods, 0);

        return goods;
    }

    /*
     * Obtiene todos los Goods registrados en el MAPA y los devuelve en forma de Array.
     * Este método es necesario ya que el IAPManager necesita una array de VirtualGood para hacer hacer comprobaciones
     * de items con la AppStore o la PlayStore.
     */
    public VirtualGood[] GetGoods()
    {
        /*
        int total = 0;
        total += Map_LifetimeVirtualGoods.Count;
        total += Map_SingleUseVirtualGoods.Count;
        total += Map_EquippableVirtualGoods.Count;

        VirtualGood[] goods = new VirtualGood[total];
        Map_LifetimeVirtualGoods.Values.CopyTo(goods, 0);
        Map_SingleUseVirtualGoods.Values.CopyTo(goods, Map_LifetimeVirtualGoods.Count);
        Map_EquippableVirtualGoods.Values.CopyTo(goods, Map_SingleUseVirtualGoods.Count);*/

        VirtualGood[] goods = new VirtualGood[Map_VirtualGoods.Count];
        Map_VirtualGoods.Values.CopyTo(goods, 0);

        return goods;
    }

    /*
     * Lee todos los objetos IAP del scriptable-object Shop y los registra en un MAP Map_LifeTimeVG <IPA-object_name, LifeTimeVG>
     * Map_LifeTimeVG["IAP_name_object"] --> nos devuelve el LifeTimeVG registrado con ese nombre.
     */
    
    public void Initialize()
    {
        Map_VirtualGoods = new Dictionary<string, VirtualGood>();
        Map_VirtualCurrencies = new Dictionary<string, VirtualCurrency>();
        Map_VirtualCurrencyPacks = new Dictionary<string, VirtualCurrencyPack>();
        Map_SingleUseVirtualGoods = new Dictionary<string, VirtualGood>();
        Map_LifetimeVirtualGoods = new Dictionary<string, VirtualGood>();
        Map_EquippableVirtualGoods = new Dictionary<string, VirtualGood>();
    }
}