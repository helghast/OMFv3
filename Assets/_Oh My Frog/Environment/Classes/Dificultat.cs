using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Dificultat {
    int id;
    float timer;
    public List<Granota> granotes;
    public List<Plataforma> plataformes;
    public List<Obstacle> obstacles;
    public List<Enemic> enemics;

    public Dificultat(int aid, float atimer) {
        this.id = aid;
        this.timer = atimer;
        granotes = new List<Granota>();
        plataformes = new List<Plataforma>();
        obstacles = new List<Obstacle>();
        enemics = new List<Enemic>();
    }

    public void addGranota(Granota granota) {
        granotes.Add(granota);
    }

    public void addPlataforma(Plataforma plataforma)
    {
        plataformes.Add(plataforma);
    }

    public void addObstacle(Obstacle obstacle)
    {
        obstacles.Add(obstacle);
    }

    public void addEnemic(Enemic enemic)
    {
        enemics.Add(enemic);
    }
}
