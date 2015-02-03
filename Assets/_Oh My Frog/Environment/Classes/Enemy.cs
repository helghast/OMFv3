using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Enemy {
    public string nom;
    public float percent;
    public float baseTimer;    //Timer for next platform, obstacle or enemy

    public Enemy(string nom, float percent, float baseTimer)
    {
        this.nom = nom;
        this.percent = percent;
        this.baseTimer = baseTimer;
    }

}
