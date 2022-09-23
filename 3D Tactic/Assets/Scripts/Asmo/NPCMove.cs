using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticsMove
{
    GameObject target;
    bool playerContact;
    public Transform closestPlayer;
    public AudioSource enemySwordHit;

    public Animator enemyAnimator;
    public EnemyHealth enemyH;

    bool enemyTurn;
    public static bool enemyAttacking;

    void Start()
    {
        //enemyAnimator = GetComponent<Animator>();

        Init();

        closestPlayer = null;
        playerContact = false;
    }


    void Update()
    {
        closestPlayer = GetClosestPlayer();
        Debug.DrawRay(transform.position, transform.forward);
        Debug.Log("ENEMY ATTACKING BOOLEAN: " + enemyAttacking);

        if (!turn || this.gameObject == null)
        {
            enemyTurn = false;
            enemyAnimator.SetBool("isWalking", false);
            return;
        }

        if (!moving)
        {
            enemyTurn = true;
            Debug.Log("ENEMY TURN");
            enemyAnimator.SetBool("isWalking", false);
            AttackPlayer();
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();
            actualTargetTile.target = true;
        }

        else
        {
            Move();
            enemyAnimator.SetBool("isWalking", true);
        }

        
    }

    void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }

    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach(GameObject obj in targets)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);
            
            if(d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }

        target = nearest;
    }

    public Transform GetClosestPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        float closestDistance = Mathf.Infinity;
        Transform trans = null;

        float currentDistance;
        currentDistance = Vector3.Distance(transform.position, target.transform.position);
        if (currentDistance < closestDistance)
        {
            closestDistance = currentDistance;
            trans = target.transform;
            if (currentDistance <= 1.5f)
            {
                playerContact = true;
            }
            else
            {
                playerContact = false;
            }

        }
        return trans;

    }

    void AttackPlayer()
    {
        if(playerContact && enemyTurn && enemyH.health > 0)
        {
            
            StartCoroutine(WaitUntilAttack());
        }
        else
        {
            enemyAttacking = false;
        }
    }

    IEnumerator WaitUntilAttack()
    {

        yield return new WaitForSeconds(1f);
        int randomDamage = Random.Range(15, 30);
        enemyAttacking = true;
        enemyAnimator.SetTrigger("enemyAttacking");
        enemySwordHit.Play();
        PlayerHealth.health -= randomDamage;
    }
}
