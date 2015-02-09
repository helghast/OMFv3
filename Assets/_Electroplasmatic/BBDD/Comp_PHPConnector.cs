using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using Utils.BBDD;

public class Comp_PHPConnector : MonoBehaviour
{
    public string BBDD_Password;
    public string BBDD_php_submit_url;
    public string BBDD_php_request_url;

	// Use this for initialization
	void Start ()
    {

	}


    // -------------------------------------------------------------------------------------
    // PUBLIC METHODS
    // -------------------------------------------------------------------------------------
    public void PostScore(string name, int value)
    {
        StartCoroutine(postScore(name, value));
    }

    // -------------------------------------------------------------------------------------
    // PRIVATE METHODS
    // -------------------------------------------------------------------------------------
    private IEnumerator postScore(string name, int value)
    {
        string hash = Utils_BBDD.Md5Sum(name + value + BBDD_Password);
        string post_url = BBDD_php_submit_url + "name=" + WWW.EscapeURL(name) + "&value=" + value + "&hash=" + hash;

        // Enviamos la petición de ejecución al PHP y esperamos la respuesta
        WWW post_result = new WWW(post_url);
        // Crea punto de retorno aquí para cuando el objeto esté listo seguir ejecutando el código desde este punto.
        yield return post_result;

        if (post_result != null)
        {
            Debug.Log("<ERROR> - Comp_PHPConnector/postScore/" + post_result.error);
        }
    }

    private IEnumerator getScore()
    {
        WWW get_result = new WWW(BBDD_php_request_url);
        yield return get_result;

        if (get_result.error != null)
        {
            Debug.Log("<ERROR> - Comp_PHPConnector/postScore/" + get_result.error);
        }
        else
        {
            //GameManager.SetCoins((int)get_result.text);

        }
    }
}
