using UnityEngine;
using System.Collections;

public class Comp_Environment_Element : MonoBehaviour
{
    public float Speed;
    public float length;
    public float minDist2NextElement;
    public float maxDist2NextElement;
    public float threshold;
    public Transform environmentTransform;
    public Transform poolTransform;

    void Awake()
    {
        environmentTransform = GameObject.Find("Elements2DInScene").transform;
        poolTransform = GameObject.Find("Pool_Environment").transform;
    }

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 mov = Vector3.right * Speed * Time.deltaTime;
        transform.Translate(mov, Space.World);
	}

    public void wakeUp()
    {

    }

}
