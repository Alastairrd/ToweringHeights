using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    //temp hardcode of turn speed and range
    [Header ("Attributes")]
    public float turnSpeed;
    public float range;
    public int cost;
    public float cooldown;
    private float cooldownDynamic;
    public GameObject projectile;
    public GameObject previewTurret;

    [Header("Unity Controlled")]
    public Transform target;
    public GameObject targetObject;
    public Transform rotator;
    public Transform firePoint;
    private Quaternion _initialRotation;
    
    

    // Start is called before the first frame update
    void Start()
    {
        _initialRotation = rotator.rotation;
        target = null;
        cooldownDynamic = cooldown;
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        //Sets turret rotation to resting position smoothly and does nothing else
        if (target == null)
        {
            rotator.rotation = Quaternion.RotateTowards(rotator.rotation, _initialRotation, Time.deltaTime * turnSpeed + 1.5f);

            return;
        }
            
        //If turret has a target, matches rotation to target's position smoothly.
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotator.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        rotator.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);

        if(cooldownDynamic <=0)
        {
            Fire();
            cooldownDynamic = cooldown;
        }

        cooldownDynamic -= Time.deltaTime;
    }

    private void Fire()
    {
        //CURRENTLY STARTING WRONG ROTATION
        GameObject projectileGO = Instantiate(projectile, firePoint.position, firePoint.rotation);
        BasicBulletScript bulletScript = projectileGO.GetComponent<BasicBulletScript>();

        if(bulletScript != null)
            bulletScript.Seek(target);
       
        
    }

    void UpdateTarget()
    {
        //Gets all enemies and sorts through to find closest
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        //if an enemy exists and closest enemy is within range, sets that to current target
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetObject = nearestEnemy.gameObject;
            
        }
        else
        {
            target = null;
            targetObject = null;
        }
    }
}
