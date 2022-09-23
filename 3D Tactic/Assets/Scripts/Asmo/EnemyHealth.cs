using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    float maxHealth = 100f;

    public Animator enemyAnim;


    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if(health <= 0)
        {
            EnemyDeath();
        }
        else
        {
            return;
        }
        

    }

    public void EnemyTakeDamage(int dmg)
    {
        if(health > 0)
        {
            health -= dmg;
            enemyAnim.SetTrigger("isHit");
        }
        
    }

    void EnemyDeath()
    {
        gameObject.GetComponent<NPCMove>().enabled = false;
        enemyAnim.SetBool("isDead", true);
        Destroy(gameObject, 5f);

    }
}
