using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

/*
 *
 * 
 * 
 */
[RequireComponent(typeof(Comp_Jump_Constants))]
[RequireComponent(typeof(Comp_Dash_Constants))]
public class Comp_Kappa_Controller : MonoBehaviour
{
    //-------------------------------------------------------------------------------- 
    // PUBLIC MEMBERS
    //--------------------------------------------------------------------------------
    public enum ControlMode
    {
        WATER_CONTROL = 0,
        LAND_CONTROL
    }
    public ControlMode control_mode;
    public LayerMask TouchRaycaster_LayerMask;

    public float GRAVITY;
    public float WATER_SPEED;
    public float LAND_SPEED;

    public Transform RESET_TRANSFORM_LIMIT;
    public Transform environmentTransform;
    
    public float Environment_Offset;

    public Vector3 cc_velocity;

    public bool Grounded;
    public bool CanSplash;

    //-------------------------------------------------------------------------------- 
    // PRIVATE MEMBERS
    //--------------------------------------------------------------------------------
    private Comp_Debug comp_debug;
    private CharacterController cc;
    private Vector3 movement;
    private Vector3 player_start_position;

    private bool canDoubleJump;

    private Comp_Dash_Constants COMP_DASH_CONSTANTS;
    private Comp_Jump_Constants COMP_JUMP_CONSTANTS;

    private RigidbodyConstraints water_control_constraints;
    private RigidbodyConstraints land_control_constraints;

    private Transform player_transform;
    private Comp_ScrollBG clouds;

    private GameObject spawner_Enemy_Objects = null;

    private Vector3 currentVel;


    //-------------------------------------------------------------------------------- 
    // TOUCH VARIABLES
    //--------------------------------------------------------------------------------
    private Vector2 touch_begin;
    private Vector2 touch_end;
    private float acc_dt_touch;
    private float acc_dt_dash;

    //-------------------------------------------------------------------------------- 
    // START METHOD
    //--------------------------------------------------------------------------------
    void Start()
    {
        player_transform = transform;

        water_control_constraints = RigidbodyConstraints.FreezePositionZ
                                  | RigidbodyConstraints.FreezeRotation;
        //| RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

        land_control_constraints = RigidbodyConstraints.FreezePositionZ
                                 | RigidbodyConstraints.FreezeRotation;
        try
        {
            comp_debug = GameObject.Find("Debug").GetComponent<Comp_Debug>();
            clouds = GameObject.Find("Sky").GetComponent<Comp_ScrollBG>();
            cc = GetComponent<CharacterController>();

            COMP_DASH_CONSTANTS = GetComponent<Comp_Dash_Constants>();
            COMP_JUMP_CONSTANTS = GetComponent<Comp_Jump_Constants>();

            spawner_Enemy_Objects = GameObject.Find("Empty_Target_Enemys_Obstacles");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        movement = Vector3.zero;
        currentVel = Vector3.zero;
        cc_velocity = Vector3.zero;

        acc_dt_touch = 0;
        acc_dt_dash = 0;

        player_start_position = generateStaticPlayerPosition();
        player_transform.position = new Vector3(player_start_position.x, player_transform.position.y, player_transform.position.z);

        canDoubleJump = false;
        CanSplash = true;

        //ChangeToWaterControlMode();
        ChangeToTerrainControlMode();

    }

    private Vector3 generateStaticPlayerPosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * CameraManager.Instance.X_Pos_Camera_Offset, 0, 0));
    }

    private void UpdateKeyboardMotion()
    {
        //horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void UpdateTouchMotion()
    {
        //horizontal = comp_joystick.horizontalVirtualAxis.GetValueRaw;
    }

    private void UpdateTouchActions()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                acc_dt_touch = 0;
                Vector3 screen_pos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
                Vector3 world_pos = Camera.main.ScreenToWorldPoint(screen_pos);
                //world_pos.z = -10;
                RaycastHit hit;
                if (Physics.Raycast(world_pos, Camera.main.transform.forward, out hit, 20, TouchRaycaster_LayerMask))
                {
                    Debug.Log("collision raycast <tag: " + hit.collider.tag + ">< name: " + hit.collider.name + "> <layer: " + hit.collider.gameObject.layer + ">");
                    if (hit.collider.tag == "Frog")
                    {
                        hit.collider.GetComponent<Comp_IA_Frog>().SelfDestroy();
                        GameLogicManager.Instance.AddFrogs(1);
                    }
                }
                else
                {
                    jump();
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (acc_dt_touch < COMP_JUMP_CONSTANTS.HOLD_TIME_JUMP)
                {
                    addJumpVelocity();
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                if (acc_dt_touch < COMP_JUMP_CONSTANTS.HOLD_TIME_JUMP)
                {
                    addJumpVelocity();
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {

            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 world_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(world_pos, Camera.main.transform.forward, out hit, 20, TouchRaycaster_LayerMask))
                {
                    Debug.Log("collision raycast <tag: " + hit.collider.tag + ">< name: " + hit.collider.name + "> <layer: " + hit.collider.gameObject.layer+">");
                    if (hit.collider.tag == "Frog")
                    {
                        hit.collider.GetComponent<Comp_IA_Frog>().SelfDestroy();
                        GameLogicManager.Instance.AddFrogs(1);
                    }
                }
            }
        }

        // Modificado para que nos devuelva botones compatibles con multiple touch screen en pantallas táctiles
        if (CrossPlatformInput.GetButtonDown("Jump"))
        {
            dash();
        }
    }

    private void UpdateKeyboardActions()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screen_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 world_pos = Camera.main.ScreenToWorldPoint(screen_pos);
            world_pos = new Vector3(world_pos.x, world_pos.y, -10);
            Debug.DrawRay(world_pos, Vector3.forward * 10, Color.green);
            int layer_mask = 1 << 5;
            if (Physics.Raycast(world_pos, Vector3.forward, 10, layer_mask))
            {
                comp_debug.addDebugMessage("Dead Zone!");
            }
            else
            {
                comp_debug.addDebugMessage("jumping");
                jump();
            }
        }


        if (Input.GetKeyDown("space"))
        {
            jump();
            acc_dt_touch = 0;
        }
        else if (Input.GetKey("space") && acc_dt_touch < COMP_JUMP_CONSTANTS.HOLD_TIME_JUMP)
        {
            addJumpVelocity();
        }

        if (Input.GetKeyDown("1"))
        {
            dash();
        }

        if (Input.GetKeyDown("2"))
        {
            ChangeToTerrainControlMode();
        }

        if (Input.GetKeyDown("3"))
        {
            ChangeToWaterControlMode();
        }
    }

    private void updateInputs()
    {
        EInputManager.UpdateInputTouch();
        if (comp_debug)
        {
            if (comp_debug.EntradaTactil)
            {
                // Touch control
                UpdateTouchMotion();
                UpdateTouchActions();
            }
            else
            {
                // keyboard control
                UpdateKeyboardMotion();
                UpdateKeyboardActions();
            }
        }
        else
        {
            // keyboard control
            UpdateKeyboardMotion();
            UpdateKeyboardActions();
        }
    }

    //-------------------------------------------------------------------------------- 
    // UNITY UPDATE METHOD
    //--------------------------------------------------------------------------------
    void Update()
    {
        acc_dt_touch += Time.deltaTime;
        acc_dt_dash += Time.deltaTime;

        Grounded = cc.isGrounded;
        if (Grounded)
            cc_velocity.y = -1;

        CanSplash = !Grounded;

        // modifica la velocity.y si estamos saltando
        updateInputs();

        movement = Vector3.zero;
        movement.x = LAND_SPEED * Time.deltaTime;
        movement.y = cc_velocity.y * Time.deltaTime + 0.5f * GRAVITY * Time.deltaTime * Time.deltaTime;

        cc.Move(movement);
        cc_velocity.y += GRAVITY * Time.deltaTime;

        // RAYCAST ORIENTACION KAPPA
        /*
        Vector3 raycast_origin = new Vector3(player_transform.position.x, player_transform.position.y + 3, player_transform.position.z + 3);
        RaycastHit hit;
        if (Physics.Raycast(raycast_origin, -Vector3.up, out hit, 100.0F))
        {
            Vector3 previous_right = transform.right;
            transform.forward = Vector3.Cross(hit.normal, previous_right);
            transform.up = hit.normal;
            Vector3 new_right = Vector3.Cross(hit.normal, transform.forward);

            transform.right = Vector3.SmoothDamp(previous_right, new_right, ref currentVel, 0.2f);

            //Vector3 saved_right = cc.transform.forward;
            //Vector3 saved_forward = cc.transform.right;
            //Vector3 new_forward = Vector3.Cross(hit.normal, saved_right);
            //Vector3 new_right = Vector3.Cross(hit.normal, saved_forward);
            //Vector3 new_right = Vector3.Cross(saved_forward, hit.normal);
            //cc.transform.up = hit.normal;
            //cc.transform.right = Vector3.Cross(cc.transform.up, saved_right);
            //cc.transform.LookAt(new_right    );
            //cc.transform.rotation = Quaternion.LookRotation(-hit.normal);

        }*/

        if (cc.transform.position.x > RESET_TRANSFORM_LIMIT.position.x)
        {
            cc.transform.position = new Vector3(0, player_transform.position.y, player_transform.position.z);
            MatchAllToPlayerPosition();

            //temp GameObject. Posible chapuza!
            Comp_Obstacle_Enemys temp_OEManager_Script = GameObject.Find("Obstacle_Enemy_Manager").GetComponent<Comp_Obstacle_Enemys>();
            //volver a poner enemy_obstacle de la escena en el pool
            temp_OEManager_Script.return_To_Pool_Position();
            //Comp_Obstacle_Enemys.return_To_Pool_Position();
            //cambiar enemy_obstacle
            //Comp_Obstacle_Enemys.position_Random_Enemy_Obstacle();
            temp_OEManager_Script.position_Random_Enemy_Obstacle();
        }
    }

    //-------------------------------------------------------------------------------- 
    // UNITY LATE UPDATE METHOD
    //--------------------------------------------------------------------------------
    void LateUpdate()
    {
        MatchAllToPlayerPosition();
    }

    private void MatchAllToPlayerPosition()
    {
        environmentTransform.position = new Vector3(Camera.main.transform.position.x + Environment_Offset, environmentTransform.position.y, environmentTransform.position.z);
    }
   
    // Unused
    void customPhysxCorrection()
    {
        Debug.Log("fdsflsdfkldsjf");
        float z_rotation = player_transform.eulerAngles.z;
        if (z_rotation > 110)
        {
            z_rotation = 110;
        }
        else if (z_rotation < 80)
        {
            z_rotation = 80;
        }

        player_transform.rotation = Quaternion.Euler(player_transform.eulerAngles.x, player_transform.eulerAngles.y, z_rotation);
    }

    

    //---------------------------------------------------------------------------------------------------
    // MECANICAS
    //---------------------------------------------------------------------------------------------------
    private void jump()
    {
        if (comp_debug.EntradaTactil)
        {
            if (EInputManager.IsTouchOverUIElement)
            {
                comp_debug.addDebugMessage("Pointer is over a game object!");
                return;
            }
            else
            {
                comp_debug.addDebugMessage("Puedo saltar!!");
            }
        }
        if (Grounded)
        {
            cc_velocity.y = COMP_JUMP_CONSTANTS.JUMP_SPEED;
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            cc_velocity.y = COMP_JUMP_CONSTANTS.DOUBLE_JUMP_SPEED;
            canDoubleJump = false;
        }
    }

    private void addJumpVelocity()
    {
        if (canDoubleJump && !Grounded)
        {
            cc_velocity.y += COMP_JUMP_CONSTANTS.DELTA_JUMP_SPEED * Time.deltaTime;
        }
    }

    public void ExecuteDash()
    {
        dash();
        Debug.Log("Dash executed!");
    }

    private void dash()
    {
        clouds.setScrollSpeed(EnvironmentManager.Instance.Clouds_Speed_Dash);
        StartCoroutine(dashCoroutine());
    }

    private void stopDash()
    {
        clouds.setScrollSpeed(EnvironmentManager.Instance.Clouds_Speed);
    }

    private IEnumerator dashCoroutine()
    {
        yield return new WaitForSeconds(COMP_DASH_CONSTANTS.DASH_TIME);
        stopDash();
    }

    public void ChangeToTerrainControlMode()
    {
        control_mode = ControlMode.LAND_CONTROL;
        //player_transform.rigidbody.constraints = land_control_constraints;
    }

    public void ChangeToWaterControlMode()
    {
        control_mode = ControlMode.WATER_CONTROL;
        //player_transform.rigidbody.constraints = water_control_constraints;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LAND")
        {
            ChangeToTerrainControlMode();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "LAND")
        {
            ChangeToWaterControlMode();
        }
    }

}
