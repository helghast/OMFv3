using UnityEngine;
using System.Collections;

public class Comp_Camera : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public float SMOOTH_TIME;
    private float vel_x;
    private float vel_y;
    private Vector3 vel;
    public float RAYCAST_TIME;

    // La posición del jugador en la pantalla (eje horizontal). La cámara se posicionará de tal manera que el jugador siempre queda en esa posición relativa.
    // Si el jugador se mueve la cámara seguirá al jugador manteniendo una distancia offset para que el jugador se siga viendo por pantalla en la posición relativa deseada.
    // Ejemplo: un valor de PLAYER_SCREEN_POSITION = 0.25 posicionará al juagdor en 1/4 del eje horizontal empezando por la derecha. Aunque el jugador se mueva, la cámara lo
    // seguirá manteniendo la distancia necesaria para que este esté posicionado siempre en 1/4 del eje horizontal de la pantalla.
    public float PLAYER_SCREEN_POSITION;
    public float Y_OFFSET;
    public float Y_TO_GROUND;
    private float x_camera_to_player_offset;
    private float y_camera_desired_pos;
    public LayerMask raycast_layer_mask;
    public int int_raycast_layer_mask;
    //private float vel_y;
    public float SMOOTH_TIME_Y;
    private float last_y;

	// Use this for initialization
	void Awake()
    {
        init();
        CameraManager.CreateManager();
        CameraManager.Instance.SetCompCamera(this);
	}

    void Start()
    {
        //cameraGroundTest();
        last_y = player.position.y;
            //gameObject.GetComponent<Comp_Kappa_Collision>();
    }

    private void init()
    {
        vel = Vector3.zero;
        vel_x = 0;
        vel_y = 0;

        //Vector3 desiredPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * PLAYER_SCREEN_POSITION, 0, 0));
        Vector3 a = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 b = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

        //x_camera_to_player_offset = PLAYER_SCREEN_POSITION;
        x_camera_to_player_offset =  a.x + PLAYER_SCREEN_POSITION * (b.x-a.x);

        //raycast_layer_mask = 1 << LayerMask.NameToLayer("Ground");
        //raycast_layer_mask = 1 << 8;
        int_raycast_layer_mask = 1 << 8;
    }

    void Update()
    {
        CameraManager.Instance.RecalculateFrustumPlanes(camera);
    }
	
	// La camara se posiciona después de que se haya posicionado el jugador
	void LateUpdate ()
    {
        //cameraGroundTest();
        Vector3 new_target_position = target.position;
        //new_target_position.x = target.position.x + x_camera_to_player_offset * 10;
        new_target_position.x = target.position.x - x_camera_to_player_offset;
        //new_target_position.y = target.position.y + Y_OFFSET;
        /*
        if (player.gameObject.GetComponent<Comp_Kappa_Controller>().Grounded)
        {
            last_y = player.position.y;
        }*/

        new_target_position.y = y_camera_desired_pos + Y_TO_GROUND;
        //new_target_position.y = last_y + Y_TO_GROUND;
        new_target_position.y = Mathf.SmoothDamp(transform.position.y, new_target_position.y, ref vel_y, SMOOTH_TIME_Y);
        new_target_position.z = transform.position.z;
        Vector3 new_position = Vector3.SmoothDamp(transform.position, new_target_position, ref vel, SMOOTH_TIME);
        transform.position = new_position;
	}

    private IEnumerator getYGround()
    {
        yield return new WaitForSeconds(RAYCAST_TIME);
        //cameraGroundTest();
    }

    private void cameraGroundTest()
    {
        Vector3 pos = player.position;
        pos.y += 10;
        pos.z += 2;
        pos.x += 0.5f;
        y_camera_desired_pos = -2;
        RaycastHit hit;
        //Debug.Log("LAYER: " + raycast_layer_mask);
        if (Physics.Raycast(pos, Vector3.down, out hit, int_raycast_layer_mask))
        {
            y_camera_desired_pos = hit.point.y + Y_TO_GROUND;
            Debug.Log("Collision: " + hit.transform.gameObject.layer + " - Name: " + hit.transform.name);
            Debug.DrawRay(pos, Vector3.down * hit.distance, Color.red, 1);
        }
        //StartCoroutine(getYGround());
    }
}
