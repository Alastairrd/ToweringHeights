using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunProjScript : MonoBehaviour
{
    public Vector3 aimPoint;
    public float projectileSpeed = 30f;
    public float projectileDamage = 2f;
    public GameObject hitEffect;

    private Vector3 velocity;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyScript enemyScript = other.gameObject.GetComponent<EnemyScript>();
            enemyScript.health -= projectileDamage;

            
        }

        GameObject effectInst = Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effectInst, 2f);
        Destroy(gameObject);    

    }

}
