using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class mouseDrawCircle : MonoBehaviour
{
    private int value = 0;
    [Range(0,50)]
    public int segments = 50;
    [Range(0,20)]
    public float xradius = 5;
    [Range(0,20)]
    public float zradius = 5;
    LineRenderer line;

    //create the line object
    void Start ()
    {
        line = gameObject.GetComponent<LineRenderer>();
        
    }

    //check if an ability is selected to start drawing the line
    private void Update()
    {
        if (value>0)
        {
            line.positionCount = (segments + 1);
            line.useWorldSpace = false;
            CreatePoints ();
        }
    }


    //draw the line method
    void CreatePoints ()
    {
        float x;
        float y = 2;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos (Mathf.Deg2Rad * angle) * zradius;

            line.SetPosition (i,new Vector3(x,y,z) );

            angle += (360f / segments);
        }
    }

    public void removeLine()
    {
        value = 0;
        for (int i = 0; i < (segments + 1); i++)
        {
            line.SetPosition(i, Vector3.zero);
        }
        
    }

    public void drawLine()
    {
        value = 1;
    }
    
}
