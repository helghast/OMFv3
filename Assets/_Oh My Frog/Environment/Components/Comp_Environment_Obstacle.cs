using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Comp_Environment_Obstacle : MonoBehaviour
{
    public float speed;
    public float y;
    public Transform environmentTransform;
    public Transform poolTransform;

    void Awake()
    {
        environmentTransform = GameObject.Find("EnvironmentScene").transform;
        poolTransform = GameObject.Find("Pool_Obstacles").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mov = Vector3.right * speed * Time.deltaTime;
        transform.Translate(mov, Space.World);
    }


}
