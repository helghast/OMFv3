using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacle_Enemys_Manager
{
    //instancia privada y estatica de esta class
    private static Obstacle_Enemys_Manager instance = null;
    //lista que guardara los obstaculos_enemigos
    private List<GameObject> obstacle_Enemys_List = null;
    private List<Transform> obstacle_Enemys_Transforms = null;
    private GameObject pool_Enemys_Obstacles = null;

    //constructor privado
    private Obstacle_Enemys_Manager()
    {
        obstacle_Enemys_List = new List<GameObject>();
        obstacle_Enemys_Transforms = new List<Transform>();
    }

    //rellenar lista del objeto static
    public void Initialize()
    {
        try
        {            
            //debe existir un prefab en el escenario con todos los enemysobstacles
            pool_Enemys_Obstacles = GameObject.FindGameObjectWithTag("Pool_Obstacle_Enemys");

            //obtener los transforms de los child que estan Enumerados
            obstacle_Enemys_Transforms.AddRange(pool_Enemys_Obstacles.GetComponentsInChildren<Transform>(true));

            //eliminar el 0 pues es el gameEmpty root
            obstacle_Enemys_Transforms.RemoveAt(0);

            foreach(Transform item in obstacle_Enemys_Transforms)
            {
                //Debug.Log(item.gameObject.name);
                obstacle_Enemys_List.Add(item.gameObject);
            }
            obstacle_Enemys_Transforms.Clear();
            obstacle_Enemys_Transforms = null;
        }
        catch(System.ArgumentNullException e)
        {
            Debug.LogException(e);
        }
        catch(System.ArgumentOutOfRangeException e)
        {
            Debug.LogException(e);
        }
    }

    //getter de la instancia
    public static Obstacle_Enemys_Manager Instance
    {
        get
        {
            //mirar que no sea null. si lo es crear objeto
            if(instance == null)
            {
                instance = new Obstacle_Enemys_Manager();
            }
            //retornar objeto
            return instance;
        }
    }

    //setter para add gameobject a la lista
    public void add_Obstacle_Enemy(GameObject obstacle_enemy)
    {
        obstacle_Enemys_List.Add(obstacle_enemy);
    }

    //getter de objeto de la lista
    public GameObject get_Obstacle_Enemy_From_List(int position)
    {
        return obstacle_Enemys_List[position];
    }

    //getter de la lista completa
    public List<GameObject> get_Full_List()
    {
        return obstacle_Enemys_List;
    }

    //obtener tamaño de la lista
    public int get_List_Size()
    {
        return obstacle_Enemys_List.Count;
    }

    //obtener posicion de la pool
    public Transform get_Pool_Transform()
    {
        return pool_Enemys_Obstacles.transform;
    }
}
