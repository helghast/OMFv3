using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Enemy {
    public string name;
    public float percent;
    public float baseTimer;    //Timer for next platform, obstacle or enemy

    public Enemy(string name, float percent, float baseTimer)
    {
        this.name = name;
        this.percent = percent;
        this.baseTimer = baseTimer;
    }

}
