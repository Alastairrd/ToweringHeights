using System;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;
    public GameObject turret;

    private GameObject turretPreview;
    private Renderer rend;
    private Color startColor;
    private BuildManager buildManager;

    private void Start()
    {
        
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    //Builds a turret on left click if a turret is selected, and no turret exists already
    /*private void OnMouseDown()
    {
        if(buildManager.GetTurretToBuild() == null)
        {
        //TODO - remove debug log, display this
            Debug.Log("No Turret Selected");
            return;
        }
        //TODO - display this
        if(turret != null)
        {
            Debug.Log("Can't build there! A turret already exists!");
            return;
        }
        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
        buildManager.DeductMoney(turretToBuild.GetComponent<TurretScript>().cost);
        buildManager.ClearTurretToBuild();

    }
   */

    public void BuildPreview()
    {
        if (turret == null)
        {
            Debug.Log("Yay");
            rend.material.color = hoverColor;

            //if (buildManager.GetPreviewToBuild() != null)
            {
                GameObject turretPreviewToBuild = buildManager.GetPreviewToBuild();
                turretPreview = Instantiate(turretPreviewToBuild, transform.position + positionOffset, transform.rotation);
            }
        }
        
    }

    public void BuildTurret(GameObject turretToBuild)
    {
        turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
        RemovePreview();
    }

    public void RemovePreview()
    {
        rend.material.color = startColor;
        Destroy(turretPreview, 0.05f);
    }

    

}
