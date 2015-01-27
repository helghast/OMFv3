using UnityEngine;
using System.Collections;

public class Comp_ScreenDevice_Right : MonoBehaviour
{
    public float Right_Offset;
	void Start ()
    {

        float x;
        Vector3 world_pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height/2, 8));
        transform.position = new Vector3(world_pos.x + Right_Offset, transform.position.y, transform.position.z);
	}

}
