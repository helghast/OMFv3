﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using OMF_Environment;

public class Layer
{
    public LAYER_ID layerID;
    public string name;
    public float z;
    private List<Element2D> elements2D;
    private Transform transformInject;
    private float timer;

    public static int MAX_ITERATIONS = 5;

    public Layer(string name, float z, LAYER_ID id)
    {
        this.name = name;
        this.z = z;
        this.layerID = id;
        elements2D = new List<Element2D>();
        transformInject = GameObject.Find("EnvironmentSpawnPoint").GetComponent<Transform>();
        timer = -1;
    }

    public void addElement(Element2D element)
    {
        elements2D.Add(element);
    }

    public void manageSpawner()
    {
        //Provisional
        //comp_Environment_manager.speed  i element.find<comp_environment_element>().length + timer random (1, 4)
        if (timer <= 0.0f)
        {
            bool trobat = false;
            for (int i = 0; i < MAX_ITERATIONS && !trobat; i++)
            {
                int idx = UnityEngine.Random.Range(0, elements2D.Count);
                if (!elements2D[idx].isActive)
                {
                    Vector3 v = new Vector3(transformInject.position.x, elements2D[idx].y, z);
                    elements2D[idx].spawn(v);
                    trobat = true;
                    float dist = elements2D[idx].compElement.length + UnityEngine.Random.Range(elements2D[idx].compElement.minDist2NextElement, elements2D[idx].compElement.maxDist2NextElement);
                    timer = dist  / -elements2D[idx].compElement.Speed;  //m / (m/s) = s;
                }
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
       
        
    }





    //-----------------------------------------------
    //  PROPERTIES
    //-----------------------------------------------
    public List<Element2D> Elements2D
    {
        get {return elements2D;}
    }

}
