using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Comp_Environment_Manager : MonoBehaviour {
    public float DISTANCE_TO_RESET_SYSTEM;
    public Transform environmentSpawnPoint_0_5;
    public float SKY_MULT;
    public float CLOUD_MULT;
    public float BACK_WATER_MULT;
    public float MID_WATER_MULT;
    public float FRONT_WATER_MULT; // GAME LAYER?
    public float VERTICAL_WATER_MULT;

    public float SPEED_DASH_MULTIPLIER;

    public float SPEED_LAYER_0;
    public float SPEED_LAYER_1;
    public float SPEED_LAYER_2;
    public float SPEED_LAYER_3;
    public float SPEED_LAYER_4;
    public float SPEED_LAYER_5;
    public float SPEED_LAYER_6;
    public float SPEED_LAYER_7;
    public float SPEED_LAYER_8;
    public float SPEED_LAYER_9;

    public float Z_LAYER_0;
    public float Z_LAYER_1;
    public float Z_LAYER_2;
    public float Z_LAYER_3;
    public float Z_LAYER_4;
    public float Z_LAYER_5;
    public float Z_LAYER_6;
    public float Z_LAYER_7;
    public float Z_LAYER_8;
    public float Z_LAYER_9;

    private float timerLevel;
    private Level currentScene;
    private int currentDificulty;
    private float timer2NextObject;
    private float DIST_TO_DISAPEAR = -15;

    public List<string> activeObstacles;
    public List<string> activeEnemys;



    void Awake() {
        activeObstacles = new List<string>();
        activeEnemys = new List<string>();

    }
    void Start() {
        EnvironmentManager.Instance.setEnvironment(this);
    }

    void Update() {
        // Aquí deberá ir el código que se encarga de spawnear los elementos de enviroment de manera aleatoria 
        // pero siguiendo algunas normas
        timerLevel += Time.deltaTime;
        manageElements3D();
        checkChangeDificulty();
        currentScene.manageSpawner();
    }

    void LateUpdate() {
        if (currentScene == null)
            return;

        foreach (Layer layer in currentScene.layers) {
            foreach (Element2D element in layer.Elements2D) {
                if (element.isActive()) {
                    if (element.compElement.transform.localPosition.x < DIST_TO_DISAPEAR) {
                        element.disable();
                    }

                }
            }
        }

        managerDeleteObstacles();
        managerDeleteEnemys();
    }

    private void managerDeleteObstacles() {
        List<string> obstacles4Delete = new List<string>();
        foreach (string nameObstacle in activeObstacles) {
            Comp_Environment_Obstacle co = EnvironmentManager.Instance.obstacles[nameObstacle];
            if (co.transform.localPosition.x < DIST_TO_DISAPEAR) {
                obstacles4Delete.Add(nameObstacle);
                co.disable();
            }
        }
        foreach (string name in obstacles4Delete) {
            activeObstacles.Remove(name);
        }
    }

    private void managerDeleteEnemys() {
        List<string> enemy4Delete = new List<string>();
        foreach (string nameEnemy in activeEnemys) {
            Comp_Base_Enemy comp_enemy = EnvironmentManager.Instance.enemys[nameEnemy];
            if (comp_enemy.transform.localPosition.x < DIST_TO_DISAPEAR) {
                enemy4Delete.Add(nameEnemy);
                comp_enemy.disable();
            }
        }
        foreach (string name in enemy4Delete) {
            activeEnemys.Remove(name);
        }
    }

    void checkChangeDificulty() {
        if (timerLevel > currentScene.dificultats[currentDificulty].timeDificulty) {
            if (currentDificulty < currentScene.dificultats.Count - 1) {
                ++currentDificulty;
                Debug.Log("canvio de dificultat a: " + currentDificulty);
            }
            timerLevel = 0;
        }
    }

    void manageElements3D() {
        timer2NextObject -= Time.deltaTime;
        if (timer2NextObject <= 0) {
            float timeDificulty = currentScene.dificultats[currentDificulty].timeDificulty;
            //TODO: El "2" se tiene que cambiar por el tiempo de plataforma / 2
            if (timerLevel < timeDificulty - 2) {   
                int random = UnityEngine.Random.Range(0, 100);
                if (random < currentScene.dificultats[currentDificulty].percentObstacles) {
                    timer2NextObject = currentScene.dificultats[currentDificulty].injectObstacle(random);
                } else {
                    timer2NextObject = currentScene.dificultats[currentDificulty].injectEnemy(random);
                }
            } else {
                timer2NextObject = currentScene.dificultats[currentDificulty].injectPlatform();
            }
        }
    }

    public void resetCurrentScene(Level scene) {
        currentScene = scene;
        currentDificulty = 0;
        timerLevel = 0;
        currentScene.initSpawn();
        timer2NextObject = 0;
    }
    public void addActiveObstacle(string name) {
        activeObstacles.Add(name);
    }

    public void addActiveEnemy(string name) {
        activeEnemys.Add(name);
    }
}
