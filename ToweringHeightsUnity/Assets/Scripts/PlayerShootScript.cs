using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using System.Collections.Specialized;

public class PlayerShootScript : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed;
    public float weaponRange = 100f;
    public float fireCooldown;
    
    
    public Transform firePoint;

    public Camera fpsCam;
    public delegate void Notify();
    public event Notify shootEvent;
    public static PlayerShootScript instance;


    private void Awake()
    {
        instance = this;
        fpsCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Debug.DrawRay(rayOrigin, fpsCam.transform.forward * weaponRange, Color.green);

        Vector3 aimPoint;
        RaycastHit hit;

        if(Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
        {
            aimPoint = hit.point;
        }
        else
        {
            aimPoint = rayOrigin + fpsCam.transform.forward * weaponRange;
        }


        if (Input.GetMouseButtonDown(0) && fireCooldown <= 0)
        {
            GameObject projectileGO = Instantiate(projectile, firePoint.position, firePoint.localRotation);
            projectileGO.GetComponent<Rigidbody>().velocity = (aimPoint - firePoint.position).normalized * projectileGO.gameObject.GetComponent<gunProjScript>().projectileSpeed;
            
            fireCooldown = 0.75f;
            onShoot();
        }

        fireCooldown -= Time.deltaTime;
        
    }
    protected virtual void onShoot()
    {
        shootEvent?.Invoke();
    }
}
