using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : TacticsMove
{

    public ButtonFunctionality buttonF;
    public EnemyHealth enemyHealth;
    public APSpriteChange apSpriteChange;
    public EnemyRadar radar;

    

    void Start()
    {
        Init();
        actionPoints = 10;


        
    }

    

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);


        if (!turn)
        {
            return;
        }

        if (!moving && actionPoints > 0)
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
            if (ButtonFunctionality.moveButtonActive)
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
                            actionPoints -= 1;
                            apSpriteChange.APUsed();
                            

                        }     
                    }
                }               
            } 
        }
        
    }

    void IsAbleToAttack()
    {
        if (ButtonFunctionality.attackButtonActive && radar.enemyContact)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag == "Enemy")
                    {
                        
                        playerAnim.SetTrigger("attacking");
                        int randDmg = Random.Range(40, 75);
                        enemyHealth.EnemyTakeDamage(randDmg);
                        Debug.Log(randDmg);
                        actionPoints -= 1;
                        apSpriteChange.APUsed();
                    }
                }
            }

        }

    }

    

}
