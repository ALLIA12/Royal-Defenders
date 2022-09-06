using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeClass
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public NodeClass connection;
    public NodeClass(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
