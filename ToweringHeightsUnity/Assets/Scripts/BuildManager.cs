using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject standardTurretPrefab;
    public GameObject standardTurretPreview;
    //TODO add more turrets

    public static BuildManager instance;

    private GameObject turretToBuild;
    private GameObject previewToBuild;
    private Node activePreviewNodeScript;
    private GameMaster gameMaster;


    private void Awake()
    {
        instance = this;
        gameMaster = GameMaster.instance;
    }

    private void Update()
    {
        if (turretToBuild != null)
        {
            Node node = RaycastBuildPreview();
            if (node !=null)
            {
                CheckActivePreviewNode(node);
            }

            
        }

        //test building turret function
        if (GameMaster.buildPhase == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Click 0");

                if (turretToBuild == null)
                {
                    //TODO - remove debug log, display this
                    Debug.Log("No Turret Selected");
                    return;

                }

                if (activePreviewNodeScript.turret != null)
                {
                    Debug.Log("Can't build there! A turret already exists!");
                    return;
                }

                if (GameMaster.playerMoney < turretToBuild.GetComponent<TurretScript>().cost)
                {
                    Debug.Log("Not enough Money");
                    return;
                }
                activePreviewNodeScript.BuildTurret(turretToBuild);
                DeductMoney();
            }

            if (Input.GetMouseButtonDown(1))
            {
                ClearTurretToBuild();
                ClearActivePreviewNode();
            }
        }

        
        
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public GameObject GetPreviewToBuild()
    {
        return previewToBuild;
    }

    //called by shop script to set selected turret for node script
    public void SetTurretToBuild(GameObject turret, GameObject preview)
    {
        turretToBuild = turret;
        previewToBuild = preview;
    }

    public void DeductMoney()
    {      
        gameMaster.DeductMoney(turretToBuild.GetComponent<TurretScript>().cost);
    }

    //called once a turret has been built once
    public void ClearTurretToBuild()
    {
        turretToBuild = null;
        previewToBuild = null;
    }

    public void CheckActivePreviewNode(Node node)
    {
        if (node == activePreviewNodeScript)
            return;
        else if (node != activePreviewNodeScript)
        {    
            SetActivePreviewNode(node);
        }
            

    }
    public void SetActivePreviewNode(Node node)
    {
        if (activePreviewNodeScript != null)
        {
            activePreviewNodeScript.RemovePreview();
        }
        activePreviewNodeScript = node;
        node.BuildPreview();
    }

    public void ClearActivePreviewNode()
    {
        if(activePreviewNodeScript != null)
        activePreviewNodeScript.RemovePreview();
    }

    public Node RaycastBuildPreview()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        Debug.DrawRay(ray.origin, ray.direction * 150, Color.red);


        if (Physics.Raycast(ray.origin, ray.direction, out hit, 150f, 1 <<LayerMask.NameToLayer("Node")) && hit.transform.CompareTag("Node"))
        {
            GameObject node = hit.collider.gameObject;
            Node nodeScript = node.GetComponent<Node>();
            return nodeScript;
        }
        else
        {
            return null;
        }
    }
}
