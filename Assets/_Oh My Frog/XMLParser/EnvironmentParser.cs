using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using OMF_Environment;

public class EnvironmentParser : BaseXMLParser
{
    Escenari escenari;
    Dificultat dificultat;
    Obstacle obstacle;
    Enemic enemic;
    Plataforma plataforma;
    Granota granota;
    Layer layer;
    Element2D element2d;
    
    public EnvironmentParser() : base() { }

    public override void onStartElement(string elem, Dictionary<string, string> atts)
    {
        if (elem == "Escenari")
        {
            escenari = new Escenari(atts["nom"]);
        }
        else if (elem == "Dificultat")
        {
            int id = Convert.ToInt32(atts["id"]);
            float timer = Convert.ToSingle(atts["timer"]);
            dificultat = new Dificultat(id, timer);
            
        }
        else if (elem == "Obstacle")
        {
            int quantitat = Convert.ToInt32(atts["quantitat"]);
            obstacle = new Obstacle(atts["nom"], quantitat);
            dificultat.addObstacle(obstacle);
        }
        else if (elem == "Enemic")
        {
            enemic = new Enemic(atts["nom"]);
            dificultat.addEnemic(enemic);
        }
        else if (elem == "Plataforma")
        {
            int quantitat = Convert.ToInt32(atts["quantitat"]);
            plataforma = new Plataforma(atts["tipus"], quantitat);
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
                    //ERROR
                    break;
            }
        }
        else if (elem == "Element2D")
        {
            float y = Convert.ToSingle(atts["y"]);
            element2d = new Element2D(atts["nom"], y, layer.layerID);
            string path = "Environment/" + escenari.name + "/" + layer.name + "/" + element2d.name;
            GameObject go =  EPrefabManager.LoadPrefab(path, EnvironmentManager.Instance.transform_pool_environment);
            element2d.setEnvironmentComponent(go.GetComponent<Comp_Environment_Element>());
            layer.addElement(element2d);
        }
    }

    public override void onEndElement(string elem)
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
    }

}

