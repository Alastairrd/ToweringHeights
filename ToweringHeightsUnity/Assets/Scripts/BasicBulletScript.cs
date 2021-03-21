using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BasicBulletScript : MonoBehaviour
{
    public Transform bulletPoint;
    public float damage = 5f;
    
    private Transform target;
    public float projectileSpeed = 70f;


    public void Seek(Transform passedTarget)
    {
        target = passedTarget;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(target == null)
        {
            Destroy(gameObject);
           return;
        }

        //Generates direction to target & rotates bullet correctly.
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(bulletPoint.rotation, lookRotation, Time.deltaTime * projectileSpeed).eulerAngles;
        bulletPoint.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

        //Determines max distance per frame
        float distanceThisFrame = Time.deltaTime * projectileSpeed;

        //determines if the bullet has reached target
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        //moves bullet forward in smooth fashion
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        EnemyScript enemyScript = target.GetComponent<EnemyScript>();
        enemyScript.health -= damage;

        Destroy(gameObject);
        
    }
}
