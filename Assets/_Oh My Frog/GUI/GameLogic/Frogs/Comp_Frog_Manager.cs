using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Comp_Frog_Manager : MonoBehaviour
{
    public List<cFrog> pool_frogs;
    public GameObject frog_Prefab;
    public Transform Pelican_Frog_Emissor;
    public Transform frogs_container;
    public int enabled_frogs;
    public int pool_size;

    void Awake()
    {
        enabled_frogs = 0;
        /*frogs_container = GameObject.Find("FrogsContainer").transform;

        // POOL DE FROGS
        pool_frogs = new List<cFrog>();
        for (int i = 0; i < pool_size; ++i)
        {
            GameObject go_frog = (GameObject)Instantiate(frog_Prefab, Vector3.zero, Quaternion.Euler(new Vector3(-15, 150, 0)));
            pool_frogs.Add(go_frog.GetComponent<Comp_IA_Frog>().frog);
            go_frog.transform.parent = frogs_container;

            pool_frogs[i].Disable();
            pool_frogs[i].index = i;
        }*/
    }
	// Use this for initialization
	void Start ()
    {
        frogs_container = GameObject.Find("FrogsContainer").transform;

        // POOL DE FROGS
        pool_frogs = new List<cFrog>();
        for(int i = 0; i < pool_size; ++i) {
            GameObject go_frog = (GameObject) Instantiate(frog_Prefab, Vector3.zero, Quaternion.Euler(new Vector3(-15, 150, 0)));
            pool_frogs.Add(go_frog.GetComponent<Comp_IA_Frog>().frog);
            go_frog.transform.parent = frogs_container;

            pool_frogs[i].Disable();
            pool_frogs[i].index = i;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void LateUpdate()
    {
        for (int i = 0; i < enabled_frogs; ++i)
        {
            //pool_frogs[i]._transform.Rotate(Vector3.up, 90 * Time.deltaTime, Space.World);
            bool visible = pool_frogs[i]._rendererBody.EP_IsVisibleFromCurrentCamera();
            // Si antes era visible y ahora ya no es visible según el nuevo view frustum, es que ha salido por la izquierda
            if (!visible && pool_frogs[i].isVisible)
            {
                DeleteFrog(i);
            }
            else
                pool_frogs[i].isVisible = visible;
        }
    }

    private cFrog getFirstFrogAvaible()
    {
        return pool_frogs[enabled_frogs];
    }

    // Elimina (recicla) una moneda de la pool de monedas.
    public void DeleteFrog(int index)
    {
        enabled_frogs--;
        pool_frogs[index].isVisible = pool_frogs[enabled_frogs].isVisible;
        pool_frogs[index].isEnabled = pool_frogs[enabled_frogs].isEnabled;
        pool_frogs[enabled_frogs].Disable();
        pool_frogs[index]._transform.position = pool_frogs[enabled_frogs]._transform.position;
    }

    public void SpawnFrog(string name, Vector3 position)
    {
        //Vector3 spawn_position = position;
        Vector3 spawn_position = Pelican_Frog_Emissor.position;
        /*
        if (GameLogicManager.Instance.GetPlayerPosition().x + 2 > position.x)
            return;*/

        cFrog frog = getFirstFrogAvaible();
        frog._transform.position = spawn_position;
        frog._transform.parent = Pelican_Frog_Emissor;
        frog.Enable();
    }
}
