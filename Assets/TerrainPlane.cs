using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPlane : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 8;
    public float scale = 1f;
    public Vector2 origin = new Vector2(0, 0);

    [SerializeField, HideInInspector]
    MeshFilter meshFilter;
    Square squareFace;
    NoiseGenerator noise;

    private void OnValidate()
    {
        Initalize();
        GenerateMesh();
    }

    void Initalize()
    {
        if (meshFilter == null)
        {
            meshFilter = new MeshFilter();

            GameObject meshObj = new GameObject("mesh");
            meshObj.transform.parent = transform;

            meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
            meshFilter = meshObj.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh();
        }

        noise = new NoiseGenerator(resolution, origin);
        squareFace = new Square(meshFilter.sharedMesh, resolution, Vector3.up, scale, noise.GenerateNoise());
    }

    void GenerateMesh()
    {
        squareFace.ConstructMesh();
    }
}
