using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class AffectShaderLightning : MonoBehaviour
{
    public Light2D light;
    public Renderer _sprite;

    public void Awake()
    {
        _sprite = GetComponent<Renderer>();
    }

    public void Update()
    {
        _sprite.material.SetFloat("_Intensity", light.intensity);
        _sprite.material.SetColor("_Color", light.color);
    }
}
