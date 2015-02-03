using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using OMF_Environment;

public class Element2D
{
    public LAYER_ID layerID;
    public string name;
    public bool isActive;
    public bool isVisible;
    public Renderer renderer;

    public Comp_Environment_Element compElement;
    public float y;

    public Element2D(string name, float y, LAYER_ID layerId)
    {
        this.name = name;
        this.y = y;
        isActive = false;
        this.layerID = layerId;
    }

    public void setEnvironmentComponent(Comp_Environment_Element compElement)
    {
        this.compElement = compElement;
        renderer = this.compElement.renderer;
        setSpeed();
    }

    public void setSpeed()
    {
        switch(layerID)
        {
            case LAYER_ID.LAYER_0:
                compElement.Speed = EnvironmentManager.Instance.SpeedLayer0;
                break;
            case LAYER_ID.LAYER_1:
                compElement.Speed = EnvironmentManager.Instance.SpeedLayer1;
                break;
            case LAYER_ID.LAYER_2:
                compElement.Speed = EnvironmentManager.Instance.SpeedLayer2;
                break;
            case LAYER_ID.LAYER_3:
                compElement.Speed = EnvironmentManager.Instance.SpeedLayer3;
                break;
            case LAYER_ID.LAYER_4:
                compElement.Speed = EnvironmentManager.Instance.SpeedLayer4;
                break;
            case LAYER_ID.LAYER_5:
                compElement.Speed = EnvironmentManager.Instance.SpeedLayer5;
                break;
            case LAYER_ID.LAYER_6:
                compElement.Speed = EnvironmentManager.Instance.SpeedLayer6;
                break;
            case LAYER_ID.LAYER_7:
                compElement.Speed = EnvironmentManager.Instance.SpeedLayer7;
                break;
            case LAYER_ID.LAYER_8:
                compElement.Speed = EnvironmentManager.Instance.SpeedLayer8;
                break;
            case LAYER_ID.LAYER_9:
                compElement.Speed = EnvironmentManager.Instance.SpeedLayer9;
                break;
            
        }
        
    }

    //Totes les funcions d'activació cap al component
    public void spawn(Vector3 position)
    {
        isActive = true;
        compElement.gameObject.SetActive(true);
        compElement.transform.position = position;
        compElement.transform.parent = compElement.environmentTransform;
    }

    public void disable()
    {
        isActive = false;
        isVisible = false;
        compElement.gameObject.SetActive(false);
        compElement.transform.parent = compElement.poolTransform;
    }

    public void setVisible(bool value)
    {
        isVisible = value;
        compElement.setVisible(isVisible);
    }

}
