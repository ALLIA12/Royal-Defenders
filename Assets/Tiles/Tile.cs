using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower tower;
    [SerializeField] bool isTaken;
    [SerializeField]
    [Range(0f, 4f)] public float tileSpeed = 0.5f;
    GridManager gridManager;
    PathFinding pathFinding;
    Vector2Int coordinates = new Vector2Int();
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinding = FindObjectOfType<PathFinding>();
    }
    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.getCoordiantesFromPos(this.transform.position);
            if (isTaken)
            {
                gridManager.blockNode(coordinates);
            }
        }
    }

    public bool GetIsTaken()
    {
        return isTaken;
    }


    void buildTower(int towertype)
    {
        if (gridManager.getGridNode(coordinates).isWalkable && !pathFinding.willBlockPath(coordinates))
        {
            bool isPlaced = tower.CreateTower(tower, this.transform.position);
            isTaken = isPlaced;
            if (isTaken)
            {
                gridManager.blockNode(coordinates);
                gridManager.changeCostOoNeighbors(coordinates);
                pathFinding.notifiyReciviers();
            }
        }
        
    }
    
}
