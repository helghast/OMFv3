using UnityEngine;
using System.Collections;

public class Comp_Water : MonoBehaviour
{
    public enum WAVE_DIRECTION
    {
        LEFT_TO_RIGHT = 0,
        RIGHT_TO_LEFT
    }
    public WAVE_DIRECTION wave_direction;

    //Mesh
    public float VertexSpacing= 1.0f;
    public float StartX;
    public float YSurface;
    public float YBottom;
    public int VertexCount;

    //Water Properties
    public float Tension= 0.025f;
    public float Spread= 0.25f; //Dont increase this above 0.30f, Although you should try it.
    public float Damping= 0.025f;
    public float CollisionVelocity; //Multiplier of the speed of the collisions done to the water
    public float MaxIncrease; //MAX Increase of water In Y
    public float MaxDecrease; //MAX Decrease of water In -Y (Use negative number)
    public Transform SpringPrefab; //Springs
    public Transform WaveSimulatorPrefab; //This is just another spring but it is used to make the waves
    public float WaveSimHeight;
    public float WaveSimSpeed;
    public float WaveHeight;
    public float WaveTimeStep;

    //Hide in inspector
    int SurfaceVertices;
    //This is used alot because all the work is done to the surface of the water not the bottom.
    float Height;
    float SmoothTime;
    bool ChangedHeight;
    float yVelocity = 0.0f;

    public GameObject go_HWater;
    private Comp_HWater comp_HWater;

    private Vector3[] vertices;
    private MeshFilter mf;
    private Mesh mesh;
    private int triNumber;
    private int[] triangle;
    private float[] lDeltas;
    private float[] rDeltas;
    private Transform DelayingObject;
    private Comp_Spring[] SpringList;

    [HideInInspector]
    public Vector3 DelayingObjectOldPos = new Vector3(0,0,0);
    Comp_WaveSimulator WaveSimulator;
    int te;
    bool Waving = false;
	
    void Awake()
    {
        // Vertices de la superficie
	    SurfaceVertices = VertexCount / 2;
        // Vertices totales de la malla
	    vertices = new Vector3[VertexCount];
	    
        // Triangulos que formarán la malla
        triNumber = 0;
	    triangle = new int[(VertexCount-2) * 3];

        SpringList = new Comp_Spring[SurfaceVertices];
	    lDeltas = new float[SpringList.Length];
	    rDeltas = new float[SpringList.Length];

        comp_HWater = go_HWater.GetComponent<Comp_HWater>();
    }

    void Start ()
    {
        // Generamos malla
        CreateWaterMesh();
	
        // Generamos cada uno de los springs
	    for(int i = 0; i < SurfaceVertices; ++i)
	    {
		    Transform TransformHolder;
		    TransformHolder = Instantiate (SpringPrefab, vertices[i], Quaternion.identity) as Transform;
            SpringList[i] = TransformHolder.GetComponent<Comp_Spring>();
		    SpringList[i].MaxIncrease = MaxIncrease;
		    SpringList[i].MaxDecrease = MaxDecrease;
		    SpringList[i].TargetY = YSurface;
		    SpringList[i].Damping = Damping;
		    SpringList[i].Tension = Tension;
		    SpringList[i].ID = i;
		    SpringList[i].Water = this;

            if (i > 0)
            {
                SpringList[i].comp_connected_spring = SpringList[i-1];
            }

		    var boxCollider = TransformHolder.GetComponent<BoxCollider>() as BoxCollider;
		    //boxCollider.size = new Vector3(VertexSpacing,0,2);
		    SpringList[i].transform.parent = this.transform;
	    }
	    //Wave Simulator
	    WaveSimulatorPrefab = Instantiate(WaveSimulatorPrefab, new Vector3(0,0,0),Quaternion.identity) as Transform;
        WaveSimulator = WaveSimulatorPrefab.GetComponent<Comp_WaveSimulator>();
	    WaveSimulator.WaveHeight = WaveSimHeight;
	    WaveSimulator.WaveSpeed = WaveSimSpeed;

	    //StartCoroutine(ChangeWaterHeight(5,2));
    }

    void Update ()
    {
	    // Without this each spring is independent.
        for (int e= 0; e < SpringList.Length; e++)
	    {
		    if (e > 0)
		    {
			    lDeltas[e] = Spread * (SpringList[e].transform.position.y - SpringList[e - 1].transform.position.y);
			    SpringList[e-1].Speed += lDeltas[e];
		    }
		    if (e < SpringList.Length - 1)
		    {
			    rDeltas[e] = Spread * (SpringList[e].transform.position.y - SpringList[e + 1].transform.position.y);
			    SpringList[e + 1].Speed += rDeltas[e];
		    }
	    }

	    for (int i= 0; i < SpringList.Length; i++)
	    {
            if (i > 0)
            {
                SpringList[i - 1].transform.position += new Vector3(0, lDeltas[i], 0);
            }
            
            if (i < SpringList.Length - 1)
            {
                SpringList[i + 1].transform.position += new Vector3(0, rDeltas[i], 0);
            }
	    }
	
	    if(ChangedHeight)
        {
		    float newPosition = Mathf.SmoothDamp(SpringList[0].TargetY, Height, ref yVelocity, SmoothTime);
		    SetWaterHeight(newPosition);
	    } 
	
	    //for(int t=0; t< SpringList.Length; t++)
	    //{
	    // Optional code for springs goes here
	    //}       
	
	    // Colocamos cada vertice en la posición de su resorte
        for (int vertex = 0; vertex < SurfaceVertices; ++vertex)
        {
            vertices[vertex].y = SpringList[vertex].transform.position.y;
	    }
	    mesh.vertices = vertices;
    }

    void FixedUpdate ()
    {
	    if(!Waving)
        {
		    StartCoroutine(MakeWave());
	    }		
    }

    void CreateWaterMesh()
    {
        // 1) Generamos malla
        mesh = new Mesh();
        mesh.name = "WaterMesh";
        mf = GetComponent<MeshFilter>();
        mf.mesh = mesh;

        Vector2[] uvs = new Vector2[VertexCount];

        // 2.1) Creo 1ero los vertices de la superficie // --> vertexData = [surface vertices, bottom vertices]
        for (int i = 0; i < SurfaceVertices; ++i)
        {
            vertices[i] = new Vector3(StartX + (VertexSpacing * i), YSurface, 0);

            // UVs
            uvs[i].x = i / (float)(SurfaceVertices - 1);
            uvs[i].y = 1;
        }

        // 2.2) Creo los vertices de la base de la malla (los añado al vector de vertices ya existente)
        for (int i = 0; i < SurfaceVertices; ++i)
        {
            vertices[i + SurfaceVertices] = new Vector3(vertices[i].x, YBottom, 0);

            // UVs
            uvs[i + SurfaceVertices].x = i / (float)(SurfaceVertices - 1);
            uvs[i + SurfaceVertices].y = 0;
        }
        mesh.vertices = vertices;
        mesh.uv = uvs;

        // 3) Una vez tenemos los vertices procedemos a conectarlos entre si para crear los triangulos.
        int bv = SurfaceVertices; // indice para el primer bottom vertex
        
        for (int sv = 0; sv < SurfaceVertices - 1; ++sv)
        {
            TriangulateRectangle(sv         //top-left
                                ,sv + 1     //top-right
                                ,bv         //bottom-left
                                ,bv + 1);   //botom-right
            bv++;
        }
        mesh.triangles = triangle;
        
        // 4) Seteo las normales
        Vector3[] normals = new Vector3[VertexCount];

        for (int n = 0; n < VertexCount; ++n)
        {
            normals[n] = -Vector3.forward;
        }
        mesh.normals = normals;

        // 5) Creo la superficie horizontal del agua (un GO aparte)
        comp_HWater.createHWater(vertices, VertexCount);
    }

    IEnumerator MakeWave()
    {
	    Waving = true;
	    SpringList[te].Speed += Vector2.Distance(new Vector2(0, WaveSimulatorPrefab.position.y), new Vector2(0, SpringList[te].transform.position.y)) * WaveHeight;
	    
        //And here is another wave behind the first wave
	    //if( te > 20) {
	    //	SpringList[te-20].Speed += Vector2.Distance(Vector2(0,WaveSimulatorPrefab.position.y), Vector2(0,SpringList[te].transform.position.y)) * WaveHeight;
	    //}
	
	    //SpringList[te].Speed += 0.20f; //This could be used but it wont look realistic, if you use this you dont need the WaveSimulator

        if (wave_direction == WAVE_DIRECTION.LEFT_TO_RIGHT)
        {
            te++;
            if (te > SpringList.Length - 1)
            {
                te = 0;
            }
        }
        else
        {
            te--;
            if (te < 0)
            {
                te = SpringList.Length - 1;
            }
        }

	    yield return new WaitForSeconds(WaveTimeStep);
	    Waving = false;
    }

    public void Splash (float Velocity, int ID, Transform Victim)
    {
		SpringList[ID].Speed += Velocity * CollisionVelocity * 0.5f;
    }

    public void SetWaterHeight(float mHeight)
    {
	    for(int t=0; t< SpringList.Length; t++)
	    {
		    SpringList[t].TargetY = mHeight;
	    }
    }

    public IEnumerator ChangeWaterHeight(float mHeight, float mSmoothTime)
    {
	    Height = mHeight;
	    SmoothTime = mSmoothTime;
	    ChangedHeight = true;
	    yield return new WaitForSeconds(mSmoothTime);
    }

    // Construimos el rectángulo a partir de 2 triangulos (vertex clock-wise order)
    void TriangulateRectangle (int p1, int p2, int p3, int p4)
    {
        triangle[triNumber] = p1;
        triNumber++;
        triangle[triNumber] = p2;
        triNumber++;
        triangle[triNumber] = p3;
        triNumber++;

        triangle[triNumber] = p3;
        triNumber++;
        triangle[triNumber] = p2;
        triNumber++;
        triangle[triNumber] = p4;
        triNumber++;
    }
}