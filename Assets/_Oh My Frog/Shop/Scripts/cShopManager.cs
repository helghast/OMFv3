using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

using Soomla.Store;

/*
 * ShopManager es responsable de gestionar la información que aparece en la tienda del juego.
 * Contiene una lista de objetos (cUI_Item) que sirve para almacenar la información necesaria
 * que debe ser mostrada al jugador en el menu principal, dentro del apartado de Tienda.
 * 
 * ShopManager se nutre del ScriptableObject Shop, elemento que tenemos que rellenar nosotros
 * a medida que nuestra tienda va creciendo. Es importante notar que la información de este
 * objeto debe coincidir con la información de los objetos que hay en GooglePlay o la AppStore.
 * 
 */

public class ShopManager
{
    //-----------------------------------------------
    //  STATIC ATRIBUTES
    //-----------------------------------------------
    private static ShopManager instance = null;
    private static Dictionary<string, List<Item>> _mapaListItems = new Dictionary<string, List<Item>>();

    public static ShopManager Instance
    {
        get
        {
            return instance;
        }
    }

    //-----------------------------------------------
    //  ATRIBUTES
    //-----------------------------------------------
    public ScriptableObject_Shop shop;
    public List<cUI_Item> UI_List_All_Items;

    //-----------------------------------------------
    //  CONSTRUCTOR INFO
    //-----------------------------------------------
    private ShopManager()
    {

    }

    public static ShopManager CreateManager()
    {
        if (instance == null)
        {
            instance = new ShopManager();
            Instance.Initialize();
        }
        return instance;
    }

    public void Initialize()
    {
        shop = GameObject.Find("Shop").GetComponent<Comp_Shop>().shop;
        UI_List_All_Items = shop.UI_Items;
    }

    //-----------------------------------------------
    //  ATRIBUTES
    //-----------------------------------------------
    public void SetToMap(string key, List<Item> values) {
        _mapaListItems.Add(key, values);
    }

    public Dictionary<string, List<Item>> GetFromMap() {
        return _mapaListItems;
    }

    public List<Item> GetListMap(string key) {
        if(_mapaListItems.ContainsKey(key)) {
            return _mapaListItems[key];
        } else {
            return null;
        }
    }

    //temporal para guardar u obtener cantidad de mangos que estan almacenados en la shop del player
    public int MangosQuantity {
        get {
            return shop.mangosQuantity;
        }
        set {
            shop.mangosQuantity = value;
        }
    }

    public int CoctelesQuantity {
        get {
            return shop.coctelesQuantity;
        }
        set {
            shop.coctelesQuantity = value;
        }
    }

    //-----------------------------------------------
    //  FUNCTIONS
    //-----------------------------------------------
    public void ImprimirMap() {        
        foreach(KeyValuePair<string, List<Item>> item in _mapaListItems) {            
            foreach(Item itemlist in item.Value) {
                StringBuilder sb = new StringBuilder();
                sb.Append(item.Key);
                sb.Append(":");
                sb.Append(itemlist.itemName);
                sb.Append(" | ");
                sb.Append(itemlist.itemDescription);
                sb.Append(" | ");
                sb.Append(itemlist.itemQuantity);
                sb.Append(" | ");
                sb.Append(itemlist.price);
                sb.Append(" | ");
                sb.Append(itemlist.status);
                Debug.Log(sb);
            }            
        }        
    }
}
