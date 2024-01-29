using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square
{
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    float scale;
    Texture2D heightMap;

    Vector3 axisA;
    Vector3 axisB;
    

    public Square(Mesh mesh, int resolution, Vector3 localUp, float scale, Texture2D heightMap)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;
        this.scale = scale;
        this.heightMap = heightMap;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int i = 0;
        int triIndex = 0;

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnFace = (localUp + ((percent.x - .5f) * 2 * axisA) + ((percent.y - .5f) * 2 * axisB)) * scale;
                pointOnFace = new Vector3(pointOnFace.x, heightMap.GetPixel(x, y).r * 15, pointOnFace.z);
                vertices[i] = pointOnFace;

                if (x != resolution - 1 && y != resolution - 1)
                {
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + resolution + 1;
                    triangles[triIndex + 2] = i + resolution;

                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;

                    triIndex += 6;
                }

                i++;
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
