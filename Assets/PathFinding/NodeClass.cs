using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeClass : IComparable<NodeClass>
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public NodeClass connection;
    public float speed;
    public float costTillHere = 99999f;
    public float hasTowersAdjecent = 0f;
    public NodeClass(Vector2Int coordinates, bool isWalkable, float speed)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
        this.speed = speed;
    }

    public int CompareTo(NodeClass other)
    {
        if (this.costTillHere < other.costTillHere) return -1;
        else if (this.costTillHere > other.costTillHere) return 1;
        else return 0;
    }
    override
    public string ToString() {
        return this.coordinates.ToString();
    }
}
