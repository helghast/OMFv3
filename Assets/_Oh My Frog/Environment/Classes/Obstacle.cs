using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Obstacle {
    public string name;
    public float percent;
    public float baseTimer;

    public Obstacle(string name, float percent, float baseTimer)
    {
        this.name = name;
        this.percent = percent;
        this.baseTimer = baseTimer;
    }
}
