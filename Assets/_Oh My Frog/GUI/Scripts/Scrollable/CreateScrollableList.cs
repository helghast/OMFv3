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
    public string price;
    public ItembuttonStatus status;
}

public class CreateScrollableList : MonoBehaviour {

    public GameObject sampleItemPanel;
    public Dictionary<string, List<GameObject>> mapaItemsShop = new Dictionary<string, List<GameObject>>();
    public Transform contentPanel;
    public Button[] arrayButtons;

	// Use this for initialization
	void Start () {
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

        if(key == "Items") {
            for(int i = 0; i < ShopManager.CreateManager().shop.items.Count; i++) {
                GameObject go = Instantiate(sampleItemPanel) as GameObject;
                SampleItem si = go.GetComponent<SampleItem>();
                si.itemTitle.text = ShopManager.CreateManager().shop.items[i].itemName;
                si.image.sprite = ShopManager.CreateManager().shop.items[i].itemImage;
                si.description.text = ShopManager.CreateManager().shop.items[i].itemDescription;
                si.nameQuantity.text = "U Have";
                si.quantity.text = ShopManager.CreateManager().shop.items[i].itemQuantity.ToString();
                if(ShopManager.CreateManager().shop.items[i].status == ItembuttonStatus.Buy) {
                    si.statusButton.GetComponentInChildren<Text>().text = ShopManager.CreateManager().shop.items[i].price;
                } else {
                    si.statusButton.GetComponentInChildren<Text>().text = ShopManager.CreateManager().shop.items[i].status.ToString();
                }
                si.positionInList = i;
                si.listName = key;
                go.transform.SetParent(contentPanel, false);

                mapaItemsShop[key].Add(go);
            }
        } else if(key == "Skins") {
            for(int i = 0; i < ShopManager.CreateManager().shop.skins.Count; i++) {
                GameObject go = Instantiate(sampleItemPanel) as GameObject;
                SampleItem si = go.GetComponent<SampleItem>();
                si.itemTitle.text = ShopManager.CreateManager().shop.skins[i].itemName;
                si.image.sprite = ShopManager.CreateManager().shop.skins[i].itemImage;
                si.description.text = ShopManager.CreateManager().shop.skins[i].itemDescription;
                si.nameQuantity.text = "U Have";
                si.quantity.text = ShopManager.CreateManager().shop.skins[i].itemQuantity.ToString();
                if(ShopManager.CreateManager().shop.skins[i].status == ItembuttonStatus.Buy) {
                    si.statusButton.GetComponentInChildren<Text>().text = ShopManager.CreateManager().shop.skins[i].price;
                } else {
                    si.statusButton.GetComponentInChildren<Text>().text = ShopManager.CreateManager().shop.skins[i].status.ToString();
                }
                si.positionInList = i;
                si.listName = key;
                go.transform.SetParent(contentPanel, false);

                mapaItemsShop[key].Add(go);
            }
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
