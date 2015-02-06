using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using OMF_Environment;
using OMF_Errors;

public class EnvironmentParser : BaseXMLParser
{
    Level escenari;
    Dificultat dificultat;
    Obstacle obstacle;
    Enemy enemic;
    Platform plataforma;
    Granota granota;
    Layer layer;
    Element2D element2d;
    float percent;
    
    public EnvironmentParser() : base() { }

    public override ErrorCode onStartElement(string elem, Dictionary<string, string> atts)
    {
        if (elem == "Escenari")
        {
            escenari = new Level(atts["nom"]);
        }
        else if (elem == "Dificultat")
        {
            int id = Convert.ToInt32(atts["id"]);
            float timer = Convert.ToSingle(atts["timer"]);
            dificultat = new Dificultat(id, timer);
            percent = 0;
        }
        else if (elem == "Obstacle")
        {
            string name = atts["nom"];
            float btimer = Convert.ToSingle(atts["timer"]);
            float perc = Convert.ToSingle(atts["percent"]);
            percent += perc;
            obstacle = new Obstacle(name, perc, btimer);
            if (ErrorCode.IS_OK != loadPrefabObstacle(name))
            {
                return ErrorCode.GO_NOT_FOUND;
            }
            /*if (!EnvironmentManager.Instance.obstacles.ContainsKey(name))
            {
                string path = "Environment/" + escenari.name + "/Obstacles/" + name + "/Prefab/" + name;
                GameObject go = EPrefabManager.LoadPrefab(path, EnvironmentManager.Instance.transform_pool_obstacles);
                if (go == null)
                    return ErrorCode.GO_NOT_FOUND;
                EnvironmentManager.Instance.obstacles[name] = go.GetComponent<Comp_Environment_Obstacle>();
            }*/
            dificultat.addObstacle(obstacle);
        }
        else if (elem == "Enemic")
        {
            string name = atts["nom"];
            float btimer = Convert.ToSingle(atts["timer"]);
            float perc = Convert.ToSingle(atts["percent"]);
            percent += perc;
            enemic = new Enemy(name, perc, btimer);
            /*if (ErrorCode.IS_OK != loadPrefabEnemy(name))
            {
                return ErrorCode.GO_NOT_FOUND;
            }*/
            dificultat.addEnemic(enemic);
        }
        else if (elem == "Plataforma")
        {
            plataforma = new Platform(atts["tipus"]);
            dificultat.addPlataforma(plataforma);
        }
        else if (elem == "Granota")
        {
            float percent = Convert.ToSingle(atts["percent"]);
            granota = new Granota(atts["color"], percent);
            dificultat.addGranota(granota);
        }
        else if (elem == "Layer")
        {
            string name = atts["id"];
            int idLayer = Convert.ToInt32( name.ToCharArray()[0]) - 48; //OJO si hi ha més de 10 layers
            LAYER_ID layerId = (LAYER_ID)idLayer;
            switch (layerId)
            {
                case LAYER_ID.LAYER_0:
                    layer = new Layer(name, EnvironmentManager.Instance.ZLayer0, layerId);
                    break;
                case LAYER_ID.LAYER_1:
                    layer = new Layer(name, EnvironmentManager.Instance.ZLayer1, layerId);
                    break;
                case LAYER_ID.LAYER_2:
                    layer = new Layer(name, EnvironmentManager.Instance.ZLayer2, layerId);
                    break;
                case LAYER_ID.LAYER_3:
                    layer = new Layer(name, EnvironmentManager.Instance.ZLayer3, layerId);
                    break;
                case LAYER_ID.LAYER_4:
                    layer = new Layer(name, EnvironmentManager.Instance.ZLayer4, layerId);
                    break;
                case LAYER_ID.LAYER_5:
                    layer = new Layer(name, EnvironmentManager.Instance.ZLayer5, layerId);
                    break;
                case LAYER_ID.LAYER_6:
                    layer = new Layer(name, EnvironmentManager.Instance.ZLayer6, layerId);
                    break;
                case LAYER_ID.LAYER_7:
                    layer = new Layer(name, EnvironmentManager.Instance.ZLayer7, layerId);
                    break;
                case LAYER_ID.LAYER_8:
                    layer = new Layer(name, EnvironmentManager.Instance.ZLayer8, layerId);
                    break;
                case LAYER_ID.LAYER_9:
                    layer = new Layer(name, EnvironmentManager.Instance.ZLayer9, layerId);
                    break;
                default:
                    return ErrorCode.LAYER_NOT_DEFINED;
            }
        }
        else if (elem == "Element2D")
        {
            float y = Convert.ToSingle(atts["y"]);
            element2d = new Element2D(atts["nom"], y, layer.layerID);
            string path = "Environment/" + escenari.name + "/" + layer.name + "/" + element2d.name;
            GameObject go =  EPrefabManager.LoadPrefab(path, EnvironmentManager.Instance.transform_pool_environment);
            if (go == null)
                return ErrorCode.GO_NOT_FOUND;
            element2d.setEnvironmentComponent(go.GetComponent<Comp_Environment_Element>());
            layer.addElement(element2d);
        }
        return ErrorCode.IS_OK;
    }

    public override ErrorCode onEndElement(string elem)
    {
        if (elem == "Dificultat")
        {
            escenari.addDificultat(dificultat);
            dificultat = null;
        }
        else if (elem == "Layer")
        {
            escenari.addLayer(layer);
            layer = null;
        }
        else if (elem == "Escenari")
        {
            EnvironmentManager.Instance.setScene(escenari);
        }
        else if (elem == "Escenaris")
        {
            //Eliminar tot
            obstacle = null;
            enemic = null;
            plataforma = null;
            granota = null;
            element2d = null;
        }
        return ErrorCode.IS_OK;
    }



    private ErrorCode loadPrefabObstacle(string name)
    {
        if (!EnvironmentManager.Instance.obstacles.ContainsKey(name))
        {
            string path = "Environment/" + escenari.name + "/Obstacles/" + name + "/Prefab/" + name;
            GameObject go = EPrefabManager.LoadPrefab(path, EnvironmentManager.Instance.transform_pool_obstacles);
            if (go == null)
                return ErrorCode.GO_NOT_FOUND;
            EnvironmentManager.Instance.obstacles[name] = go.GetComponent<Comp_Environment_Obstacle>();
        }
        return ErrorCode.IS_OK;
    }

    private ErrorCode loadPrefabEnemy(string name)
    {
        if (!EnvironmentManager.Instance.obstacles.ContainsKey(name))
        {
            string path = "Environment/" + escenari.name + "/Enemys/" + name + "/Prefab/" + name;
            GameObject go = EPrefabManager.LoadPrefab(path, EnvironmentManager.Instance.transform_pool_enemys);
            if (go == null)
                return ErrorCode.GO_NOT_FOUND;
            EnvironmentManager.Instance.obstacles[name] = go.GetComponent<Comp_Environment_Obstacle>();
        }
        return ErrorCode.IS_OK;
    }
}