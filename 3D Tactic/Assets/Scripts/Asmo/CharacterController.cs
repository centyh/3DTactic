using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterController : TacticsMove
{

    public ButtonFunctionality buttonF;
    public EnemyHealth enemyHealth;
    public APSpriteChange apSpriteChange;
    public EnemyRadar radar;

    public Animator playerAnim;
    public PlayerHealth playerHealth;

    public AudioSource swordHit;

    public bool playerAlive;
    public bool raycastBlocked;
    public bool playerTurn;
    

    void Start()
    {
        raycastBlocked = false;
        playerAlive = true;
        Init();

        
    }

    

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);
        PlayerDeath();



        if (!turn)
        {
            playerAnim.SetBool("isWalking", false);
            playerTurn = false;
            return;
        }

        if (!moving)
        {
            playerTurn = true;
            Debug.Log(playerTurn);
            OnMouseHover();
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
        if (ButtonFunctionality.moveButtonActive && turn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Tile" && !raycastBlocked)
                    {
                        Tile t = hit.collider.GetComponent<Tile>();
                        if (t.selectable)
                        {
                            MoveToTile(t);
                            buttonF.MoveButtonDeactive();
                            apSpriteChange.APUsed();


                        }     
                    }
                }               
            } 
        }
        
    }

    void IsAbleToAttack()
    {
        if (ButtonFunctionality.attackButtonActive && radar.enemyContact && turn)
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
                        int randDmg = Random.Range(40, 60);
                        enemyHealth.EnemyTakeDamage(randDmg);
                        Debug.Log(randDmg);

                        TurnManager.EndTurn();

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

    void PlayerDeath()
    {
        if(PlayerHealth.health <= 0)
        {
            playerAlive = false;
            playerAnim.SetBool("isDead", true);
        }
        else
        {
            playerAnim.SetBool("isDead", false);
        }
        
    }

    private void OnMouseHover()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            raycastBlocked = true;
            Debug.Log("Raycast Blocked");
            return;
        }
        else
        {
            raycastBlocked = false;
        }
    }

}
