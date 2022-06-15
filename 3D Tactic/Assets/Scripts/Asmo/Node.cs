using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int gridX, gridY;

    public bool isWall;
    public Vector3 position;

    public Node Parent; // For the A* Algorithm, will store what node it previously came from so it can trace the shortest path

    public int gCost;
    public int hCost;

    public int fCost { get { return gCost + gCost; } }

    public Node(bool a_isWall, Vector3 a_Pos, int a_gridX, int a_gridY)
    {
        isWall = a_isWall; // Tells the program if this node is being obstructed
        position = a_Pos; // The world position of the node
        gridX = a_gridX; // X Position in the Node Array
        gridY = a_gridY; // Y Position in the Node Array
    }

}
