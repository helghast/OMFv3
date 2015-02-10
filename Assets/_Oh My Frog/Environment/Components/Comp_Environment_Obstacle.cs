using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Comp_Environment_Obstacle : MonoBehaviour
{
    private string nameObstacle;
    public float speed;
    public float y;
    public Transform environmentTransform;
    private Transform injectTransform;
    private Transform poolTransform;

    void Awake()
    {
        environmentTransform = GameObject.Find("ObstaclesInScene").transform;
        injectTransform = GameObject.Find("EnvironmentSpawnPoint").GetComponent<Transform>();
        poolTransform = GameObject.Find("Pool_Obstacles").transform;
    }

    void Start()
    {
        speed = EnvironmentManager.Instance.SpeedLayer9;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mov = Vector3.right * speed * Time.deltaTime;
        transform.Translate(mov, Space.World);
    }

    public void spawn()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            transform.position = new Vector3(injectTransform.position.x, y, EnvironmentManager.Instance.ZLayer9);
            transform.parent = this.environmentTransform;
            EnvironmentManager.Instance.addActiveObstacle(nameObstacle);
        }
    }

    public void disable()
    {
        transform.parent = poolTransform;
        gameObject.SetActive(false);

    }

    public void setName(string name)
    {
        this.nameObstacle = name;
    }


}
