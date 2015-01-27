using UnityEngine;
using System.Collections;

public class Item_ID : MonoBehaviour {

    private string ITEM_ID;
    private int ITEM_POSITION_LIST;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void set_Item_ID(string id){
        this.ITEM_ID = id;
    }

    public string get_Item_ID()
    {
        return ITEM_ID;
    }

    public int get_Item_POSITION_LIST()
    {
        return ITEM_POSITION_LIST;
    }

    public void set_Item_POSITION_LIST(int position)
    {
       this.ITEM_POSITION_LIST = position;
    }
}
