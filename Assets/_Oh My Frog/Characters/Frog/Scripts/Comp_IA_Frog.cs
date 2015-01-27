using UnityEngine;
using System.Collections;

public class Comp_IA_Frog : MonoBehaviour
{
    public Comp_Frog_Manager comp_Frog_Manager;
    public cFrog frog;
    private Animator animator;
    private Vector3 velocity;
    private float gravity;

    public float Fall_Speed;
    public float Sunk_Speed;
    public Comp_Kappa_Controller comp_Kappa_Controller;
    public float speed;

    void Awake()
    {
        comp_Frog_Manager = GameObject.Find("GameLogic_Manager").GetComponent<Comp_Frog_Manager>();
        frog = new cFrog(this);
        animator = GetComponent<Animator>();

        comp_Kappa_Controller = GameObject.Find("Player").GetComponent<Comp_Kappa_Controller>();
    }
    
    void Start()
    {
        velocity = Vector3.zero;
        gravity = -4;
        speed = comp_Kappa_Controller.LAND_SPEED;
        speed -= 1;
    }
      

    // Update is called once per frame
    void Update()
    {
        //Vector3 mov = Vector3.zero;
        Vector3 mov = (-2 * Vector3.up + Vector3.right * speed) * Time.deltaTime;
        //mov.y = velocity.y * Time.deltaTime + 0.5f * gravity * Time.deltaTime * Time.deltaTime;
        //velocity.y += gravity * Time.deltaTime;
        //transform.Rotate(Vector3.forward, 90 * Time.deltaTime);
        transform.Translate(mov, Space.World);
    }

    public void WakeUp()
    {
        velocity.y = 0;
        transform.parent = null;
    }

    public void SelfDestroy()
    {
        comp_Frog_Manager.DeleteFrog(frog.index);
    }

}
