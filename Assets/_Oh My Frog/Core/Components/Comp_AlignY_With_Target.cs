using UnityEngine;
using System.Collections;

public class Comp_AlignY_With_Target : MonoBehaviour
{
    public Transform target;
    public float height;
    public float offset;

	// Use this for initialization
	void Start ()
    {
        transform.position = new Vector3(transform.position.x, target.position.y + offset + height / 2, transform.position.z);
	}
}
