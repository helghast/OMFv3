using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClickButtonGUI : MonoBehaviour {

    //si se usa el SetActive ya no es necesario el CanvasGroups
    //public CanvasGroup[] listCanvasGroups = null;
    private string[] languagesArray = new string[] { "SPANISH", "ENGLISH", "FRENCH", "GERMAN", "ITALIAN" };
    public GameObject panelTotem, panelRedesSociales, panelAudioOptions;

    void Awake()
    {
        //para poder usar un objeto inactivo, iniciarlo como inactivo desde el awake, mejor que manualmente desde el editor
        panelAudioOptions.SetActive(false);
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
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
        string facebookshare = "https://www.facebook.com/sharer/sharer.php?u=" + WWW.EscapeURL("https://www.facebook.com/ohmyfrog.surfrescue");

        Application.OpenURL(facebookshare);
    }

    public void shareTwitter()
    {
        /*string twittershare = "http://twitter.com/home?status=" + WWW.EscapeURL("direccion twitter");

        Application.OpenURL(twittershare);*/
    }

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
