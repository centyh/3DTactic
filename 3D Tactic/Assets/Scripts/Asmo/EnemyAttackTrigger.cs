using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    Animator enemyAnimator;

    bool isAttacking;

    void Start()
    {
        enemyAnimator = gameObject.GetComponent<Animator>();
        isAttacking = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyAnimator.GetComponent<Animator>().SetBool("isAttacking", true);
            Debug.Log("enemy attack");
        }
    }

    void Update()
    {

    }
}
