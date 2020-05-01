using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class OrbitRenderer : MonoBehaviour
{
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    Mesh mesh;
    public Vector3[] startEndPositions = new Vector3[2];
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();
        CreateMash();
    }

    void CreateMash() {
        Vector3 first = startEndPositions[0];
        Vector3 second = startEndPositions[1];
        int spacing = 10;
        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(first.x-spacing, first.y-spacing, first.z), // 0: bottom left
            new Vector3(first.x+spacing, first.y-spacing, first.z), // 1: bottom right
            new Vector3(first.x+spacing, first.y+spacing, first.z), // 2: top right
            new Vector3(first.x-spacing, first.y+spacing, first.z), // 3: top left
        };
        mesh.vertices = vertices;

        int[] indices = new int[6]
        {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };
        mesh.triangles = indices;

        Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        mesh.normals = normals;

        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };
        mesh.uv = uv;

        meshFilter.mesh = mesh;
        mesh.RecalculateNormals();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnValidate()
    {
        CreateMash();
    }
}
