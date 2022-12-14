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
                Tile currentTile = GameObject.Find(coordinates.ToString()).GetComponent<Tile>();
                grid.Add(coordinates, new NodeClass(coordinates, !currentTile.GetIsTaken(), currentTile.tileSpeed));
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
    public void unblockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = true;
        }
    }

    public void changeCostOoNeighbors(Vector2Int coordinates, float  changeValue)
    {
        Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down, new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1) };
        foreach (Vector2Int direction in directions)
        {
            if (grid.ContainsKey(coordinates + direction) && grid[coordinates + direction].isWalkable)
            {
                grid[coordinates + direction].hasTowersAdjecent += changeValue;
                //Debug.Log($"{grid[coordinates + direction]}has become {grid[coordinates + direction].hasTowersAdjecent}");
            }
        }
        Vector2Int[] directionsx2 = { Vector2Int.up*2, Vector2Int.down * 2, Vector2Int.left * 2, Vector2Int.right * 2,
                                      new Vector2Int(1, 2), new Vector2Int(2, 1), new Vector2Int(2, 2),
                                      new Vector2Int(1, -2), new Vector2Int(2, -1), new Vector2Int(2, -2),
                                      new Vector2Int(-1, -2), new Vector2Int(-2, -1), new Vector2Int(-2, -2),
                                      new Vector2Int(-1, 2), new Vector2Int(-2, 1), new Vector2Int(-2, 2)
        };
        foreach (Vector2Int direction in directionsx2)
        {
            if (grid.ContainsKey(coordinates + direction) && grid[coordinates + direction].isWalkable)
            {
                grid[coordinates + direction].hasTowersAdjecent += (changeValue / 2f);
                //Debug.Log($"{grid[coordinates + direction]}has become {grid[coordinates + direction].hasTowersAdjecent}");
            }
        }
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, NodeClass> entery in grid)
        {
            entery.Value.connection = null;
            entery.Value.isExplored = false;
            entery.Value.isPath = false;
            entery.Value.costTillHere = 99999f;
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
