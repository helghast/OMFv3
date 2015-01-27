using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Comp_GameLogic_Manager : MonoBehaviour
{
    public float GAME_SPEED;
    public float METERS_MULTIPLIER;

    public GameObject papayaCounterGo;
    public GameObject mangoCounterGo;
    public GameObject frogCounterGo;

    private Text papayaCounterText;
    private Text mangoCounterText;
    private Text frogCounterText;

    void Awake()
    {
        frogCounterText = frogCounterGo.GetComponent<Text>();
        mangoCounterText = mangoCounterGo.GetComponent<Text>();
        papayaCounterText = papayaCounterGo.GetComponent<Text>();
        //GameLogicManager.Instance.Initialize();
    }

	// Use this for initialization
	void Start ()
    {
        GameLogicManager.Instance.GameSpeed = GAME_SPEED;
        GameLogicManager.Instance.MetersMultiplier = METERS_MULTIPLIER;

        GameLogicManager.Instance.SetCoins(GameLogicManager.Instance.Coins);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //GameManager.AddMeters(Time.deltaTime);
        GameLogicManager.Instance.AddMeters(Time.deltaTime);
	}

    void OnApplicationQuit()
    {
        GameLogicManager.Instance.saveMangos();
        PlayerPrefs.Save();
    }

    public void updatePapayaCounter(int value)
    {
        papayaCounterText.text = value.ToString();
    }

    public void updateMangoCounter(int value)
    {
        mangoCounterText.text = value.ToString();
    }

    public void updateFrogCounter(int value)
    {
        frogCounterText.text = value.ToString();
    }


}
