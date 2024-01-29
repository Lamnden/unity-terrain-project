using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NoiseGenerator
{
    int resolution;
    Vector2 origin;

    public NoiseGenerator(int resolution, Vector2 origin)
    {
        this.resolution = resolution;
        this.origin = origin;
    }

    public Texture2D GenerateNoise()
    {
        Texture2D heightMap = new Texture2D(resolution, resolution);
        Color[] pix = new Color[resolution * resolution];
        float scale = 8f;
        int max = 10;

        for (int i = 0; i < max; i++)
        {
            for (float y = 0.0f; y < resolution; y++)
            {
                for (float x = 0.0f; x < resolution; x++)
                {
                    float xCoord = origin.x + x / heightMap.width * scale;
                    float yCoord = origin.y + y / heightMap.height * scale;
                    float sample = Mathf.PerlinNoise(xCoord, yCoord);
                    pix[(int)y * heightMap.width + (int)x] = pix[(int)y * heightMap.width + (int)x] + new Color(sample, sample, sample) / (max * i + 1.2f);
                }
            }
            scale += 10f;
        }

        heightMap.SetPixels(pix);
        heightMap.Apply();

        return heightMap;
    }
}
