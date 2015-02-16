﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SampleItem : MonoBehaviour
{

    public GameObject item;
    public Text itemTitle;
    public Image image;
    public Text description;
    public Text nameQuantity;
    public Text quantity;
    public Button statusButton;
    public int positionInList;
    public string listName;

    private int i = 0;
    private ItembuttonStatus status = ItembuttonStatus.Buy;

    // Use this for initialization
    void Start()
    {
        //statusButton.onClick.AddListener(() => changeStatusButton(i++));
    }

    // Update is called once per frame
    void Update()
    {
        if(i > 3)
        {
            i = 0;
        }
    }

    public void clickButton()
    {
        Debug.Log(statusButton.GetComponentInChildren<Text>().text);
    }

    public void changeStatusButton()
    {
        string tempstatus = "";

        switch(status)
        {
            case ItembuttonStatus.Buy:
                tempstatus = ItembuttonStatus.ConfirmBuy.ToString();
                status = ItembuttonStatus.ConfirmBuy;
                break;
            case ItembuttonStatus.ConfirmBuy:
                tempstatus = ItembuttonStatus.Equip.ToString();
                status = ItembuttonStatus.Equip;
                break;
            case ItembuttonStatus.Equip:
                tempstatus = ItembuttonStatus.Unequip.ToString();
                status = ItembuttonStatus.Unequip;
                break;
            case ItembuttonStatus.Unequip:
                tempstatus = ItembuttonStatus.Equip.ToString();
                status = ItembuttonStatus.Equip;
                break;
        }
        statusButton.GetComponentInChildren<Text>().text = tempstatus;
        if(listName == "Items") {
            ShopManager.CreateManager().shop.items[positionInList].status = status;
        } else if(listName == "Skins") {
            ShopManager.CreateManager().shop.skins[positionInList].status = status;
        }        
    }
}
