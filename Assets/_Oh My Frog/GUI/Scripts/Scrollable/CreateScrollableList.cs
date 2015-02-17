using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public enum ItembuttonStatus{ Buy = 0, ConfirmBuy, Equip, Unequip };

[System.Serializable]
public class Item{
    public string itemName;
    public Sprite itemImage;
    public string itemDescription;
    public int itemQuantity;
    public int price;
    public ItembuttonStatus status;
}

public class CreateScrollableList : MonoBehaviour {

    public GameObject sampleItemPanel;
    public Dictionary<string, List<GameObject>> mapaItemsShop = new Dictionary<string, List<GameObject>>();
    public Transform contentPanel;
    public Button[] arrayButtons;
    public Transform purchasePanel;

	// Use this for initialization
	void Start () {
        purchasePanel.gameObject.SetActive(false);
        //populateList();
	}

    //ugly!
    public void populateList(string key)
    {
        //desactivar boton segun el actual key cargado
        for(int i = 0; i < arrayButtons.Length; i++) {
            if(arrayButtons[i].GetComponentInChildren<Text>().text == key) {
                arrayButtons[i].enabled = false;
                arrayButtons[i].GetComponent<Image>().color = Color.grey;
            } else {
                arrayButtons[i].enabled = true;
                arrayButtons[i].GetComponent<Image>().color = Color.green;
            }
        }

        //crear una nueva key en el diccionario
        if(!mapaItemsShop.ContainsKey(key)) {
            mapaItemsShop.Add(key, new List<GameObject>());
        }

        //eliminar los items del panel
        for(int i = 0; i < contentPanel.GetComponentsInChildren<SampleItem>().Length; i++) {
            Destroy(contentPanel.GetComponentsInChildren<SampleItem>()[i].gameObject);
        }

        //dependiendo de laky/string obtener una lista de la shop u otra
        if(key == "Items") {
            createlistItems(key, ShopManager.CreateManager().shop.items);
        } else if(key == "Skins") {
            createlistItems(key, ShopManager.CreateManager().shop.skins);
        }
    }

    //very ugly!!!
    public void destroyPopulatedList() {
        foreach(KeyValuePair<string, List<GameObject>> kv in mapaItemsShop) {
            foreach(GameObject go in kv.Value) {
                Destroy(go);
            }
        }
        mapaItemsShop.Clear();
    }

    private void createlistItems(string key, List<Item> list) {
        for(int i = 0; i < list.Count; i++) {
            GameObject go = Instantiate(sampleItemPanel) as GameObject;
            SampleItem si = go.GetComponent<SampleItem>();
            si.itemTitle.text = list[i].itemName;
            si.image.sprite = list[i].itemImage;
            si.description.text = list[i].itemDescription;
            si.nameQuantity.text = "U Have";
            si.quantity.text = list[i].itemQuantity.ToString();
            si.priceElement = list[i].price;
            if(list[i].status == ItembuttonStatus.Buy) {
                si.statusButton.GetComponentInChildren<Text>().text = list[i].price.ToString();
            } else {
                si.statusButton.GetComponentInChildren<Text>().text = list[i].status.ToString();
            }
            si.status = list[i].status;
            si.positionInList = i;
            si.listName = key;
            go.transform.SetParent(contentPanel, false);

            mapaItemsShop[key].Add(go);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
