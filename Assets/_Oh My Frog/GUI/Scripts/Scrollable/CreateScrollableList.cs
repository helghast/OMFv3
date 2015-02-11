using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public enum ItembuttonStatus{ Buy, ConfirmBuy, Equip, Unequip };

[System.Serializable]
public class Item{
    public string itemName;
    public Sprite itemImage;
    public string itemDescription;
    public int itemQuantity;
    public ItembuttonStatus status;
}

public class CreateScrollableList : MonoBehaviour {

    public GameObject sampleItemPanel;
    public List<Item> itemList;
    public Transform contentPanel;

	// Use this for initialization
	void Start () {
        //populateList();
	}

    public void populateList()
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            GameObject newItem = Instantiate(sampleItemPanel) as GameObject;
            SampleItem newSampleItem = newItem.GetComponent<SampleItem>();
            newSampleItem.itemTitle.text = itemList[i].itemName;
            newSampleItem.image.sprite = itemList[i].itemImage;
            newSampleItem.description.text = itemList[i].itemDescription;
            newSampleItem.nameQuantity.text = "U Have";
            newSampleItem.quantity.text = itemList[i].itemQuantity.ToString();
            newSampleItem.statusButton.GetComponentInChildren<Text>().text = itemList[i].status.ToString();
            newItem.transform.SetParent(contentPanel, false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
