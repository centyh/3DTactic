using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadar : MonoBehaviour
{
    private GameObject[] enemies;
    public Transform closestEnemy;
    public bool enemyContact;

    private GameObject portal;
    public Transform closestPortal;
    public bool portalContact;

    Vector3 _direction;
    Quaternion _lookRotation;
    public float rotationSpeed;

    void Start()
    {
        closestEnemy = null;
        enemyContact = false;

        closestPortal = null;
        portalContact = false;
    }

    
    void Update()
    {
        closestEnemy = GetClosestEnemy();
        closestPortal = GetClosestPortal();

        if (enemyContact)
        {
            //LookTowardsEnemy();
        }
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

    public Transform GetClosestPortal()
    {
        portal = GameObject.FindGameObjectWithTag("Portal");
        float closestPortal = Mathf.Infinity;
        float currentDis = Vector3.Distance(transform.position, portal.transform.position);
        Transform transf = null;

        if(currentDis < closestPortal)
        {
            closestPortal = currentDis;
            transf = portal.transform;
            if(currentDis <= 2f)
            {
                portalContact = true;
            }
            else
            {
                portalContact = false;
            }
        }
        return transf;

    }

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

    //void LookTowardsEnemy()
    //{
    //    _direction = (closestEnemy.position - transform.position).normalized;
    //    _lookRotation = Quaternion.LookRotation(_direction);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
    //}
}
