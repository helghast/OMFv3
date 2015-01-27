using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentManager
{
    //-----------------------------------------------
    //  STATIC ATRIBUTES
    //-----------------------------------------------
    private static EnvironmentManager instance;
    public static EnvironmentManager Instance
    {
        get
        {
            return instance;
        }
    }

    //-----------------------------------------------
    //  CONSTRUCTOR INFO
    //-----------------------------------------------
    private EnvironmentManager()
    {
        
    }
    //-----------------------------------------------
    //  ATRIBUTES
    //-----------------------------------------------
    private Comp_Environment_Manager comp_env_manager;

    public Escenari currentScene;


    public Transform transform_pool_environment;
    private Comp_Debug comp_debug;
    //-----------------------------------------------
    //  FUNCTIONS
    //-----------------------------------------------
    public void setEnvironment(Comp_Environment_Manager _comp_env_manager)
    {
        comp_env_manager = _comp_env_manager;
    }

    public void Initialize()
    {
        comp_env_manager = GameObject.Find("Environment_Manager").GetComponent<Comp_Environment_Manager>();
        transform_pool_environment = GameObject.Find("Pool_Environment").GetComponent<Transform>();
        comp_debug = GameObject.Find("Debug").GetComponent<Comp_Debug>();
        
        bool is_ok = loadXML("Escenario0");
        if (is_ok)
        {
           
        }
    }

    //-----------------------------------------------
    //  STATIC METHODS
    //-----------------------------------------------
    public static void CreateManager()
    {
        if (instance == null)
        {
            instance = new EnvironmentManager();
            Instance.Initialize();
        }
    }

    //-----------------------------------------------
    //  GETTERS
    //-----------------------------------------------

    // --------------------------------------------------------------------------------------------
    // WATER
    //---------------------------------------------------------------------------------------------
    public float Water_FLayer_Speed
    {
        get {return comp_env_manager.FRONT_WATER_MULT;}
    }

    public float Water_BLayer_Speed
    {
        get {return comp_env_manager.BACK_WATER_MULT;}
    }

    public float Water_MLayer_Speed
    {
        get {return comp_env_manager.MID_WATER_MULT;}
    }

    public float Water_FLayer_Speed_Dash
    {
        get { return comp_env_manager.FRONT_WATER_MULT * comp_env_manager.SPEED_DASH_MULTIPLIER; }
    }

    public float Water_BLayer_Speed_Dash
    {
        get { return comp_env_manager.BACK_WATER_MULT * comp_env_manager.SPEED_DASH_MULTIPLIER; }
    }

    public float Water_MLayer_Speed_Dash
    {
        get { return comp_env_manager.MID_WATER_MULT * comp_env_manager.SPEED_DASH_MULTIPLIER; }
    }

    // --------------------------------------------------------------------------------------------
    // SKY & CLOUDS
    //---------------------------------------------------------------------------------------------
    public float Sky_Speed
    {
        get {return comp_env_manager.SKY_MULT;}
    }

    public float Clouds_Speed
    {
        get {return comp_env_manager.CLOUD_MULT;}
    }

    public float Sky_Speed_Dash
    {
        get {return comp_env_manager.SKY_MULT * comp_env_manager.SPEED_DASH_MULTIPLIER;}
    }

    public float Clouds_Speed_Dash
    {
        get {return comp_env_manager.CLOUD_MULT * comp_env_manager.SPEED_DASH_MULTIPLIER;}
    }
    //------------------------------------------------------------------------------------
    // Layers
    //------------------------------------------------------------------------------------
    public float ZLayer0 { get { return comp_env_manager.Z_LAYER_0; } }
    public float ZLayer1 { get { return comp_env_manager.Z_LAYER_1; } }
    public float ZLayer2 { get { return comp_env_manager.Z_LAYER_2; } }
    public float ZLayer3 { get { return comp_env_manager.Z_LAYER_3; } }
    public float ZLayer4 { get { return comp_env_manager.Z_LAYER_4; } }
    public float ZLayer5 { get { return comp_env_manager.Z_LAYER_5; } }
    public float ZLayer6 { get { return comp_env_manager.Z_LAYER_6; } }
    public float ZLayer7 { get { return comp_env_manager.Z_LAYER_7; } }
    public float ZLayer8 { get { return comp_env_manager.Z_LAYER_8; } }
    public float ZLayer9 { get { return comp_env_manager.Z_LAYER_9; } }

    //------------------------------------------------------------------------------------
    // Layers
    //------------------------------------------------------------------------------------
    public float SpeedLayer0 { get { return comp_env_manager.SPEED_LAYER_0; } }
    public float SpeedLayer1 { get { return comp_env_manager.SPEED_LAYER_1; } }
    public float SpeedLayer2 { get { return comp_env_manager.SPEED_LAYER_2; } }
    public float SpeedLayer3 { get { return comp_env_manager.SPEED_LAYER_3; } }
    public float SpeedLayer4 { get { return comp_env_manager.SPEED_LAYER_4; } }
    public float SpeedLayer5 { get { return comp_env_manager.SPEED_LAYER_5; } }
    public float SpeedLayer6 { get { return comp_env_manager.SPEED_LAYER_6; } }
    public float SpeedLayer7 { get { return comp_env_manager.SPEED_LAYER_7; } }
    public float SpeedLayer8 { get { return comp_env_manager.SPEED_LAYER_8; } }
    public float SpeedLayer9 { get { return comp_env_manager.SPEED_LAYER_9; } }

    /**
     * Cargar xml de una escena
     * xmlName: nombre de la escena
     */
    public bool loadXML(string xmlName)
    {
        EnvironmentParser environmentParser = new EnvironmentParser();
        TextAsset textAsset = (TextAsset)Resources.Load("Environment/XMLs/"+ xmlName);
        //return environmentParser.xmlParseFile(Application.dataPath + "/_Oh My Frog/Environment/XML/" + xmlName + ".xml");
        return environmentParser.xmlParseFile(textAsset);
    }

    public void setScene(Escenari scene)
    {
        currentScene = scene;
    }
}
