using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Comp_Base_Enemy : MonoBehaviour
{
    public string enemyName;
    public float speed;
    public float y;

    public Transform environmentTransform;
    public Transform injectTransform;
    public Transform poolTransform;

    void Awake() { }

    public void InitAwake()
    {
        environmentTransform = GameObject.Find("EnemysInScene").transform;
        injectTransform = GameObject.Find("EnvironmentSpawnPoint").GetComponent<Transform>();
        poolTransform = GameObject.Find("Pool_Enemys").transform;
        speed = -3;
    }

    void Start() { }
    void Update() { }

    public void advance()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
    }

    public void spawn()
    {
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
            this.transform.position = new Vector3(injectTransform.position.x, y, EnvironmentManager.Instance.ZLayer9);
            this.transform.parent = this.environmentTransform;
            EnvironmentManager.Instance.addActiveEnemy(enemyName);

        }
    }

    public void disable()
    {
        transform.parent = poolTransform;        
        gameObject.SetActive(false);
    }

    public void setName(string name)
    {
        enemyName = name;
    }

}