using UnityEngine;
using System.Collections;

// Componente para cada uno de los springs (resortes) de la malla del agua
// La idea se basa en la ley de hooke
public class Comp_Spring : MonoBehaviour
{
    public float TargetY; //This is the YSurface in "Water" which is the height of the mesh
    public float Speed;
    public float PropagationSpeed;
    public float PropagationDisplacement;
    public float Displacement;
    public float Damping;
    public float Tension;
    public int ID;
    public int adjacentID;
    public Comp_Water Water; //The "Water" script set this upon instantiating of each spring.
    public Vector3 OriginalPosition;
    public float MaxDecrease;
    public float MaxIncrease;
    public float WaveHeight;
    public float WaveSpeed;
    public Comp_Spring comp_connected_spring;
    private float currentVelocity;

    // 1 Vector3 reutilizable para evitar "new" en update y sobrecargar al GC 
    // --> A C# structure is a value type and the instances or objects of a structure are created in stack (por tanto no perjudica al GC)
    public Vector3 spring_position;

    private Comp_Kappa_Controller comp_kappa_controller;
    
    void Start()
    {
	    OriginalPosition = transform.position;

        spring_position = new Vector3(transform.position.x, transform.position.y + Speed, transform.position.z);
        transform.position = spring_position;
        currentVelocity = 0;
        comp_kappa_controller = GameObject.Find("Player").GetComponent<Comp_Kappa_Controller>();
    }

    void Update() //or FixedUpdate
    {
	    // This is the Spring effect that makes the water bounce and stuff
        Displacement = TargetY - transform.position.y + PropagationDisplacement;
      
        Speed += Tension * Displacement - Speed * Damping;

        spring_position.x = transform.position.x;
        spring_position.y = transform.position.y + Speed;
        spring_position.z = transform.position.z;

	    // Limiting the waves
	    if(transform.position.y < OriginalPosition.y + MaxDecrease)
        {
            spring_position.y = OriginalPosition.y + MaxDecrease;
            Speed = 0;
	    }

	    if(transform.position.y > OriginalPosition.y + MaxIncrease)
        {
            spring_position.y = OriginalPosition.y + MaxIncrease;
            Speed = 0;
	    }
        transform.position = spring_position;

        // Transmite una pequeña porcion de la velocidad de este spring a la spring adyacente (propagación cutre para splash)
        if (comp_connected_spring)
        {
            float propagator = 0.2f;
//            float propagator = 0.40f;

            comp_connected_spring.Speed += propagator * Speed;
     
            Speed -= propagator * Speed * 0.97f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LAND")
            return;

        float impact = other.collider.rigidbody.velocity.y;
        impact = Mathf.Clamp(impact, -20, 20);
        Water.Splash(impact, ID, other.transform);
        Debug.Log("Splash ERROR tag: " + other.name);   
    }
}