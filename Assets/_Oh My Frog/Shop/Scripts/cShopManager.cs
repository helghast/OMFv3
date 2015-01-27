using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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
    private static ShopManager instance;
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

    public static void CreateManager()
    {
        if (instance == null)
        {
            instance = new ShopManager();
            Instance.Initialize();
        }
    }

    public void Initialize()
    {
        shop = GameObject.Find("Shop").GetComponent<Comp_Shop>().shop;
        UI_List_All_Items = shop.UI_Items;
    }

    //-----------------------------------------------
    //  ATRIBUTES
    //-----------------------------------------------
    

    //-----------------------------------------------
    //  FUNCTIONS
    //-----------------------------------------------
    

    
}
