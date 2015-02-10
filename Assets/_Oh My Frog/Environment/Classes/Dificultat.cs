using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Dificultat {
    int id;
    public float timeDificulty;
    public List<Granota> granotes;
    public List<Platform> plataformes;
    public List<Obstacle> obstacles;
    public List<Enemy> enemics;
    public float percentObstacles;
    private float globalPercent;

    public Dificultat(int aid, float atimer) {
        this.id = aid;
        this.timeDificulty = atimer;
        granotes = new List<Granota>();
        plataformes = new List<Platform>();
        obstacles = new List<Obstacle>();
        enemics = new List<Enemy>();
        percentObstacles = 0;
        globalPercent = 0;
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
        percentObstacles += obstacle.percent;
        globalPercent = percentObstacles;
        obstacle.percent = percentObstacles;
        obstacles.Add(obstacle);
    }

    public void addEnemic(Enemy enemic)
    {
        globalPercent += enemic.percent;
        enemic.percent = globalPercent;
        enemics.Add(enemic);
    }


    public float injectEnemy(int random) {
        foreach (Enemy enemy in enemics)
        {
            if (random < enemy.percent)
            {
                Debug.Log("inject enemy " + enemy.name);
                EnvironmentManager.Instance.enemys[enemy.name].spawn();
                return enemy.baseTimer;
            }
        }
        return 0;
    }

    public float injectObstacle(int random)
    {

        foreach (Obstacle obstacle in obstacles)
        {
            if (random < obstacle.percent)
            {
                Debug.Log("inject obstacle " + obstacle.name);
                EnvironmentManager.Instance.obstacles[obstacle.name].spawn();
                return obstacle.baseTimer;
            }
        }
        return 0;
    }


    public float injectPlatform()
    {
        if (plataformes.Count != 0) { 
            int idxPlatform = UnityEngine.Random.Range(0, plataformes.Count-1);
            Debug.Log("inject platform" + plataformes[idxPlatform].type + idxPlatform.ToString());
            return 3;
        }
        return 0;
    }
}
