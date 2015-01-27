using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScrollableList : MonoBehaviour {

    //de momento hacer una copia de la lista estatica
    //public static List<cUI_Item> tempList = null;
    /*
     * los atributos de cada item cUI_Item de la lista son:
     * string name, string desc, string item_ID, string product_ID, float cost, Sprite sprite
     */

    //objecto a repetir
    public GameObject itemPanel;
    //cantidad de veces a repetir en cuantas columnas
    //public int itemCount = 10;
    //ahora itemCount es la cantidad de items disponibles en la lista estatica
    private int itemCount;
    public int columnCount = 1;

    void Awake(){
        //obtener lista de items
        /*tempList = ItemManager.Instance.UI_List_All_Items;
        if(tempList == null)
        {
            tempList = GameObject.Find("TestShop").GetComponent<Comp_Shop>().shop.UI_Items;
        }*/
       /* try
        {
            tempList = ItemManager.Instance.UI_List_All_Items;
        }
        catch(System.NullReferenceException e)
        {
            tempList = GameObject.Find("TestShop").GetComponent<Comp_Shop>().shop.UI_Items;
            Debug.LogException(e, this);
        }*/
        //tempList = GameObject.Find("TestShop").GetComponent<Comp_Shop>().shop.UI_Items;
        
        //cantidad de items en la lista
        //itemCount = tempList.Count;
        
        
        // DESCOMENTADO ALEJANDRO --> bugfixing reestructuracion de codigo
        //itemCount = ShopManager.Instance.UI_List_All_Items.Count;
    }

	// Use this for initialization
	void Start ()
    {
        //obtener rectransform del item
        RectTransform rowRectTransform = itemPanel.GetComponent<RectTransform>();
        //obtener rectransform del panel en el que esta este script
        RectTransform containerRectTransform = gameObject.GetComponent<RectTransform>();

        //calcular el ancho y alto de cada componente UI hijo del itempanel
        //y del panel
        float witdh = containerRectTransform.rect.width / columnCount;
        float ratio = witdh / rowRectTransform.rect.width;
        float height = rowRectTransform.rect.width * ratio;
        int rowCount = itemCount / columnCount;
        //si es >0 es necesario una fila mas
        if (rowCount != 0)
        {
            if (itemCount % rowCount > 0)
            {
                rowCount++;
            }
        }
        else
            rowCount = 1;
        

        //ajustar altura del contenedor para meter todos los items
        float scrollHeight = height * rowCount;
        containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
        containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);

        int j = 0;
        //por la cantidad de items
        for(int i = 0; i < itemCount; i++)
        {
            //en vez de usar un doblefor, se usa este if, porque los items pueden no encajar perfectamente dentro de rows/colums
            if(i % columnCount == 0)
            {
                j++;
            }
            //crear nuevo item basado en el pasado publicamente y poner en el panel
            GameObject newItemToFit = Instantiate(itemPanel) as GameObject;
            newItemToFit.name = " item at (" + i + "," + j + "): " + ShopManager.Instance.UI_List_All_Items[i].name;

            //usar transform.setparent(transform, bool) en vez de newItem.transform.parent = gameObject.transform; "Deprecado".
            newItemToFit.transform.SetParent(gameObject.transform, false);
            
            //mover y escalar el nuevo item
            RectTransform newItemRectTransform = newItemToFit.GetComponent<RectTransform>();
            float x = -containerRectTransform.rect.width / 2 + witdh * (i % columnCount);
            float y = containerRectTransform.rect.height / 2 - height * j;
            newItemRectTransform.offsetMin = new Vector2(x, y);

            x = newItemRectTransform.offsetMin.x + witdh;
            y = newItemRectTransform.offsetMin.y + height;
            newItemRectTransform.offsetMax = new Vector2(x, y);

            //rellenar con atributos de cada item
            //Los Children se guardan en el transform del objeto mismo: newItemToFit.transform.FindChild("Text_Item_Name").GetComponent<Text>().text = tempList[i].name;
            //nombre/titulo
            //newItemToFit.GetComponentInChildren<Text>().text = tempList[i].name; "funciona pero no es preciso si hay mas de un Text. Usar:"
            newItemRectTransform.FindChild("Text_Item_Name").GetComponent<Text>().text = ShopManager.Instance.UI_List_All_Items[i].name;
            //imagen. 
            newItemRectTransform.FindChild("Image_Item").GetComponent<Image>().sprite = ShopManager.Instance.UI_List_All_Items[i].sprite;
            newItemRectTransform.FindChild("Image_Item").GetComponent<Image>().preserveAspect = true;
            
            //guardar el id
            newItemToFit.GetComponent<Item_ID>().set_Item_ID(ShopManager.Instance.UI_List_All_Items[i].item_ID);
            newItemToFit.GetComponent<Item_ID>().set_Item_POSITION_LIST(i);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
