//#define IOS_DEVELOPMENT
#define ANDROID_DEVELOPMENT

using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using GooglePlayGames;

public class PickupManager
{
    //-----------------------------------------------
    //  CONSTRUCTOR INFO
    //-----------------------------------------------
   // private 
    private static PickupManager instance;
    private PickupManager()
    {

    }

    public static PickupManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PickupManager();
            }
            return instance;
        }
    }
}