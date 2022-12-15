using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public bool isTaken;
    [SerializeField] public bool hasTower = false;
    public GameObject currentTower;
    [SerializeField]
    [Range(0f, 4f)] public float tileSpeed = 0.5f;
    public GridManager gridManager;
    public PathFinding pathFinding;
    public Vector2Int coordinates = new Vector2Int();
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

}
