using UnityEngine;
using System.Collections;
using Facebook.MiniJSON;
using System.Linq;

public class Comp_Facebook_Feed
{
    //unica instancia de la class
    private static Comp_Facebook_Feed instance = null;
    private static bool logged = false;

    public bool estaLogeado
    {
        get
        {
            return logged;
        }
        set
        {
            logged = value;
        }
    }

    //constructor privado
    private Comp_Facebook_Feed()
    {
        
    }

    //obtener unica instancia
    public static Comp_Facebook_Feed Initialize()
    {
        if(instance == null)
        {
            instance = new Comp_Facebook_Feed();
        }
        return instance;
    }

    //primer metodo a llamar de la class FB para poder trabajar con sesiones de Facebook
    public void CallFBInit()
    {
        FB.Init(OnInitComplete, OnHideUnity);
    }

    //metodo que devuelve cuando el estado de logeo como print
    private void OnInitComplete()
    {
        Debug.Log("FB.Init completado: esta el user logeado? " + FB.IsLoggedIn);
    }

    //metodo para no ocultar unity
    private void OnHideUnity(bool isGameShown)
    {
        Debug.Log("Se ve el juego? " + isGameShown);
    }

    //metodo que realiza el login a Facebook
    public void CallFBLogin()
    {
        FB.Login("email", LoginCallBack);
    }

    //metodo que realiza el callback.
    private void LoginCallBack(FBResult result)
    {
        if(result.Error != null)
        {
            Debug.Log("Logeado");
        }
        else
        {
            Debug.Log("Fallo!");
        }
        //estaLogeado = FB.IsLoggedIn;
    }

    //metodo que realiza un feed al facebook.
    public void makeFeed()
    {
        //FB.Init(FB.OnInitComplete, FB.OnHideUnity);  
        FB.Feed(
             toId: "",
             link: "https://www.facebook.com/ohmyfrog.surfrescue",
             linkName: "I am more awesome than anyone!!!!!!",
             linkCaption: "I'm playing OMF:SR",
             linkDescription: "Prueba",
             picture: "https://scontent-a-mad.xx.fbcdn.net/hphotos-xap1/v/t1.0-9/10665697_304545143067074_11542050484542634_n.jpg?oh=00c5ac85cc50b797cb78a997753fc87a&oe=5568FD5E",
             mediaSource: null,
             actionName: "",
             actionLink: "",
             reference: null,
             properties: null,
             callback: null
             );
    }
}
