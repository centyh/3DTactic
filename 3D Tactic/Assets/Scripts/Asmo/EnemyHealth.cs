using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public static float health;
    float maxHealth = 100f;

    public Animator enemyAnim;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        EnemyDeath();
    }

    public void EnemyTakeDamage(int dmg)
    {
        if(health > 0)
        {
            health -= dmg;
        }
        
    }

    void EnemyDeath()
    {
        if(health <= 0)
        {
            enemyAnim.SetBool("isDead", true);
            //Destroy(gameObject);
        }
        
    }
}
