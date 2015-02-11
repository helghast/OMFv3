using UnityEngine;
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

    private int i;
    private ItembuttonStatus status;

    // Use this for initialization
    void Start()
    {
        i = 0;
        status = ItembuttonStatus.Buy;

        statusButton.onClick.AddListener(() => changeStatusButton(i++));
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

    public void changeStatusButton(int integer)
    {
        Debug.Log(integer);
        string tempstatus = "";
        switch(integer)
        {
            case 0:
                tempstatus = ItembuttonStatus.Buy.ToString();
                break;
            case 1:
                tempstatus = ItembuttonStatus.ConfirmBuy.ToString();
                break;
            case 2:
                tempstatus = ItembuttonStatus.Equip.ToString();
                break;
            case 3:
                tempstatus = ItembuttonStatus.Unequip.ToString();
                break;
        }
        statusButton.GetComponentInChildren<Text>().text = tempstatus;
    }
}
