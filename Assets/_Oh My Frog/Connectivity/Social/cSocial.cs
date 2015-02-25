using UnityEngine;
using System.Collections;

public class cSocial
{
    public cSocial()
    {
        Debug.Log(ConnectivityManager.SocialAuthenticate() ? "Connected" : "Disconnected");
    }
	
}
