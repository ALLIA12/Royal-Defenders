using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class mousePosition3D : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    

    // Update is called once per frame
    void Update()
    {
        
        //raycast
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        
        //only detects tile layer due to layerMask
        if (Physics.Raycast(ray, out Hit , float.MaxValue, layerMask))
        {
            //change position
            transform.position = Hit.point;
            
            if (Hit.collider.tag == "selectable")
            {
                //send message to tile object (buldTower) function to build tower
                if (Input.GetMouseButtonDown(0)) Hit.transform.SendMessage("buildTower");
                
            }
            
        }
        
        
    }
}
