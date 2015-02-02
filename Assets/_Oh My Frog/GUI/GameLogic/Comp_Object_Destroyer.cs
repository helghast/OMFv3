using UnityEngine;
using System.Collections;

public class Comp_Object_Destroyer : MonoBehaviour
{
    public string[] registered_tags;
    public Transform follow;
    public float Distance;
    private Transform _transform;


	// Use this for initialization
	void Start ()
    {
        _transform = transform;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        _transform.position = follow.position + new Vector3(-Distance, 0, 0);

	}
}
