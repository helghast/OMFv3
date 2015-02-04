using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Level
{
    public string name;
    public List<Dificultat> dificultats;
    public List<Layer> layers;


    public Level(string name)
    {
        this.name = name;
        dificultats = new List<Dificultat>();
        layers = new List<Layer>();
    }

    public void addDificultat(Dificultat d)
    {
        dificultats.Add(d);
    }

    public void addLayer(Layer layer)
    {
        layers.Add(layer);
    }

    public void manageSpawner()
    {
        foreach (Layer l in layers)
        {
            l.manageElements2D();
        }

    }


    public void initSpawn()
    {
        foreach (Layer l in layers)
        {
            l.initSpawn();
        }

    }
}

