using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : TacticsMove
{

    public ButtonFunctionality buttonF;



    void Start()
    {
        Init();
        actionPoints = 3;

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
            FindSelectableTiles();
            CheckMouse();
            IsAbleToAttack();
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
            if (buttonF.moveButtonActive)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Tile")
                    {
                        Tile t = hit.collider.GetComponent<Tile>();
                        if (t.selectable)
                        {
                            MoveToTile(t);
                            buttonF.MoveButtonDeactive();
                        }
                    }
                }
            } 
        }
        
    }

    void IsAbleToAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (buttonF.attackButtonActive)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag == "Enemy")
                    {
                        Debug.Log("OSUIT VIHOLLISEENN");
                        
                    }
                }
            }

        }
    }

    

}
