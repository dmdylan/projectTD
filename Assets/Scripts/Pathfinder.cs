using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Vector2Int[] directions = 
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
        ExploreNeighbors(startWaypoint);
        PathFind();
    }

    private void ExploreNeighbors(Waypoint from)
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int explorationCoordinates = from.GetGridPos() + direction;
            try
            {
                QueueNewNeightbors(explorationCoordinates);
            }
            catch
            {

            }
        }
    }

    private void QueueNewNeightbors(Vector2Int explorationCoordinates)
    {
        Waypoint neighbor = grid[explorationCoordinates];
        if (neighbor.isExplored)
        {

        }
        else
        {
            neighbor.SetTopColor(Color.blue);
            queue.Enqueue(neighbor);
        }

    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Overlapping " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }    
        }
    }

    private void PathFind()
    {
        queue.Enqueue(startWaypoint);

        while(queue.Count > 0 && isRunning)
        {
            var searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            print("Searching from " + searchCenter);
            HaltIfEndFound(searchCenter);
            ExploreNeighbors(searchCenter);
        }
        print("Finished pathfinding?");
    }

    private void HaltIfEndFound(Waypoint searchCenter)
    {
        if(searchCenter == endWaypoint)
        {
            isRunning = false;
            print("Searching from end node, therefore stopping");
        }
    }
}
