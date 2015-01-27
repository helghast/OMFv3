using UnityEngine;
using System.Collections;

public class Comp_Relative_mov : MonoBehaviour
{
    public int direction;
	// Use this for initialization
	void Start ()
    {
        transform.position = new Vector3(0, 0, 0);
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //transform.Translate(direction * Vector3.right * Time.deltaTime, Space.Self);
        //transform.Translate(direction * Vector3.right * Time.deltaTime);
	}
}
