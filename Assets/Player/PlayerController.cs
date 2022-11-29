using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Tower[] TowerA;
    public int TowerType;
    public AudioSource sound;
    public AudioClip buildSound;

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.gameIsPaused) { return; }
        ConsctructingTowers();
        ShowTowerMenu();
    }
    private void ShowTowerMenu()
    {
        //raycast
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        //only detects tile layer due to layerMask
        if (Physics.Raycast(ray, out Hit, float.MaxValue, layerMask))
        {
            //change position
            transform.position = Hit.point;

            if (Hit.collider.tag == "selectable")
            {

                Tile tile = Hit.transform.GetComponent<Tile>();
                // Code to build Tower
                // For hard difficulty don't show the new path, just needed to change the order of the if statment
                if (tile.hasTower && Input.GetMouseButtonDown(2))
                {
                    Debug.Log("COPE");
                    tile.currentTower.GetComponent<Tower>().ShowCanvas();
                    //BuildTower(tile, Hit);
                }

            }

        }
    }
    private void ConsctructingTowers()
    {
        //raycast
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        //only detects tile layer due to layerMask
        if (Physics.Raycast(ray, out Hit, float.MaxValue, layerMask))
        {
            //change position
            transform.position = Hit.point;

            if (Hit.collider.tag == "selectable")
            {

                if (TowerType > 0)
                {
                    Hit.collider.SendMessage("LightUp");

                    // This shit is temp
                    if (TowerType != 1 && TowerType != 2 && TowerType != 3)
                    {
                        Debug.Log($"{TowerType} is COPE");
                        return;
                    }
                    Tile tile = Hit.transform.GetComponent<Tile>();
                    //Code to build Tower
                    // For hard difficulty don't show the new path, just needed to change the order of the if statment
                    if (SettingsMenu.difficulty == 2)
                    {
                        if (tile.gridManager.getGridNode(tile.coordinates).isWalkable && !tile.isTaken && Input.GetMouseButtonDown(0) && !tile.pathFinding.willBlockPath(tile.coordinates))
                        {
                            BuildTower(tile, Hit);
                        }
                    }
                    else
                    {
                        if (tile.gridManager.getGridNode(tile.coordinates).isWalkable && !tile.isTaken && !tile.pathFinding.willBlockPath(tile.coordinates) && Input.GetMouseButtonDown(0))
                        {
                            BuildTower(tile, Hit);
                        }
                    }
                }

            }

        }

    }

    private void BuildTower(Tile tile, RaycastHit hit)
    {
        bool isPlaced;
        GameObject towerObject;
        TowerA[TowerType - 1].CreateTower(out isPlaced, out towerObject, TowerA[TowerType - 1], hit.transform.position, tile);
        tile.isTaken = isPlaced;
        sound.PlayOneShot(buildSound);
        if (tile.isTaken)
        {
            tile.hasTower = true;
            tile.currentTower = towerObject;
            tile.gridManager.blockNode(tile.coordinates);
            tile.gridManager.changeCostOoNeighbors(tile.coordinates, 3);
            tile.pathFinding.notifiyReciviers();
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
