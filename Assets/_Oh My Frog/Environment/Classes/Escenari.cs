using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Escenari
{
    public string name;
    public List<Dificultat> dificultats;
    public List<Layer> layers;


    public Escenari(string name)
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
        foreach (Layer l in layers) {
            l.manageSpawner();
        }

    }
}

