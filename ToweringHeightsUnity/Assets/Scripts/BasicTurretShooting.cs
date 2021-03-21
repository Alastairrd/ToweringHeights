using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurretShooting : MonoBehaviour
{
    
    public GameObject projectile;
    public float fireRate;
    public float projectileSpeed;
    public GameObject target;
    public TurretScript turretScript;
    public bool isFiring;
    
    // Start is called before the first frame update
    void Start()
    {       
        isFiring = false;
    }

    private void Update()
    {
        if (turretScript.target == null)
        { 
            isFiring = false;
            return;
        }
            
        else
        {
            target = turretScript.targetObject;
            isFiring = true;
            InitiateFiring();
        }
    }

    //Called by basic turret script once range is confirmed
    void InitiateFiring()
    {
        for(; ;)
        {

        }
    }
}
