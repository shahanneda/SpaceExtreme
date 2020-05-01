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
        int spacing = 1;

        Vector3[] vertices = new Vector3[8]
        {
            new Vector3(first.x, first.y, first.z), // bottom left
            new Vector3(first.x+spacing, first.y, first.z), // bottom right
            new Vector3(first.x+spacing, first.y+spacing, first.z), // top right
            new Vector3(first.x, first.y+spacing, first.z), // top left
            
            new Vector3(second.x, second.y, second.z), // bottom left
            new Vector3(second.x+spacing, second.y, second.z), // bottom right
            new Vector3(second.x+spacing, second.y+spacing, second.z), // top right
            new Vector3(second.x, second.y+spacing, second.z), // top left


        };
        mesh.vertices = vertices;

        int[] tris = new int[36]
        {
            0, 2, 1, // tringle from bottom left -> bottom right -> top rigth
            3, 2, 0, // triangle from top left -> top right -> bottom left


            4, 6, 5, // tringle from bottom left -> bottom right -> top rigth
            7, 6, 4, // triangle from top left -> top right -> bottom left
            
            1,6,5, //  right
            1,2,6,

            0,7,4, // left
            0,7,4,

            0,4,5, // bottom
            0,1,5, 

            6, 7, 3, 
            3, 2, 6,
        };
        mesh.triangles = tris;

        Vector3[] normals = new Vector3[8]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,

            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        mesh.RecalculateNormals();

        Vector2[] uv = new Vector2[8]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),


            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1)
        };
        mesh.uv = uv;

        meshFilter.mesh = mesh;

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
