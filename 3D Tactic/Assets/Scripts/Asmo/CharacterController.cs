using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : TacticsMove
{

    public bool attackButtonActive = false;
    public bool moveButtonActive = false;


    void Start()
    {
        Init();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);

        if (!turn)
        {
            return;
        }

        if (!moving)
        {
            curState = GameState.PlayerTurn;
            FindSelectableTiles();
            CheckMouse();
                   
        }

        else
        {
            Move();
        }
        
    }

    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();
                    if (t.selectable)
                    {
                        MoveToTile(t);
                    }
                }
            }
        }
    }

    public void AttackButtonActive()
    {
        attackButtonActive = true;
        Debug.Log("ATTACK BUTTON ACTIVE");
    }

    public void MoveButtonActive()
    {
        moveButtonActive = true;
        Debug.Log("MOVE BUTTON ACTIVE");
    }

    

}
