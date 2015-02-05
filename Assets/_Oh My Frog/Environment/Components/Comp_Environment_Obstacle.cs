using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Comp_Environment_Obstacle : MonoBehaviour
{
    public float speed;
    public float y;
    public Transform environmentTransform;
    private Transform injectTransform;
    private Transform poolTransform;

    void Awake()
    {
        environmentTransform = GameObject.Find("EnvironmentScene").transform;
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
        if (!this.gameObject.activeSelf)
        {
            //this.GetComponentInChildren<ParticleEmitter>().renderer.sortingLayerName = "Particulas";
            this.gameObject.SetActive(true);
            this.transform.position = new Vector3(injectTransform.position.x, y, EnvironmentManager.Instance.ZLayer9);
            this.transform.parent = this.environmentTransform;
        }
    }


}
