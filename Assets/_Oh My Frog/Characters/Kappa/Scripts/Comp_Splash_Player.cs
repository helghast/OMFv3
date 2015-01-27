using UnityEngine;
using System.Collections;

public class Comp_Splash_Player : MonoBehaviour
{
    public Transform player_transform;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Revisar pq no acaba de funcionar bien al 100%
        //transform.position = new Vector3(player_transform.position.x, transform.position.y, transform.position.z);
        transform.position = new Vector3(player_transform.position.x, player_transform.position.y, transform.position.z);

        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0, -10, 0);
        }
	}

    void OnEnterTrigger(Collider other)
    {
        //transform.rigidbody.velocity = new Vector3(0, player_transform.gameObject.GetComponent<Comp_Player_Controller_Physx>().velocity.y, 0);
        Debug.Log("Entered!");
    }
}
