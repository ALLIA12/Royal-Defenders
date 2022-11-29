using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public bool isTaken;
    [SerializeField]
    [Range(0f, 4f)] public float tileSpeed = 0.5f;
    public GridManager gridManager;
    public PathFinding pathFinding;
    public Vector2Int coordinates = new Vector2Int();
    private Renderer _renderer;
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinding = FindObjectOfType<PathFinding>();
        _renderer = GetComponentInChildren<Renderer>();
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
    
    

    private void Update()
    {
        turnOff();
    }

    private void LightUp()
    {
        if (tag == "selectable" && !isTaken)
        {
            _renderer.material.EnableKeyword("_EMISSION");
            _renderer.material.SetColor("_EmissionColor", new Color(1.0f, 0.6f, 0.0f, 1.0f) * 1.0f);
        }
        else
        {
            _renderer.material.DisableKeyword("_EMISSION");
        }
    }

    private void turnOff()
    {
        if (tag == "selectable")
        {
            _renderer.material.DisableKeyword("_EMISSION");
        }
    }


    public bool GetIsTaken()
    {
        return isTaken;
    }

}
