using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SampleItem : MonoBehaviour {
    public GameObject item;
    public Text itemTitle;
    public Image image;
    public Text description;
    public Text nameQuantity;
    public Text quantity;
    public Button statusButton;
    public int positionInList;
    public int priceElement;
    public string listName;

    //private int i = 0;
    public ItembuttonStatus status;

    // Use this for initialization
    void Start() {
        //statusButton.onClick.AddListener(() => changeStatusButton(i++));

    }

    // Update is called once per frame
    void Update() {
        /*if(i > 3)
        {
            i = 0;
        }*/
    }

    public void clickButton() {
        Debug.Log(statusButton.GetComponentInChildren<Text>().text);
    }

    public void changeStatusButton() {
        switch(status) {
            case ItembuttonStatus.Buy:
                status = ItembuttonStatus.ConfirmBuy;
                break;
            case ItembuttonStatus.ConfirmBuy:
                status = confirmBuyCheck() ? ItembuttonStatus.Equip : ItembuttonStatus.Buy;
                break;
            case ItembuttonStatus.Equip:
                status = ItembuttonStatus.Unequip;
                equipOrUnEquip(true);
                break;
            case ItembuttonStatus.Unequip:
                status = ItembuttonStatus.Equip;
                equipOrUnEquip(false);
                break;
        }
        statusButton.GetComponentInChildren<Text>().text = status.ToString();
        allUnequipToEquip();
    }

    private bool confirmBuyCheck() {
        if(ShopManager.CreateManager().MangosQuantity < priceElement) {
            Debug.Log("te faltan mangos");
            GameObject.Find("New_Shop_Panel").GetComponent<CreateScrollableList>().purchasePanel.gameObject.SetActive(true);
            return false;
        } else {
            ShopManager.CreateManager().MangosQuantity -= priceElement;
            switch(listName) {
                case "Items":
                    ShopManager.CreateManager().shop.items[positionInList].itemQuantity += 1;
                    quantity.text = ShopManager.CreateManager().shop.items[positionInList].itemQuantity.ToString();
                    break;
                case "Skins":
                    ShopManager.CreateManager().shop.skins[positionInList].itemQuantity += 1;
                    quantity.text = ShopManager.CreateManager().shop.skins[positionInList].itemQuantity.ToString();
                    break;
            }
            GameObject.Find("CantidadMangos").GetComponent<Text>().text = ShopManager.CreateManager().MangosQuantity.ToString();
            return true;
        }
    }

    private void allUnequipToEquip() {
        switch(listName) {
            case "Items":
                ShopManager.CreateManager().shop.items[positionInList].status = status;
                for(int i = 0; i < ShopManager.CreateManager().shop.items.Count; i++) {
                    if(ShopManager.CreateManager().shop.items[i].status == ItembuttonStatus.Unequip && i != positionInList) {
                        ShopManager.CreateManager().shop.items[i].status = ItembuttonStatus.Equip;
                    }
                }
                break;
            case "Skins":
                ShopManager.CreateManager().shop.skins[positionInList].status = status;
                for(int i = 0; i < ShopManager.CreateManager().shop.skins.Count; i++) {
                    if(ShopManager.CreateManager().shop.skins[i].status == ItembuttonStatus.Unequip && i != positionInList) {
                        ShopManager.CreateManager().shop.skins[i].status = ItembuttonStatus.Equip;
                    }
                }
                break;
        }
    }

    public void equipOrUnEquip(bool newStatus) {
        if(listName == "Skins") {
            Comp_Kappa_Skins skins = GameObject.Find("Kappa2_007").GetComponent<Comp_Kappa_Skins>();
            skins.arraySkins[positionInList].SetActive(newStatus);
            skins.arraySkins[positionInList + 1].SetActive(newStatus);
        }
    }
}
