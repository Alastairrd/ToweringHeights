using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 50f;

    public GameObject fpsCamObj;
    public GameObject buildCamObj;
    public GameObject mainCam;

    private CinemachineVirtualCamera fpsCam;
    private CinemachineTrackedDolly buildCamDolly;
    private CinemachineVirtualCamera buildCam;
    
    public bool buildCamActive;

    public static CameraController camConInstance;

    private void Awake()
    {
        camConInstance = this;
    }


    public void Start()
    {
        fpsCam = fpsCamObj.GetComponent<CinemachineVirtualCamera>();
        buildCam = buildCamObj.GetComponent<CinemachineVirtualCamera>();
        buildCamDolly = buildCam.GetCinemachineComponent<CinemachineTrackedDolly>();
    }
    public void Update()
    {
        if(buildCamActive)
        {
            fpsCamObj.SetActive(false);
            float cameraInput = Input.GetAxis("Horizontal");
            buildCamDolly.m_PathPosition += cameraInput * Time.deltaTime * cameraSpeed;
            
            //RaycastTest();
            

        }
        else
        fpsCamObj.SetActive(true);    
    }

    
}
