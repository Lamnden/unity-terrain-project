using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseRenderer : MonoBehaviour
{
    public int resolution;
    public Vector2 origin;

    NoiseGenerator noise;

    private Renderer rend;

    private void OnValidate()
    {
        rend = GetComponent<Renderer>();
        noise = new NoiseGenerator(resolution, origin);
        rend.material.mainTexture = noise.GenerateNoise();
    }
}
