using UnityEngine;
using System.Collections;
using Facebook.MiniJSON;
using System.Collections.Generic;

public class FacebookManager
{

    static FacebookManager instance = null;

    public string FullName;
    public string Gender;

    public static FacebookManager Instance()
    {
        if(instance == null)
        {
            instance = new FacebookManager();
        }
        return instance;
    }

    /*void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        FB.Init(FB.OnInitComplete, FB.OnHideUnity);
        DontDestroyOnLoad(gameObject);
    }*/

    public bool isInit = false;
    public string FeedToId = "";
    public string FeedLink = "";
    public string FeedLinkName = "";
    public string FeedLinkCaption = "";
    public string FeedLinkDescription = "";
    public string FeedPicture = "";
    public string FeedMediaSource = "";
    public string FeedActionName = "";
    public string FeedActionLink = "";
    public string FeedReference = "";
    public bool IncludeFeedProperties = false;
    private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();

    //private string status = "Ready";

    private string lastResponse = "";

    private Texture2D lastResponseTexture;

    public string ApiQuery = "";

    public static FacebookManager use;

   /* void Start()
    {

        use = this;

    }*/

    public void PostToFacebook()
    {
        if(isInit)
        {
            /* CallFBFeed ();
         }
         else{*/
            CallFBLogin();
        }
    }


    public void CallFBInit()
    {
        FB.Init(OnInitComplete, OnHideUnity);
    }

    private void OnInitComplete()
    {
        Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
        isInit = true;

        //FB Initialized. Let's login if we're not.
        //if(!FB.IsLoggedIn) CallFBLogin();
    }

    private void OnHideUnity(bool isGameShown)
    {
        FbDebug.Log("OnHideUnity");
        if(!isGameShown)
        {
            // pause the game - we will need to hide                                             
            Time.timeScale = 0;
        }
        else
        {
            // start the game back up - we're getting focus again                                
            Time.timeScale = 1;
        }
    }


    private void CallFBLogin()
    {
        FB.Login("publish_actions", LoginCallback);
    }

    void LoginCallback(FBResult result)
    {
        if(result.Error != null)
            lastResponse = "Error Response:\n" + result.Error;
        else if(!FB.IsLoggedIn)
        {
            lastResponse = "Login cancelled by Player";
        }
        else if(FB.IsLoggedIn)
        {
            //Logged in successfully. Let's post a story.
            CallFBFeed();
        }
    }

    private void CallFBLogout()
    {
        FB.Logout();
    }

    private void CallFBFeed()
    {
        Dictionary<string, string[]> feedProperties = null;
        if(IncludeFeedProperties)
        {
            feedProperties = FeedProperties;
        }
        FB.Feed(
            toId: FeedToId,
            link: "placeholder",
            linkName: "placeholder",
            linkCaption: "placeholder",
            linkDescription: "placeholder",
            picture: "placeholder",
            mediaSource: FeedMediaSource,
            actionName: "placeholder",
            actionLink: "placeholder",
            reference: FeedReference,
            properties: feedProperties,
            callback: Callback
            );
    }

    public void Callback(FBResult result)
    {
        lastResponseTexture = null;
        if(result.Error != null)
            lastResponse = "Error Response:\n" + result.Error;
        else if(!ApiQuery.Contains("/picture"))
            lastResponse = "Success Response:\n" + result.Text;
        else
        {
            lastResponseTexture = result.Texture;
            lastResponse = "Success Response:\n";
        }
    }
}