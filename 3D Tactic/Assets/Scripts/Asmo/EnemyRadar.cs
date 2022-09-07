using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadar : MonoBehaviour
{
    private GameObject[] enemies;
    public Transform closestEnemy;
    public bool enemyContact;


    void Start()
    {
        closestEnemy = null;
        enemyContact = false;

    }

    
    void Update()
    {
        closestEnemy = GetClosestEnemy();

        
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if(other.isTrigger != false && other.CompareTag("Enemy"))
    //    {
    //        closestEnemy = GetClosestEnemy();
    //        enemyContact = true;
            
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.isTrigger != true && other.CompareTag("Enemy"))
    //    {
    //        enemyContact = false;
    //    }
    //}


    public Transform GetClosestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform trans = null;

        foreach(GameObject go in enemies)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if(currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                trans = go.transform;
                if(currentDistance <= 2f)
                {
                    enemyContact = true;
                }
                else
                {
                    enemyContact = false; 
                }
                
            }
        }
        return trans;
        
    }
}
