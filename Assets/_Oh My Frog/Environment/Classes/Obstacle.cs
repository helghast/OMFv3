using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Obstacle {
    public string nom;
    public float percent;
    public float baseTimer;

    public Obstacle(string nom, float percent, float baseTimer)
    {
        this.nom = nom;
        this.percent = percent;
        this.baseTimer = baseTimer;
    }
}
