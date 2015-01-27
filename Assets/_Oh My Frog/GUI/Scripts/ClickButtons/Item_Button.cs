using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item_Button : MonoBehaviour {

    //elementos ui del panel objetivo a modificar con los datos del elemento de la lista
    public Button item_Button;
    public Text item_Name_Text;
    public Text item_Description;
    public Image item_Image;
    public Text item_Price;
    //private cUI_Item temp_cUI_Item = null;
    private int listShop_Position;

	// Use this for initialization
	void Start () {
        if(item_Name_Text != null && item_Description != null && item_Image != null && item_Button != null && item_Price != null)
        {
            //intenta obtener objeto de la shop
            /*try
            {
                temp_cUI_Item = ItemManager.Instance.UI_List_All_Items[gameObject.GetComponentInParent<Item_ID>().get_Item_POSITION_LIST()];
            }
            catch(System.NullReferenceException e)
            {
                Debug.LogException(e);
            }*/
            //intenta obtener posicion de elemento en la lista shop
            try
            {
                listShop_Position = gameObject.GetComponentInParent<Item_ID>().get_Item_POSITION_LIST();
            }
            catch(System.NullReferenceException e)
            {
                Debug.LogException(e);
            }
            //add listener al boton de cada item
            item_Button.onClick.AddListener(() => mostrarPreview());
        }
        else
        {
            Debug.LogError("algun componente es nulo");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void mostrarPreview()
    {
        Debug.Log(GetComponentInParent<Item_ID>().get_Item_ID() + " <=> listposition: " + listShop_Position);

        //obtener datos de la lista del itemmanager estatico mediante el int obtenido
        item_Name_Text.text = ShopManager.Instance.UI_List_All_Items[listShop_Position].name;
        item_Image.sprite = ShopManager.Instance.UI_List_All_Items[listShop_Position].sprite;
        item_Description.text = ShopManager.Instance.UI_List_All_Items[listShop_Position].desc;
        item_Price.text = "Price = " + ShopManager.Instance.UI_List_All_Items[listShop_Position].cost.ToString();

        //obtener datos del objeto temporal obtenido de la shop
        /*item_Name_Text.text = temp_cUI_Item.name;
        item_Image.sprite = temp_cUI_Item.sprite;
        item_Description.text = temp_cUI_Item.desc;
        item_Price.text = "Price = " + temp_cUI_Item.cost.ToString();*/
    }
}
