using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class tileSelectCode : MonoBehaviour
{
    public Material glow;
    private Material startcolor;
    private Renderer _renderer;
    
    //get the original material of the tile
    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.tag != "UI")
            {
                if (child.GetComponent<Renderer>() != null)
                {
                    _renderer = child.GetComponent<Renderer>();
                    startcolor = _renderer.material;
                    break;
                }
            }
        }
    }

    //change material to light up material only if it has nothing on it meaning selectable
    private void LightUp()
    {
        if (tag == "selectable")
        {
            _renderer.material = glow;
        }
    }

    private void FixedUpdate()
    {
        turnOff();
    }

    //go back to original material
    private void turnOff()
    {
        _renderer.material = startcolor;
    }
}
