using UnityEngine;
using System.Collections;

public class Comp_Kappa_Collision : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider kappa_collider)
    {
        if(kappa_collider.gameObject.name != "Player")
        {
            Debug.Log(kappa_collider.gameObject.name);
        }        
    }
}
