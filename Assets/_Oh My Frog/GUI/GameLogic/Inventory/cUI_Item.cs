using UnityEngine;
using System.Collections;
using System;
using Eletroplasmatic_IAP;

[System.Serializable]
public class cUI_Item
{
    public string name;
    public string desc;
    public ItemType type;
    public string item_ID;
    public string product_ID;
    public float cost;
    public Sprite sprite;

    public cUI_Item()
    {

    }
}
