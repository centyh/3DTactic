using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : MonoBehaviour
{


    public bool turn = false;

    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;

    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;

    public bool moving = false;
    public int move = 5; //Siirretään unit dataan
    public int attackMove = 3;
    public float moveSpeed = 2;
    public bool attacking = false;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0f;

    public Tile actualTargetTile;


    private void Start()
    {

    }

    protected void Init() //mahdollinen nimen muutos
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        halfHeight = GetComponent<Collider>().bounds.extents.y;
        
        TurnManager.AddUnit(this);
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile tile = null;

        if(Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();    
        }

        return tile;
    }

    public void ComputeAdjacencyLists(Tile target)
    {
        foreach(GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors(target);
        }
    }

    public void FindSelectableTiles()
    {
        if (ButtonFunctionality.moveButtonActive)
        {
            ComputeAdjacencyLists(null);
            GetCurrentTile();

            Queue<Tile> process = new Queue<Tile>();

            process.Enqueue(currentTile);
            currentTile.visited = true;
            //currentTile.parent = null      Leave as null

            while (process.Count > 0)
            {
                Tile t = process.Dequeue();

                selectableTiles.Add(t);
                t.selectable = true;


                if (t.distance < move)
                {
                    foreach (Tile tile in t.adjacencyList)
                    {
                        if (!tile.visited)
                        {
                            tile.parent = t;
                            tile.visited = true;
                            tile.distance = 1 + t.distance;
                            process.Enqueue(tile);
                        }
                    }
                }
            }
        }
        else if (ButtonFunctionality.attackButtonActive)
        {
            ComputeAdjacencyLists(null);
            GetCurrentTile();

            Queue<Tile> process = new Queue<Tile>();

            process.Enqueue(currentTile);
            currentTile.visited = true;
            //currentTile.parent = null      Leave as null

            while (process.Count > 0)
            {
                Tile t = process.Dequeue();

                selectableTiles.Add(t);
                t.selectable = true;


                if (t.distance < attackMove)
                {
                    foreach (Tile tile in t.adjacencyList)
                    {
                        if (!tile.visited)
                        {
                            tile.parent = t;
                            tile.visited = true;
                            tile.distance = 1 + t.distance;
                            process.Enqueue(tile);
                        }
                    }
                }
            }
        }
        

        
    }

   


    public void MoveToTile(Tile tile)
    {
        path.Clear();
        tile.target = true;
        moving = true;

        Tile next = tile;
        while(next != null)
        {
            path.Push(next);
            next = next.parent;
            
        }
    }

    public void Move()
    {
        if(path.Count > 0)
        {
            
            Tile t = path.Peek();
            Vector3 target = t.transform.position;

            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                CalculateHeading(target);
                SetHorizontalVelocity();
                
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;

            }
            else
            {
                //Tile center reached
                
                transform.position = target;
                path.Pop();
            }

        }
        else
        {
            RemoveSelectableTiles();
            moving = false;

            TurnManager.EndTurn(); //End turn after movement, Action Points later
        }
    }

    protected void RemoveSelectableTiles()
    {
        if(currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }

        foreach (Tile tile in selectableTiles)
        {
            tile.Reset();
        }

        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    protected Tile FindLowestF(List<Tile> list)
    {
        Tile lowest = list[0];

        foreach(Tile t in list)
        {
            if(t.f < lowest.f)
            {
                lowest = t;
            }
        }

        list.Remove(lowest);

        return lowest;
    }

    protected Tile FindEndTile(Tile t)
    {
        Stack<Tile> tempPath = new Stack<Tile>();

        Tile next = t.parent;
        while(next != null)
        {
            tempPath.Push(next);
            next = next.parent;
        }

        if(tempPath.Count <= move)
        {
            return t.parent;
        }

        Tile endTile = null;
        for(int i = 0; i <= move; i++)
        {
            endTile = tempPath.Pop();
        }

        return endTile;

    }

    protected void FindPath(Tile target)
    {
        ComputeAdjacencyLists(target);
        GetCurrentTile();

        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(currentTile);
        //currentTile.parent = ??
        currentTile.h = Vector3.Distance(currentTile.transform.position, target.transform.position);
        currentTile.f = currentTile.h;

        while(openList.Count > 0)
        {
            Tile t = FindLowestF(openList);

            closedList.Add(t);

            if(t == target)
            {
                actualTargetTile = FindEndTile(t);
                MoveToTile(actualTargetTile);
                return;
            }

            foreach(Tile tile in t.adjacencyList)
            {
                if (closedList.Contains(tile))
                {
                    //Do nothing, already processed
                }
                else if (openList.Contains(tile))
                {
                    //Check if we have faster way to location

                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);

                    if(tempG < tile.g) //If tile.g is higher than tempG, we didnt find faster way to location
                    {
                        tile.parent = t;

                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else
                {
                    tile.parent = t;

                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.g + tile.h;

                    openList.Add(tile);
                }
            }
        }

        Debug.Log("Path not found");
    }


    public void BeginTurn()
    {
        turn = true;
    }

    public void EndTurn()
    {
        turn = false;
    }
}
