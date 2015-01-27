using UnityEngine;
using System.Collections;

public class Comp_Environment_Manager : MonoBehaviour
{
    public float DISTANCE_TO_RESET_SYSTEM;
    public Transform environmentSpawnPoint_0_5;
    public float SKY_MULT;
    public float CLOUD_MULT;
    public float BACK_WATER_MULT;
    public float MID_WATER_MULT;
    public float FRONT_WATER_MULT; // GAME LAYER?
    public float VERTICAL_WATER_MULT;

    /*
    public float BACK_BACKGROUND_MULT;
    public float MID_BACKGROUND_MULT;
    public float FRONT_BACKGROUND_MULT;
    */
    

    //public float FOREGORUND_MULT;


    public float SPEED_DASH_MULTIPLIER;

    public float SPEED_LAYER_0;
    public float SPEED_LAYER_1;
    public float SPEED_LAYER_2;
    public float SPEED_LAYER_3;
    public float SPEED_LAYER_4;
    public float SPEED_LAYER_5;
    public float SPEED_LAYER_6;
    public float SPEED_LAYER_7;
    public float SPEED_LAYER_8;
    public float SPEED_LAYER_9;

    public float Z_LAYER_0;
    public float Z_LAYER_1;
    public float Z_LAYER_2;
    public float Z_LAYER_3;
    public float Z_LAYER_4;
    public float Z_LAYER_5;
    public float Z_LAYER_6;
    public float Z_LAYER_7;
    public float Z_LAYER_8;
    public float Z_LAYER_9;

    void Awake()
    {
        Debug.Log("Awake Comp Environment Manager");
    }
	void Start ()
    {
        EnvironmentManager.Instance.setEnvironment(this);
	}

    void Update()
    {
        // Aquí deberá ir el código que se encarga de spawnear los elementos de enviroment de manera aleatoria 
        // pero siguiendo algunas normas
        EnvironmentManager.Instance.currentScene.manageSpawner();
    }

    void LateUpdate()
    {
        Escenari scene = EnvironmentManager.Instance.currentScene;
        if (scene == null)
            return;
        /*
        for (int i = 0; i < scene.layers.Count; ++i)
        {
            Layer layer = scene.layers[i];
            for (int j = 0; j < layer.Elements2D.Count; ++j)
            {
                bool visible = layer.Elements2D[j].renderer.EP_IsVisibleFromCurrentCamera();
                // Si antes era visible y ahora ya no es visible según el nuevo view frustum, es que ha salido por la izquierda
                if (!visible && layer.Elements2D[j].isVisible)
                {
                    //DeleteCoin(i);
                    layer.Elements2D[j].disable();
                }
                else
                    layer.Elements2D[j].setVisible(visible);
            }
        }*/
    }
}
