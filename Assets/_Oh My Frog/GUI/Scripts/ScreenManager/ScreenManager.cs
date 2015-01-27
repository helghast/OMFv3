using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour {

	// Use this for initialization
    void Start()
    {
        /*//deprecated
        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            iPhoneSettings.screenOrientation = iPhoneScreenOrientation.LandscapeLeft;
            
        }*/
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        /*ios resolutions
         * iphone5=1136x640
         * iphone6=1334x750
         * iphone6+=2208 x 1242
         * ipad=1024 x 768
         * iPad Retina=2048 x 1536
         */
        Resolution[] resolutions = Screen.resolutions;
        foreach(Resolution res in resolutions)
        {
            print(res.width + "x" + res.height);
        }
        Screen.SetResolution(resolutions[0].width, resolutions[0].height, true);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
