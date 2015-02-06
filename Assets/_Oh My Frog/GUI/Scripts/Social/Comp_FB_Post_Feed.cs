using UnityEngine;
using System.Collections;

public class Comp_FB_Post_Feed : MonoBehaviour {

    //crear instancia para facebook
    void Awake()
    {
        Comp_Facebook_Feed.Initialize();
    }

    //iniciar fb.init
    void Start()
    {
        Comp_Facebook_Feed.Initialize().CallFBInit();
    }

    void Update()
    {

    }


    void OnGUI()
    {        
        if(GUI.Button(new Rect(10, 10, 50, 50), "Click"))
        {
            Comp_Facebook_Feed.Initialize().CallFBLogin();
        }
        if(Comp_Facebook_Feed.Initialize().estaLogeado == true)
        {
            if(GUI.Button(new Rect(70, 70, 50, 50), "Feed"))
            {
                Comp_Facebook_Feed.Initialize().makeFeed();
            }
        }
    }
}
