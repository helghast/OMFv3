using UnityEngine;
using System.Collections;

[System.Serializable]
public class cCoin
{
    public Comp_Coin comp_coin;
    public bool isEnabled;
    public bool isVisible;
    public int index;
    public Transform _transform;
    public Renderer _renderer;
    public Collider _collider;
    public GameObject _gameObject;

    public cCoin(Comp_Coin comp_coin)
    {
        this.comp_coin = comp_coin;
        _transform = comp_coin.transform;
        _renderer = comp_coin.renderer;
        _collider = comp_coin.collider;
        _gameObject = comp_coin.gameObject;
    }

    public void Enable()
    {
        _renderer.enabled = true;
        _collider.enabled = true;
        isEnabled = true;
        isVisible = false;
        comp_coin.comp_Coin_Manager.enabled_coins++;
        Debug.Log("Coin enabled!");
    }

    // Marca la moneda como inactiva
    public void Disable()
    {
        isEnabled = false;
        isVisible = false;
        _renderer.enabled = false;
        _collider.enabled = false;
    }

    // Elimina (reciclandola) la moneda de la escena devolviendola a la pool
    public void DeleteCoin()
    {
        //Disable();
        comp_coin.comp_Coin_Manager.DeleteCoin(index);
    }
}
