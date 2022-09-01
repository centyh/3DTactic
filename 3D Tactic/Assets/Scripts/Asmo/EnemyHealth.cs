using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public static float health;
    float maxHealth = 100f;


    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        //Debug.Log(health);
    }

    public void EnemyTakeDamage(int dmg)
    {
        if(health > 0)
        {
            health -= dmg;
        }
        else
        {
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
