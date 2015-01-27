using UnityEngine;
using System.Collections;

public class Comp_SinkObject : MonoBehaviour
{
    public float WaterLevel;
    public float SinkSpeed;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.y < WaterLevel)
        {
            if (transform.rigidbody.velocity.y < SinkSpeed)
            {
                transform.rigidbody.velocity = new Vector3(transform.rigidbody.velocity.x, SinkSpeed, transform.rigidbody.velocity.z);
            }
        }
	
	}
}
