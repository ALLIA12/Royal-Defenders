using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    Dictionary<Vector2Int, NodeClass> grid = new Dictionary<Vector2Int, NodeClass>();
    public NodeClass getGridNode(Vector2Int key)
    {
        if (grid.ContainsKey(key))
        {
            return grid[key];
        }
        else
        {
            return null;
        }
    }
    public Dictionary<Vector2Int, NodeClass> getGrid()
    {
        return grid;
    }

    private void Awake()
    {
        CreateGrid();
    }
    void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new NodeClass(coordinates, true));
            }
        }
    }
    public void blockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }
    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, NodeClass> entery in grid)
        {
            entery.Value.connection = null;
            entery.Value.isExplored = false;
            entery.Value.isPath = false;
        }
    }
    public Vector2Int getCoordiantesFromPos(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / 12);
        coordinates.y = Mathf.RoundToInt(position.z / 12);
        return coordinates;
    }
    public Vector3 getPosFromCoordinates(Vector2 coordinates)
    {
        Vector3 pos = new Vector3();
        pos.x = coordinates.x * 12;
        pos.z = coordinates.y * 12;
        return pos;
    }
}
