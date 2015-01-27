using UnityEngine;
using System.Collections;

[System.Serializable]
public class Comp_Coin : MonoBehaviour
{
    public Comp_Coin_Manager comp_Coin_Manager;
    public cCoin coin;

	// Use this for initialization
	void Awake ()
    {
        comp_Coin_Manager = GameObject.Find("GameLogic_Manager").GetComponent<Comp_Coin_Manager>();
        coin = new cCoin(this);
	}
	/*
	// Update is called once per frame
	void Update ()
    {
	
	}
     */
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            coin.DeleteCoin();
            Debug.Log("Coin " + coin.index + " cogida");
            GameLogicManager.Instance.AddCoins(1);
        }
        else if (other.tag == "Object_Destroyer")
        {
            coin.DeleteCoin();
        }
    }
}
