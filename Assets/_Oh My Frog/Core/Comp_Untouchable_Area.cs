using UnityEngine;
using System.Collections;

public class Comp_Untouchable_Area : MonoBehaviour
{
    public Vector2 pos;
    public Vector2 size;
    private Mesh quad_mesh;
    private Vector3[] vertices;
    private Vector2[] uvs;
    private int[] triangles;
    private int triCounter;

    public Vector2 min_xy_anchor;
    public Vector2 max_xy_anchor;

	// Use this for initialization
	void Start ()
    {
        triCounter = 0;
        quad_mesh = new Mesh();
        quad_mesh.name = "DeadZone MESH";
        MeshFilter mf = GetComponent<MeshFilter>();
        mf.mesh = quad_mesh;
        vertices = new Vector3[4];
        uvs = new Vector2[4];
        triangles = new int[(4 - 2) * 3];

        pos = new Vector3(min_xy_anchor.x, Screen.height - pos.y - size.y, 1);
/*        pos = new Vector3(pos.x, Screen.height - pos.y - size.y, 1);
        if (pos.x < 0)
        {
            pos = new Vector3(Screen.width - size.x - 10, pos.y, 1);
        }*/

        Vector3 vertex = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y,1));
        vertices[0] = vertex;
        uvs[0] = new Vector2(0, 0);

        vertex = Camera.main.ScreenToWorldPoint(new Vector3(pos.x + size.x, pos.y, 1));
        vertices[1] = vertex;
        uvs[1] = new Vector2(1, 0);


        vertex = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y + size.y, 1));
        vertices[2] = vertex;
        uvs[2] = new Vector2(0, 1);


        vertex = Camera.main.ScreenToWorldPoint(new Vector3(pos.x + size.x, pos.y + size.y, 1));
        vertices[3] = vertex;
        uvs[3] = new Vector2(1, 1);

        
        quad_mesh.vertices = vertices;
        connectQuad(0, 1, 2, 3);
        quad_mesh.triangles = triangles;
        quad_mesh.uv = uvs;
        MeshCollider comp_mesh_collider = gameObject.AddComponent<MeshCollider>();
        comp_mesh_collider.sharedMesh = quad_mesh;

        Vector3[] normals = new Vector3[4];

        for (int n = 0; n < 4; ++n)
        {
            normals[n] = -Vector3.forward;
        }
        quad_mesh.normals = normals;

        transform.position = new Vector3(0,0,0);
	}
	
    void connectQuad(int p1, int p2, int p3, int p4)
    {
        triangles[triCounter] = p2;
        triCounter++;
        triangles[triCounter] = p1;
        triCounter++;
        triangles[triCounter] = p3;
        triCounter++;

        triangles[triCounter] = p2;
        triCounter++;
        triangles[triCounter] = p3;
        triCounter++;
        triangles[triCounter] = p4;
        triCounter++;
/*
        triangles[triCounter] = p1;
        triCounter++;
        triangles[triCounter] = p2;
        triCounter++;
        triangles[triCounter] = p3;
        triCounter++;

        triangles[triCounter] = p3;
        triCounter++;
        triangles[triCounter] = p2;
        triCounter++;
        triangles[triCounter] = p4;
        triCounter++;*/
    }
}
