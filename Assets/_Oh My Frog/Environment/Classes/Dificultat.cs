using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Dificultat {
    int id;
    public float timer;
    public List<Granota> granotes;
    public List<Platform> plataformes;
    public List<Obstacle> obstacles;
    public List<Enemy> enemics;

    public Dificultat(int aid, float atimer) {
        this.id = aid;
        this.timer = atimer;
        granotes = new List<Granota>();
        plataformes = new List<Platform>();
        obstacles = new List<Obstacle>();
        enemics = new List<Enemy>();
    }

    public void addGranota(Granota granota) {
        granotes.Add(granota);
    }

    public void addPlataforma(Platform plataforma)
    {
        plataformes.Add(plataforma);
    }

    public void addObstacle(Obstacle obstacle)
    {
        obstacles.Add(obstacle);
    }

    public void addEnemic(Enemy enemic)
    {
        enemics.Add(enemic);
    }
}
