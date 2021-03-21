using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
        
    }

    //called by shop button to set selected turret to build
    public void SelectBasicTurret()
    {
        if (GameMaster.playerMoney < buildManager.standardTurretPrefab.GetComponent<TurretScript>().cost)
        {
            Debug.Log("Not enough Money");
            return;
        }
            

        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab, buildManager.standardTurretPreview);      
    }

}
