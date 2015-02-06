using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class ClickButtonGUI : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonX2
    {
        public Transform Clicked;
        public Transform Unclicked;

        public void setClicked(bool state)
        {
            Clicked.gameObject.SetActive(state);
            Unclicked.gameObject.SetActive(!state);
        }
    }

    public LayerMask layerMask;
    //si se usa el SetActive ya no es necesario el CanvasGroups
    //public CanvasGroup[] listCanvasGroups = null;
    private string[] languagesArray = new string[] { "SPANISH", "ENGLISH", "FRENCH", "GERMAN", "ITALIAN" };
    public GameObject panelTotem, panelRedesSociales, panelAudioOptions;

    public ButtonX2 playButton;
    public ButtonX2 buyButton;
    public ButtonX2 achievementsButton;
    public ButtonX2 rankingButton; 

    void Awake()
    {
        //para poder usar un objeto inactivo, iniciarlo como inactivo desde el awake, mejor que manualmente desde el editor
        panelAudioOptions.SetActive(false);
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 screen_pos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
                Vector3 world_pos = Camera.main.ScreenToWorldPoint(screen_pos);
                //world_pos.z = -10;
                RaycastHit hit;
                if (Physics.Raycast(world_pos, Camera.main.transform.forward, out hit, 20, layerMask))
                {
                    Debug.Log("collision raycast <tag: " + hit.collider.tag + ">< name: " + hit.collider.name + "> <layer: " + hit.collider.gameObject.layer + ">");
                    if (hit.collider.tag == "Frog")
                    {
                        hit.collider.GetComponent<Comp_IA_Frog>().SelfDestroy();
                        GameLogicManager.Instance.AddFrogs(1);
                    }
                }
                else
                {
                    
                }
            }
        }

	}

    public void clickButton(Text textButton)
    {
        switch(textButton.text.ToLower())
        {
            case "options":
                openOrClosePanel(true, 0.5f);
                break;
            case "accept":
                openOrClosePanel(false, 1f);
                break;
        }
        Debug.Log(textButton.text);
    }

    private void openOrClosePanel(bool status, float alpha)
    {
        panelAudioOptions.SetActive(status);
        panelTotem.GetComponent<CanvasGroup>().alpha = alpha;
        panelTotem.GetComponent<CanvasGroup>().interactable = !status;
        panelRedesSociales.GetComponent<CanvasGroup>().alpha = alpha;
        panelRedesSociales.GetComponent<CanvasGroup>().interactable = !status;
    }

    public void changeLanguage(Button langButton)
    {
        int positionInArray;

        for (int i = 0; i < languagesArray.Length; i++)
		{
            if(languagesArray[i].Equals(langButton.GetComponentInChildren<Text>().text))
            {
                if(i == languagesArray.Length - 1)
                {
                    positionInArray = 0;
                }
                else
                {
                    positionInArray = i + 1;
                }
                langButton.GetComponentInChildren<Text>().text = languagesArray[positionInArray];
                break;
            }	 
	    }
    }

    public void shareOnFacebook()
    {
        //forma facil
        string facebookshare = "https://www.facebook.com/sharer/sharer.php?u=" + WWW.EscapeURL("https://www.facebook.com/ohmyfrog.surfrescue");

        Application.OpenURL(facebookshare);
    }

    public void shareTwitter()
    {
        /*string twittershare = "http://twitter.com/home?status=" + WWW.EscapeURL("direccion twitter");

        Application.OpenURL(twittershare);*/
    }

    //facebook vars
    const string AppId = "1375252672785531";
    const string ShareUrl = "http://www.facebook.com/dialog/feed";
    const string PictureLink = "https://www.facebook.com/ohmyfrog.surfrescue";

    public static void Share(string link, string pictureLink, string name,
        string caption, string description, string redirectUri)
    {
        Application.OpenURL(ShareUrl +
            "?app_id=" + AppId +
            "&amp;link=" + WWW.EscapeURL(link) +
            "&amp;picture=" + WWW.EscapeURL(pictureLink) +
            "&amp;name=" + WWW.EscapeURL(name) +
            "&amp;caption=" + WWW.EscapeURL(caption) +
            "&amp;description=" + WWW.EscapeURL(description) +
            "&amp;redirect_uri=" + WWW.EscapeURL(redirectUri));
    }
}
