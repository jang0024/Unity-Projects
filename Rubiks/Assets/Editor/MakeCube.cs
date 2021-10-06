using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MakeCube : Editor
{
    //Menu
    [MenuItem("Mesh/Make Plane")]
    static void GeneratePlane()
    {
        float width = 1.0f;
        float height = 1.0f; 

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0, 0, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, height, 0),
            new Vector3(width, height, 0)
        };
        mesh.vertices = vertices;

        int[] tris = new int[6]
        {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };
        mesh.triangles = tris;

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
 

        // save mesh:
        AssetDatabase.CreateAsset( mesh, "Assets/CustomMesh/testplane.mesh" );
        AssetDatabase.SaveAssets();
    }
    [MenuItem("Mesh/Make Plane XY")]
    static void GeneratePlaneXY()
    {
        float width = 1.0f;
        float height = 1.0f; 

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0, 0, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, height, 0),
            new Vector3(width, height, 0)
        };
        mesh.vertices = vertices;

        int[] tris = new int[6]
        {
            // lower left triangle
            1, 2, 0,
            // upper right triangle
            1, 3, 2
        };
        mesh.triangles = tris;

        Vector3[] normals = new Vector3[4]
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward
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
 

        // save mesh:
        AssetDatabase.CreateAsset( mesh, "Assets/CustomMesh/testplanexy.mesh" );
        AssetDatabase.SaveAssets();
    }

    // makes all 6 sides of the cube:
    [MenuItem("Mesh/Make Cube Sides")]
    static void AllCubePlanes()
    {
        // set offsets on mesh:
        List<Vector3> offsets = new List<Vector3>() {Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.left, Vector3.right};
        for (int i = 0; i< offsets.Count; i++)
        { 
            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[4]
            {
                new Vector3(-0.5f, -0.5f, 0),
                new Vector3(0.5f, -0.5f, 0),
                new Vector3(-0.5f, 0.5f, 0),
                new Vector3(0.5f, 0.5f, 0)
            };
            // based on offsets, rotate and shift:
            Quaternion rot = Quaternion.LookRotation(offsets[i],Vector3.forward);
            for (int k = 0; k<vertices.Length; k++)
            {
                vertices[k] = rot * vertices[k];
                vertices[k] += Vector3.Scale(new Vector3(0.5f,0.5f,0.5f), offsets[i]);
            }

            mesh.vertices = vertices;

            int[] tris = new int[6]
            {
                // lower left triangle
                1, 2, 0,
                // upper right triangle
                1, 3, 2
            };
            mesh.triangles = tris;

            Vector3[] normals = new Vector3[4]
            {
                offsets[i],
                offsets[i],
                offsets[i],
                offsets[i]
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
    

            // save mesh:
            AssetDatabase.CreateAsset( mesh, "Assets/CustomMesh/testplanexy"+i+".mesh" );
            AssetDatabase.SaveAssets();
        }
    }

    [MenuItem("Prefab Generate/Make Cube")]
    static void MakeCubeWithPlanes()
    {
        List<Vector3> offsets = new List<Vector3>() {Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.left, Vector3.right};
        GameObject obj = new GameObject("Single Cube");
        // make all 6 sides of the cube:
        for (int i = 0; i< offsets.Count; i++){
            GameObject intobj = new GameObject("Side "+i);
            Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/CustomMesh/testplanexy"+i+".mesh");
            MeshFilter meshFilter = intobj.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;
            intobj.transform.SetParent(obj.transform);
            MeshRenderer meshRend =  intobj.AddComponent<MeshRenderer>();
            meshRend.material = AssetDatabase.LoadAssetAtPath<Material>("Assets/Material/Side"+i+".mat");
             

        }
    }

    static GameObject GenerateCube()
    {
        List<Vector3> offsets = new List<Vector3>() {Vector3.forward, Vector3.back, Vector3.up, Vector3.down, Vector3.left, Vector3.right};
        GameObject obj = new GameObject("Single Cube");
        // make all 6 sides of the cube:
        for (int i = 0; i< offsets.Count; i++){
            GameObject intobj = new GameObject("Side "+i);
            Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/CustomMesh/testplanexy"+i+".mesh");
            MeshFilter meshFilter = intobj.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;
            intobj.transform.SetParent(obj.transform);
            MeshRenderer meshRend =  intobj.AddComponent<MeshRenderer>();
            meshRend.material = AssetDatabase.LoadAssetAtPath<Material>("Assets/Material/Side"+i+".mat"); 
        }
        return obj;
    }

    [MenuItem("Prefab Generate/Make 3x3x3 Cube")]
    static void MakeRubik(){
        GameObject[] allCubes = new GameObject[27];
        GameObject mainCube = new GameObject("main cube");
        for (int i = 0; i< 27; i++)
        {
            allCubes[i] = GenerateCube();
            allCubes[i].transform.SetParent(mainCube.transform);

        }
        // odd number of sides:
        int totalSides = MathF.Floor(3/2.0f);
        for (int i = 0; i< 27; i++)
        {
             

        }
         
        
    }

}
