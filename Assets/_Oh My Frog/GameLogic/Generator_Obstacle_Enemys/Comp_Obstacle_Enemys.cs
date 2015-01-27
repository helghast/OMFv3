using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Comp_Obstacle_Enemys : MonoBehaviour {

    [Tooltip("Solo para elementos de debug")]
    public bool prueba;    
    [Tooltip("Solo para ver si obtiene correctamente un elemento EO de la pool")]
    public string temporal = "";
    [Tooltip("Solo para activar/desactivar los colliders de los EO de la pool")]
    public bool status_Colliders;

    private GameObject enemy_Obstacle_InScene = null;
    private GameObject posicion_Empty_Scene_Target = null;
    //
    public GameObject root_Things = null;
    //punto que va con el kappa donde se mueve un elemento de la pool
    public GameObject Empty_Target_Enemys_Obstacles = null;

    void Awake()
    {
        Obstacle_Enemys_Manager.Instance.Initialize();        
        //posicion_Empty_Scene_Target = GameObject.Find("Empty_Target_Enemys_Obstacles");

        if(root_Things == null)
        {
            root_Things = GameObject.Find("Scene_Core_GO");
        }
        if(Empty_Target_Enemys_Obstacles == null)
        {
            Empty_Target_Enemys_Obstacles = GameObject.Find("Empty_Target_Enemys_Obstacles");
        }
    }

	// Use this for initialization
	void Start () {
        //la primera vez se pone desde aqui. Las siguentes se puede hacer desde el Comp_Kappa_Controller.cs
        position_Random_Enemy_Obstacle();
	}
	
	// Update is called once per frame
	void Update () {
        //testeos d'esos
        for_Debug_Items_in_Scene();
        change_Status_Of_Colliders();

	}

    //obtener elemento de la pool y ponerlo en escena o volver a llevarlo a la pool
    public void position_Random_Enemy_Obstacle()
    {
        //obtener elemento de la pool random.
        enemy_Obstacle_InScene = Obstacle_Enemys_Manager.Instance.get_Obstacle_Enemy_From_List(Random.Range(0, Obstacle_Enemys_Manager.Instance.get_List_Size()));

        //posicionar en escena
        enemy_Obstacle_InScene.transform.position = Empty_Target_Enemys_Obstacles.transform.position;
    }

    //devolver enemy_obstacle que esta en escena a la pool
    public void return_To_Pool_Position()
    {
        enemy_Obstacle_InScene.transform.position = Obstacle_Enemys_Manager.Instance.get_Pool_Transform().position;
        enemy_Obstacle_InScene = null;
    }

    //solo para testeo
    private void for_Debug_Items_in_Scene()
    {
        if(prueba)
        {
            //esto solo para Debug
            temporal = enemy_Obstacle_InScene.name;
        }
    }

    //desactivar/activar colliders para pruebas. Usara isTrigger
    private void change_Status_Of_Colliders()
    {
        foreach(GameObject item in Obstacle_Enemys_Manager.Instance.get_Full_List())
        {
            item.collider.enabled = status_Colliders;
        }
    }
}
