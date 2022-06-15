using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;
    public Transform startPosition;
    public Transform targetPosition;

    private void Awake()
    {
        grid = GetComponent<Grid>();

    }

    private void Update()
    {
        FindPath(startPosition.position, targetPosition.position);
    }

    void FindPath(Vector3 a_startPos, Vector3 a_targetPos)
    {
        Node startNode = grid.NodeFromWorldPosition(a_startPos);
        Node targetNode = grid.NodeFromWorldPosition(a_targetPos);

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(startNode);

        while(openList.Count > 0)
        {
            Node currentNode = openList[0];
            for(int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if(currentNode == targetNode)
            {
                GetFinalPath(startNode, targetNode);
                break;
            }
            

            foreach (Node neighborNode in grid.GetNeighboringNodes(currentNode))
            {
                if(!neighborNode.isWall || closedList.Contains(neighborNode))
                {
                    continue;
                }
                int moveCost = currentNode.gCost + GetManhattenDistance(currentNode, neighborNode);

                if(!openList.Contains(neighborNode) || moveCost < neighborNode.fCost)
                {
                    neighborNode.gCost = moveCost;
                    neighborNode.hCost = GetManhattenDistance(neighborNode, targetNode);
                    neighborNode.Parent = currentNode;

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }
    }

    void GetFinalPath(Node a_startingNode, Node a_endNode)
    {
        List<Node> finalPath = new List<Node>();
        Node currentNode = a_endNode;

        while(currentNode != a_startingNode)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        finalPath.Reverse();

        grid.finalPath = finalPath;
    }

    int GetManhattenDistance(Node a_nodeA, Node a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.gridX - a_nodeB.gridX);
        int iy = Mathf.Abs(a_nodeA.gridY - a_nodeB.gridY);

        return ix + iy;
    }
}
