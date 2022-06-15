using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform startPosition;
    public LayerMask WallMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float distance;

    Node[,] grid;
    public List<Node> finalPath;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        for(int y = 0; y < gridSizeX; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool wall = true;

                if(Physics.CheckSphere(worldPoint, nodeRadius, WallMask))
                {
                    wall = false;
                }

                grid[x, y] = new Node(wall, worldPoint, x, y);
            }
                
        }
    }

    public Node NodeFromWorldPosition(Vector3 a_worldPosition)
    {
        float xPoint = ((a_worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float yPoint = ((a_worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y);

        xPoint = Mathf.Clamp01(xPoint);
        yPoint = Mathf.Clamp01(yPoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xPoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * yPoint);

        return grid[x, y];
    }

    public List<Node> GetNeighboringNodes(Node a_node)
    {
        List<Node> NeighboringNodes = new List<Node>();
        int xCheck;
        int yCheck;

        //Right side
        xCheck = a_node.gridX + 1;
        yCheck = a_node.gridY;
        if(xCheck >= 0 && xCheck < gridSizeX)
        {
            if(yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Left side
        xCheck = a_node.gridX - 1;
        yCheck = a_node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Top side
        xCheck = a_node.gridX;
        yCheck = a_node.gridY + 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Bottom side
        xCheck = a_node.gridX;
        yCheck = a_node.gridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        return NeighboringNodes;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if(grid != null)
        {
            foreach(Node node in grid) // Loop through every node in the grid
            {
                if (node.isWall) // If the current node is a wall node
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }
                if(finalPath != null) // If the final path is not empty
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawCube(node.position, Vector3.one * (nodeDiameter - distance)); // Draw the node at the position of the node
            }
        }
    }
}
