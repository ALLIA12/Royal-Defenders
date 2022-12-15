using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class tileSelectCode : MonoBehaviour
{
    public Material glow;
    private Material startcolor;
    private Renderer _renderer;

    private void Start()
    {
        foreach(Transform child in transform)
        {
            if (child.tag != "UI")
            {
                if (child.GetComponent<Renderer>() != null)
                {
                    _renderer = child.GetComponent<Renderer>();
                    startcolor =  _renderer.material;
                    break;
                }
            }
        }
    }

    private void LightUp()
    {
        if (tag == "selectable")
        {
            _renderer.material = glow;
        }
    }
    void OnMouseExit()
    {
        _renderer.material = startcolor;
    }
}
