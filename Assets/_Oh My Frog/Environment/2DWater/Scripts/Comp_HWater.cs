using UnityEngine;
using System.Collections;

public class Comp_HWater : MonoBehaviour
{
    public float lenght;
    private Vector3[] v_vertices;
    private Vector3[] h_vertices;
    private MeshFilter mf;
    private Mesh mesh;
    private int triNumber;
    private int[] triangle;
    private int surface_vertices;

    public void createHWater(Vector3[] vertices, int vertex_count)
    {
        triNumber = 0;
        triangle = new int[(vertex_count - 2) * 3];

        surface_vertices = vertex_count / 2;
        h_vertices = new Vector3[vertex_count];
        this.v_vertices = vertices;

        // 1) Generamos malla
        mesh = new Mesh();
        mesh.name = "HWaterMesh";
        mf = GetComponent<MeshFilter>();
        mf.mesh = mesh;

        Vector2[] uvs = new Vector2[vertex_count];

        // 2.1) Creo 1ero los vertices de la superficie // --> vertexData = [surface vertices, bottom vertices]
        for (int i = 0; i < surface_vertices; ++i)
        {
            h_vertices[i] = v_vertices[i];

            // UVs
            uvs[i].x = i / (float)(surface_vertices - 1);
            uvs[i].y = 1;
        }

        // 2.2) Creo los vertices de la base de la malla (los añado al vector de vertices ya existente)
        for (int i = 0; i < surface_vertices; ++i)
        {
            h_vertices[i + surface_vertices] = new Vector3(h_vertices[i].x, h_vertices[i].y, lenght);

            // UVs
            uvs[i + surface_vertices].x = i / (float)(surface_vertices - 1);
            uvs[i + surface_vertices].y = 0;
        }
        mesh.vertices = h_vertices;
        mesh.uv = uvs;

        // 3) Una vez tenemos los vertices procedemos a conectarlos entre si para crear los triangulos.
        int bv = surface_vertices; // indice para el primer bottom vertex

        for (int sv = 0; sv < surface_vertices - 1; ++sv)
        {
            TriangulateRectangle(sv         //top-left
                                , sv + 1     //top-right
                                , bv         //bottom-left
                                , bv + 1);   //botom-right*/
            bv++;
        }
        mesh.triangles = triangle;

        // 4) Seteo las normales
        Vector3[] normals = new Vector3[vertex_count];

        for (int n = 0; n < vertex_count; ++n)
        {
            normals[n] = Vector3.up;
        }
        mesh.normals = normals;
    }

    // Construimos el rectángulo a partir de 2 triangulos (vertex clock-wise order)
    void TriangulateRectangle(int p1, int p2, int p3, int p4)
    {
        triangle[triNumber] = p2;
        triNumber++;
        triangle[triNumber] = p1;
        triNumber++;
        triangle[triNumber] = p3;
        triNumber++;

        triangle[triNumber] = p3;
        triNumber++;
        triangle[triNumber] = p4;
        triNumber++;
        triangle[triNumber] = p2;
        triNumber++;
        /*
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
        triNumber++;*/
    }

	// Update is called once per frame
	void LateUpdate()
    {
        // Colocamos cada vertice en la posición de su resorte
        for (int vertex = 0; vertex < surface_vertices; ++vertex)
        {
            h_vertices[vertex].y = v_vertices[vertex].y;
            //h_vertices[vertex + surface_vertices].y = v_vertices[vertex].y;
        }
        mesh.vertices = h_vertices;
	}
}
