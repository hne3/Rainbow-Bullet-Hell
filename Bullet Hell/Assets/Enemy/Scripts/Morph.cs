using UnityEngine;
using System.Collections;

public class Morph : MonoBehaviour {

    public int numVerts = 99;

    public Vector3[] vert;
    public Vector2[] uv;
    public int[] triangles;

	private void Start ()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        for(int i = 0; i < numVerts; i++)
        {
            float x = 0.0f;
            float y = 0.0f;
        }
        mesh.vertices = vert;
        mesh.uv = uv;
        mesh.triangles = triangles;
	}
	
	private void Update ()
    {
	
	}
}
