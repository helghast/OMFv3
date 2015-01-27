using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Comp_Coin_Manager : MonoBehaviour
{
    public float player_spawn_distance;
    public int pool_size;
    public GameObject coin_Prefab;
    public int enabled_coins;

    public List<cCoin> pool_Coins;
    public List<GameObject> list_Coin_Group;
    public Dictionary<string, Comp_Coin_Group> map_Coin_Group;
    public Transform Coin_Spawn_Point;
    public Transform Coin_Spawn_Limit_Point;

    private Transform mangos_container;

	// Use this for initialization
    void Awake()
    {
        enabled_coins = 0;
        mangos_container = GameObject.Find("MangosContainer").transform;

        map_Coin_Group = new Dictionary<string, Comp_Coin_Group>();
        for (int i = 0; i < list_Coin_Group.Count; ++i)
        {
            GameObject aux = (GameObject)Instantiate(list_Coin_Group[i], Vector3.zero, Quaternion.identity);
            Comp_Coin_Group comp_Coin_Group = aux.GetComponent<Comp_Coin_Group>();
            map_Coin_Group[comp_Coin_Group.name] = comp_Coin_Group;
            //Debug.Log("dictionary entry: " + comp_Coin_Group.name);
        }

        // POOL DE COINS
        pool_Coins = new List<cCoin>();
        for (int i = 0; i < pool_size; ++i)
        {
            GameObject go_coin = (GameObject)Instantiate(coin_Prefab, Vector3.zero, Quaternion.Euler(new Vector3(90, 0, 0)));
            pool_Coins.Add(go_coin.GetComponent<Comp_Coin>().coin);
            go_coin.transform.parent = mangos_container;

            pool_Coins[i].Disable();
            pool_Coins[i].index = i;
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < enabled_coins; ++i)
        {
            pool_Coins[i]._transform.Rotate(Vector3.up, 90 * Time.deltaTime, Space.World);
            bool visible = pool_Coins[i]._renderer.EP_IsVisibleFromCurrentCamera();
            // Si antes era visible y ahora ya no es visible según el nuevo view frustum, es que ha salido por la izquierda
            if (!visible && pool_Coins[i].isVisible)
            {
                DeleteCoin(i);
            }
            else
                pool_Coins[i].isVisible = visible;
        }
    }

    public void SpawnCoinGroup(string name, Vector3 position)
    {
        Vector3 spawn_position = Coin_Spawn_Point.position;

        if (GameLogicManager.Instance.GetPlayerPosition().x + 2 > Coin_Spawn_Limit_Point.position.x)
            return;

        Comp_Coin_Group comp_Coin_Group = map_Coin_Group[name];
        for (int i = 0; i < comp_Coin_Group.positions.Count; ++i)
        {
            cCoin coin = getFirstCoinAvaible();
            coin._transform.position = comp_Coin_Group.positions[i].position + spawn_position;
            coin.Enable();
        }
    }

    private cCoin getFirstCoinAvaible()
    {
        return pool_Coins[enabled_coins];
    }

    // Elimina (recicla) una moneda de la pool de monedas.
    public void DeleteCoin(int index)
    {
        enabled_coins--;
        pool_Coins[index].isVisible = pool_Coins[enabled_coins].isVisible;
        pool_Coins[index].isEnabled = pool_Coins[enabled_coins].isEnabled;
        pool_Coins[enabled_coins].Disable();
        pool_Coins[index]._transform.position = pool_Coins[enabled_coins]._transform.position;
    }
}
