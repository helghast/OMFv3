using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ScriptableObject_Shop : ScriptableObject
{
    public List<sItem>      Surfboards;
    public List<sItem>      ItemSets;
    //public List<Material>   Skins;
    public List<cUI_Item>   UI_Items;

    public List<cCurrency> Currencies;
    public List<cCurrencyPack> CurrencyPacks;

    public List<Item> items;
    public List<Item> skins;

    public int coctelesQuantity;
    public int mangosQuantity;
}
