using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class mousePosition3D : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Tower [] TowerA;
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
                    Hit.collider.SendMessage("LightUp");
                    
                    
                    // This shit is temp
                    if (TowerType != 1 && TowerType != 2) {
                        Debug.Log($"{TowerType} is COPE");
                        return;
                    }
                    
                    //Code to build Tower
                    if (Input.GetMouseButtonDown(0) && !Hit.transform.GetComponent<Tile>().isTaken)
                    {
                        bool isPlaced = TowerA[TowerType - 1].CreateTower(TowerA[TowerType - 1], Hit.transform.position);
                        Tile tile = Hit.transform.GetComponent<Tile>();
                        tile.isTaken = isPlaced;
                        if (tile.isTaken)
                        {
                            tile.gridManager.blockNode(tile.coordinates);
                            tile.gridManager.changeCostOoNeighbors(tile.coordinates);
                            tile.pathFinding.notifiyReciviers();
                            
                        }
                    }
                    
                }

            }
            
        }

    }


    //method for choosing the type of tower from the Tower Hotbar
    public void TowerPicker(int towerNO)
    {
        TowerType = towerNO;
    }

    public int gettowerType()
    {
        return TowerType;
    }
}
