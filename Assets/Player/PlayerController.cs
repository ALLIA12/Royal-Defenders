using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Tower[] TowerA;
    [SerializeField] private VictoryMenu victoryMenu;
    [SerializeField] private GameObject hotBarTower;
    [SerializeField] private GameObject hotBarAbility;
    public GameObject asteroid;
    public int TowerType;
    public int abilityType;
    public AudioSource sound;
    public AudioClip buildSound;
    private Tile oldTile;
    public static int penaltyHandler;
    // Update is called once per frame
    private void Start()
    {
        penaltyHandler = 0;
    }

    void Update()
    {
        if (PauseMenu.gameIsPaused) { return; }
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (abilityType > 0)
            {
                hotBarTower.SendMessage("turnOff");
                ability();
            }
            else if (TowerType> 0)
            {
                hotBarAbility.SendMessage("turnOff");
                ConsctructingTowers();                              
            }
            else
            {
                hotBarAbility.SendMessage("turnOn");
            }
            ShowTowerMenu();
            RemoveCurrentMenu();
        }
    }

    private void ability()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        //only detects tile layer due to layerMask
        if (Physics.Raycast(ray, out Hit, float.MaxValue, layerMask))
        {
            //change position
            transform.position = Hit.point;
            
            this.SendMessage("drawLine");
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 temp = new Vector3(transform.position.x,150,transform.position.z);
                Instantiate(asteroid, temp, quaternion.identity);
                Debug.Log("ability" + abilityType);
                abilityType = 0;
                this.SendMessage("removeLine");
                hotBarTower.SendMessage("turnOn");
                hotBarAbility.SendMessage("turnOff");
            }
        }

    }


    private void RemoveCurrentMenu()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (oldTile != null)
            {
                if (oldTile.hasTower)
                {
                    oldTile.currentTower.GetComponent<Tower>().ShowCanvas(false);
                }
                oldTile = null;
            }
        }
    }

    private void ShowTowerMenu()
    {
        //raycast
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        //only detects tile layer due to layerMask
        if (Physics.Raycast(ray, out Hit, float.MaxValue, layerMask))
        {
            transform.position = Hit.point;
            Tile tile = Hit.transform.GetComponent<Tile>();
            // For hard difficulty don't show the new path, just needed to change the order of the if statment
            if (tile.hasTower && Input.GetMouseButtonDown(2))
            {
                if (oldTile != null && oldTile.tag == "Untagged")
                {
                    Tower tower = oldTile.currentTower.GetComponent<Tower>();
                    if (tower != null) { tower.ShowCanvas(false); }
                }
                tile.currentTower.GetComponent<Tower>().ShowCanvas(true);
                oldTile = tile;
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
            Vector3 forward = transform.TransformDirection(Vector3.up) * 20;
            Debug.DrawRay(transform.position,forward,Color.magenta);
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
            tile.tag = "Untagged";
            victoryMenu.numberOfTowersBuilt++;
        }
    }


    //method for choosing the type of tower from the Tower Hotbar
    public void TowerPicker(int towerNO)
    {
        TowerType = towerNO;
    }
    public void abilityPicker(int abilityNO)
    {
        abilityType = abilityNO;
        if (abilityType == -1)
        {
            this.SendMessage("removeLine");
            hotBarTower.SendMessage("turnOn");
        }
    }

    public int gettowerType()
    {
        return TowerType;
    }
    public int getAbilityType()
    {
        return abilityType;
    }
}
