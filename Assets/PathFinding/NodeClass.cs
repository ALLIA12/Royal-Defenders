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
    public float speedTillHere = float.MaxValue;
    public NodeClass(Vector2Int coordinates, bool isWalkable, float speed)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
        this.speed = speed;
    }

    public int CompareTo(NodeClass other)
    {
        if (this.speedTillHere + other.speed< other.speedTillHere) return -1;
        else if (this.speed > other.speed) return 1;
        else return 0;
    }
    public String ToString()
    {
        return this.coordinates + " speed is: " + this.speed;
    }
}
