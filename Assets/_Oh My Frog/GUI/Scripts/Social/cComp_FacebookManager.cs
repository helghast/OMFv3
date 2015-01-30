using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Facebook.MiniJSON;

public class cComp_FacebookManager : MonoBehaviour {

    private static cComp_FacebookManager instance = null;
    private Action<bool> _loginCallback;
    bool _canpostTofb;
    internal PostData _postData { get; set; }

    public object LoginAndPostImageCallback { get; set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //constructor privado
    private cComp_FacebookManager()
    {

    }

    public static cComp_FacebookManager Instance()
    {
        if(instance == null)
        {
            instance = new cComp_FacebookManager();
        }
        return instance;
    }

    //metodos de facebook class
    //metodo init llamado una sola vez y siempre al inicio de usar algo del FB
    public void Init(Action<bool> callback)
    {
        //inicializar static facebook del sdk
        _loginCallback = callback;
        //FB es la class base de Facebook.
        FB.Init(InitCallback, null, null);
    }

    //si se consigue llamar correctamente a Facebook, el Action sera exitoso = true y estara disponible el login
    private void InitCallback()
    {
        _loginCallback(true);
    }



    //logins, tipos
    //login con readpermisions. permite leer datos basicos del user (username, id, email..)
    public void LoginWithReadPermission(Action<bool> callback)
    {
        _loginCallback = callback;
        FB.Login("email", LoginReadPermissions);
    }

    //login con postpermisions. llama a los publishactions que permite postear cosas como el usuario

    //login con ambos permisos. primero se logea con el readpermisions, y luego le permite postear en su nombre

    //para decirnos si se logea o no.
    private void LoginReadPermissions(FBResult result)
    {
        //si hay algun error en el login
        if(result.Error != null)
        {
            //decir que no hay exito en el logeo
            _loginCallback(false);
        }
        Debug.Log(result.Error);
        Debug.Log(result.Text);

        //si ya estamos logeados es necesario preguntar por todos nuestros permisos
        if(FB.IsLoggedIn)
        {
            //si no esta logeado se hace un query por peticion GET. 
            //me/permissions pide los permisos del user sobre la app
            FB.API("/me/permissions", Facebook.HttpMethod.GET, PermissionsCallback);
        }
        else
        {
            //si no se deslogea el user se queda una sesion abierta. Si se intenta logear de nuevo crashea.
            FB.Logout();
            FB.Init(InitCompleteCallback, null, null);
        }
    }

    private void InitCompleteCallback()
    {
        throw new NotImplementedException();
    }

    private void PermissionsCallback(FBResult response) 
    {
        if(response.Text.Contains("publish_actions"))
        {
            CanPostToFacebook = true;
        }
        else
	    {
            CanPostToFacebook = false;
	    }
        Debug.Log("Publish Permission Accepted? [" + CanPostToFacebook + "]");
        GetUserProfile();
    }

    //devolver si la app puede postear en nombre del user
    public bool CanPostToFacebook
    {
        get
        {
            return _canpostTofb;
        }
        set
        {
            _canpostTofb = value;
        }
    }

    //obtener la info basica del user
    private void GetUserProfile()
    {
        FB.API("/me?fields=id,name,first_name,last_name,locale", Facebook.HttpMethod.GET, GetUserProfileCall);
    }

    private void GetUserProfileCall(FBResult response)
    {
        if(response.Error != null)
        {
            if(_loginCallback != null)
            {
                _loginCallback(false);
            }
            return;
        }
        Debug.Log(response.Error);
        Debug.Log(response.Text);

        Dictionary<string, object> a = Json.Deserialize(response.Text) as Dictionary<string, object>;
        Debug.Log(a["id"].ToString());
        Debug.Log(a["first_name"].ToString() + " " + a["last_name"].ToString());

        if(_loginCallback != null)
        {
            _loginCallback(true);
        }
    }

    public void PostMessage(string message, Action<string> callback)
    {
        if(!IsSessionValid())
        {
            LoginWithBothPermissions(LoginAndPostMessage);
        }
        if(!CanPostToFacebook)
        {
            callback("cp");
            return;
        }

        _postData = new PostData(message, callback);

        Dictionary<string, string> a = new Dictionary<string, string>();
        a.Add("message", message);
        FB.API("/me/feed", Facebook.HttpMethod.POST, PostResponse, a);
    }

    private void LoginWithBothPermissions(Action<bool> LoginAndPostMessage)
    {
        throw new NotImplementedException();
    }

    private void PostResponse(FBResult result)
    {
        Dictionary<string, object> s = Json.Deserialize(result.Text) as Dictionary<string, object>;
        object a;

        if (result.Error != null)
	    {
		    _postData.Callback(result.Error);
            return;
	    }
        s.TryGetValue("cancelled", out a);

        //si a != null significa que el usuario ha cancelado el request
        if (a != null)
	    {
            _postData.Callback("cancelled");
        }
        else
        {
            _postData.Callback("");
        }
    }

    public bool IsSessionValid()
    {
        return FB.IsLoggedIn;
    }

    private void LoginAndPostMessage(bool result)
    {
        if(result)
        {
            PostMessage(_postData.Message, _postData.Callback);
        }
        else
        {
            _postData.Callback("error");
        }
    }

}
