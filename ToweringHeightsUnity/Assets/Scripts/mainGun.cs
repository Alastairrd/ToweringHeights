using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainGun : MonoBehaviour
{
    Animator anim;
    public Transform mainBattery;
    public ParticleSystem batteryHeatPrefab;
    PlayerShootScript playerShootScript;
    // Start is called before the first frame update
    void Start()
    {
        playerShootScript = PlayerShootScript.instance;
        anim = GetComponent<Animator>();
        playerShootScript.shootEvent += fps_onShoot;
    }

    public void fps_onShoot()
    {
        anim.SetTrigger("FireTrigger");
        ParticleSystem batteryHeatInst = (ParticleSystem)Instantiate(batteryHeatPrefab, mainBattery.position, mainBattery.rotation);
        batteryHeatInst.transform.SetParent(mainBattery);
        Destroy(batteryHeatInst, 0.4f);
    }

    

}
