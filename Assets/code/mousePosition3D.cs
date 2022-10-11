using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class mousePosition3D : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    public int TowerType;

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.gameIsPaused) { return; }
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
                if (TowerType>0)
                {
                    //send message to tile object (buldTower) function to build tower
                    if (Input.GetMouseButtonDown(0)) Hit.transform.SendMessage("buildTower", TowerType);
                }

            }
            
        }
        
        
    }
    
    //method for choosing the type of tower from the Tower Hotbar
    public void TowerPicker(int towerNO)
    {
        TowerType = towerNO;
    }
}
