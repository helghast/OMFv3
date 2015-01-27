using UnityEngine;
using System.Collections;

public class comp_ia_pelican : MonoBehaviour
{
    public GameObject[] frogs;  //pool de granotes
    public float speed;         // Velocitat del pelican
    public int countFrogs;      //Contador de granotes agafades
    private CharacterController cc;
    public Vector3 myPos;
    public Transform transform_camera;
    public int direction;
    public float initialSpeed = 12f;
    public float YDistanceToCamera;
    public float SMOOTH_Y;
    public float SMOOTH_SPEED;
    //public float 
	// Use this for initialization
    private float vel_y;

    public Transform Right_Limit;
    public Transform Center_Limit;
    public Transform Pelican_Target_Point;
    public Transform Frog_SpawnPoint;
    public float wander_range;
    public float random_speed_max;
    public float random_speed_min;
    public float Target_Distance_Threshold;

    private float current_random_speed;
    private float target_random_speed;
    private float vel_speed;
    private Vector3 mov_direction;

    void Awake ()
    {
        vel_y = 0;
        vel_speed = 0;
        current_random_speed = Random.Range(random_speed_min, random_speed_max);
        //GameObject kappa = GameObject.Find("Player");
        transform_camera = Camera.main.transform;
    }

	void Start ()
    {
        calcNewTarget();
        transform_camera = Camera.main.transform;
        direction = 1;
        speed = initialSpeed;
        GameLogicManager.Instance.SpawnFrog("", Frog_SpawnPoint.position);
        InvokeRepeating("ThrowFrog",5, 5);
	}

    private void ThrowFrog()
    {
        GameLogicManager.Instance.SpawnFrog("", Frog_SpawnPoint.position);
    }

    private void calcNewTarget()
    {
        float new_x; 
        float new_y;
        int direction;

        float previous_target_random_speed = target_random_speed;
        target_random_speed = Random.Range(random_speed_min, random_speed_max);
        
        if (previous_target_random_speed > 0)
        {
            target_random_speed *= -1;
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        
        //mov_direction = Pelican_Target_Point.position - transform.position;
        if (direction < 0)
        {
            new_x = Center_Limit.position.x + Random.Range(0.0f, 2.0f);
            new_y = Center_Limit.position.y + Random.Range(-0.25f, 0.25f);
            Pelican_Target_Point.position = new Vector3(new_x, new_y, transform.position.z);
        }

        if (direction > 0)
        {
            new_x = (Right_Limit.position.x) - Random.Range(1.0f, 2.0f);
            new_y = Center_Limit.position.y + Random.Range(-0.25f, 0.25f);
            Pelican_Target_Point.position = new Vector3(new_x, new_y, transform.position.z);
        }
            
        mov_direction.Normalize();
    }
	
	// Update is called once per frame
	void Update ()
    {
        current_random_speed = Mathf.SmoothDamp(current_random_speed, target_random_speed, ref vel_speed, SMOOTH_SPEED);

        //transform.Translate(mov_direction * current_random_speed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.right * current_random_speed * Time.deltaTime, Space.World);

        if (Mathf.Abs(Pelican_Target_Point.position.x - transform.position.x) < Target_Distance_Threshold)
        {
            calcNewTarget();
        }
	}
}
