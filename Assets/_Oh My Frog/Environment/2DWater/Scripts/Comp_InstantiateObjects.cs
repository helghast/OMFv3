using UnityEngine;
using System.Collections;

public class Comp_InstantiateObjects : MonoBehaviour
{
    public GameObject the_sphere;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetMouseButtonDown(0))
        {
		    float mousex = Input.mousePosition.x;
		    float mousey = Input.mousePosition.y;
		    Ray ray = Camera.main.ScreenPointToRay (new Vector3(mousex, mousey, 0));
            GameObject go = Instantiate(the_sphere, new Vector3(ray.origin.x, ray.origin.y, 0), Quaternion.identity) as GameObject;
		    //KillObject(go);
	    }
	}
}