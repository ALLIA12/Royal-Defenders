using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int endCoordnaites;

    NodeClass startNode;
    NodeClass endNode;
    NodeClass currentSearchNode;

    Queue<NodeClass> frontier = new Queue<NodeClass>();
    PriorityQueue<NodeClass> frontierUniformCost = new PriorityQueue<NodeClass>();
    PriorityQueue<NodeClass> frontierAStar = new PriorityQueue<NodeClass>();

    Dictionary<Vector2Int, NodeClass> reached = new Dictionary<Vector2Int, NodeClass>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    Vector2Int[] directionsAll = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down, new Vector2Int(1,1), new Vector2Int(1,-1),new Vector2Int(-1,1), new Vector2Int(-1,-1)  };
    GridManager gridManager;
    Dictionary<Vector2Int, NodeClass> grid = new Dictionary<Vector2Int, NodeClass>();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.getGrid();
            startNode = grid[startCoordinates];
            endNode = grid[endCoordnaites];
        }

    }
    public Vector2Int getStartCoordinates()
    {
        return startCoordinates;
    }
    public Vector2Int getEndCoordinates()
    {
        return endCoordnaites;
    }


    void Start()
    {
        // Implement dificulty choice here here 
        // getNewPath();
        // implement uniform here
        //getUniformPath();
        // implement a* here
        getAStarPath();

    }
    ///
    public List<NodeClass> getAStarPath()
    {
        return getAStarPath(startCoordinates);
    }

    public List<NodeClass> getAStarPath(Vector2Int startCoordinates)
    {
        gridManager.ResetNodes();
        AStarSearch(startCoordinates);
        return BuildPath();
    }

    private void AStarSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        endNode.isWalkable = true;
        frontierAStar.Clear();
        reached.Clear();
        bool isRunning = true;
        grid[coordinates].costTillHere = 0;
        frontierAStar.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);
        while (frontierAStar.Count() > 0 && isRunning)
        {
            currentSearchNode = frontierAStar.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighborsAStar();
            if (currentSearchNode.coordinates == endCoordnaites)
            {
                isRunning = false;
            }
        }
    }

    private void ExploreNeighborsAStar()
    {
        List<NodeClass> neighbors = new List<NodeClass>();

        foreach (Vector2Int direction in directionsAll)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
        }

        foreach (NodeClass neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                if (currentSearchNode.costTillHere + 1 / neighbor.speed < neighbor.costTillHere)
                {
                    neighbor.connection = currentSearchNode;
                    // add some value for towers here which will be added to the cost, damage over time, type of 
                    neighbor.costTillHere = currentSearchNode.costTillHere + 1 / neighbor.speed + neighbor.hasTowersAdjecent;
                    reached.Add(neighbor.coordinates, neighbor);
                    frontierAStar.Enqueue(neighbor);
                }
            }
        }
    }


    /// <summary>
    /// Get the best path using Uniform cost search
    /// </summary>
    /// <returns> uniform cost path </returns>
    public List<NodeClass> getUniformPath()
    {
        return getUniformPath(startCoordinates);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coordinates"> the starting coordinates</param>
    /// <returns></returns>
    public List<NodeClass> getUniformPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        UniformCostSearch(coordinates);
        return BuildPath();
    }

    public List<NodeClass> getNewPath()
    {
        return getNewPath(startCoordinates);
    }


    public List<NodeClass> getNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }


    void ExploreNeighbors()
    {
        List<NodeClass> neighbors = new List<NodeClass>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
        }

        foreach (NodeClass neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connection = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void ExploreNeighborsUniform()
    {
        List<NodeClass> neighbors = new List<NodeClass>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
        }

        foreach (NodeClass neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                if (currentSearchNode.costTillHere + 1/neighbor.speed < neighbor.costTillHere) { 
                    neighbor.connection = currentSearchNode;
                    neighbor.costTillHere = currentSearchNode.costTillHere + 1/ neighbor.speed;
                    reached.Add(neighbor.coordinates, neighbor);
                    frontierUniformCost.Enqueue(neighbor);
                }
            }
        }
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        endNode.isWalkable = true;
        frontier.Clear();
        reached.Clear();
        bool isRunning = true;
        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);
        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if (currentSearchNode.coordinates == endCoordnaites)
            {
                isRunning = false;
            }
        }
    }

    void UniformCostSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        endNode.isWalkable = true;
        frontierUniformCost.Clear();
        reached.Clear();
        bool isRunning = true;
        grid[coordinates].costTillHere = 0;
        frontierUniformCost.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);
        while (frontierUniformCost.Count() > 0 && isRunning)
        {
            currentSearchNode = frontierUniformCost.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighborsUniform();
            if (currentSearchNode.coordinates == endCoordnaites)
            {
                isRunning = false;
            }
        }
    }

    List<NodeClass> BuildPath()
    {
        List<NodeClass> path = new List<NodeClass>();
        NodeClass currentNode = endNode;
        path.Add(currentNode);
        currentNode.isPath = true;
        while (currentNode.connection != null)
        {
            currentNode = currentNode.connection;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        path.Reverse();

        return path;
    }
    public bool willBlockPath(Vector2Int coordinates)
    {
        if (coordinates == endCoordnaites) return true;
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            // Add check for difficulty here
            //List<NodeClass> newPath = getNewPath();
            //List<NodeClass> newPath = getUniformPath();
            List<NodeClass> newPath = getAStarPath();
            grid[coordinates].isWalkable = previousState;
            if (newPath.Count <= 1)
            {
                // Add check for difficulty here
                //getNewPath();
                // getUniformPath();
                getAStarPath();
                return true;
            }
        }
        return false;
    }
    public void notifiyReciviers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}




