using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class meshSelect : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer _renderer;
    
    void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    

    private void OnMouseEnter()
    {
        _renderer.material.EnableKeyword("_EMISSION");
        _renderer.material.SetColor("_EmissionColor", new Color(1.0f,0.6f,0.0f,1.0f) * 1.0f);
    }

    private void OnMouseExit()
    {
        _renderer.material.DisableKeyword("_EMISSION");
    }
}
