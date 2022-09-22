using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : TacticsMove
{

    public ButtonFunctionality buttonF;
    public EnemyHealth enemyHealth;
    public APSpriteChange apSpriteChange;
    public EnemyRadar radar;

    public Animator playerAnim;

    public AudioSource swordHit;

    

    

    void Start()
    {
        Init();
        playerActionPoints = 3;
        
    }

    

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);
        Debug.Log("PLAYER APS: " + playerActionPoints);


        if (!turn)
        {
            playerAnim.SetBool("isWalking", false);
            return;
        }

        if (!moving)
        {
            FindSelectableTiles();
            CheckMouse();
            IsAbleToAttack();
            PressPortal();
            playerAnim.SetBool("isWalking", false);
           

        }

        else
        {
            Move();
            playerAnim.SetBool("isWalking", true);
            
        }

        
        
    }

    void CheckMouse()
    {
        if (ButtonFunctionality.moveButtonActive)
        {
            if (Input.GetMouseButtonDown(0))
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
                            apSpriteChange.APUsed();
                            playerActionPoints -= 1;

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
                        swordHit.Play();
                        int randDmg = Random.Range(40, 75);
                        enemyHealth.EnemyTakeDamage(randDmg);
                        Debug.Log(randDmg);
                        playerActionPoints -= 1;
                        apSpriteChange.APUsed();
                        

                    }
                }
            }

        }
    }

    void PressPortal()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Portal")
                {
                    UIManager.gameComplete = true;
                }
            }
        }
    }

    

}
