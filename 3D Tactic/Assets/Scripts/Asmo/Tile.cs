using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;

    public List<Tile> adjacencyList = new List<Tile>();

    //Needed BFS
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

    //A*
    public float f = 0; // G + H, used to find the best path
    public float g = 0; // Cost from parent to the current tile
    public float h = 0; // Cost from current tile to destionation


    void Start()
    {
        
    }

    
    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color = Color.black;
        }
        else if(target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }


    }

    public void Reset()
    {
        adjacencyList.Clear();

        walkable = true;
        current = false;
        target = false;
        selectable = false;

        visited = false;
        parent = null;
        distance = 0;

        f = g = h = 0;
    }

    public void FindNeighbors(Tile target)
    {
        Reset();

        CheckTile(Vector3.forward, target);
        CheckTile(-Vector3.forward, target);
        CheckTile(Vector3.right, target);
        CheckTile(-Vector3.right, target);
    }

    public void CheckTile(Vector3 direction, Tile target)
    {
        Vector3 halfExtents = new Vector3(0.25f, 0.25f, 0.25f);

        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach(Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if(tile != null && tile.walkable)
            {
                RaycastHit hit;

                if(!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) || (tile == target))
                {
                    adjacencyList.Add(tile);
                }  
            }
        }
    }
}
