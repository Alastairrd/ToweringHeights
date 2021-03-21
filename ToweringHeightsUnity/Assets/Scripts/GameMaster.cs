using System;
using System.Collections;
using UnityEngine;


public class GameMaster : MonoBehaviour
{
    public static int playerMoney;
    public static int playerKills { get; private set; }

    public static int playerLives;

    public WaveSpawner waveSpawner;
    public GameObject fpsController;
    
    private CameraController camController;


    private PlayerMovement playerMovement;
    private PlayerShootScript playerShootScript;
    private WaitForSeconds endRoundWait = new WaitForSeconds(4);
    private BuildManager buildManager;
    private UIController uiController;

    public static GameMaster instance;
    public static bool buildPhase { get; private set; }
    private void Awake()
    {
        instance = this;
        
        camController = CameraController.camConInstance;
        //playerShootScript = fpsController.GetComponent<PlayerShootScript>();
        
        playerMovement = fpsController.GetComponent<PlayerMovement>();
        buildPhase = true;      
    }

    // Start is called before the first frame update
    void Start()
    {
        uiController = UIController.instance;
        buildManager = BuildManager.instance;
        playerShootScript = PlayerShootScript.instance;
        playerShootScript.enabled = false;
        playerMovement.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(buildPhase == true)
        {
            camController.buildCamActive = true;

            if (Input.GetKeyDown("return"))
            {
                Debug.Log("start");
                WaveBegin();
                camController.buildCamActive = false;
            }
        }
    }

    public void WaveBegin()
    {
        Debug.Log("wavebegin");
        //disable building
        buildPhase = false;
        //uiController.SetFPSPhase();
        buildManager.SetTurretToBuild(null, null);
        buildManager.ClearActivePreviewNode();

        
        EnableFPS();

        //TODO change ui

       

        //begins wave spawn countdown
        waveSpawner.countdownBool = true;
        
    }

    public void DisableFPS()
    {
        playerMovement.enabled = false;
        playerShootScript.enabled = false;
        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
    }

    public void EnableFPS()
    {
        uiController.SetFPSPhase();
        playerMovement.enabled = true;
        playerShootScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public IEnumerator WaveOver()
    {
        //Delay for 4 seconds before changing states
        for (int i = 0; i < 1; i++)
        {           
            yield return endRoundWait;
            i++;
        }
        //enables building
        buildPhase = true;
        uiController.SetBuildPhase();



        //TODO change ui
        DisableFPS();
        //disables fps controls and camera
        
    }

    public void DeductMoney(int cost)
    {
        playerMoney -= cost;
        uiController.UpdateCounts();
    }

    public void enemyDeath(int moneyReward)
    {
        playerKills++;
        playerMoney += moneyReward;
        uiController.UpdateCounts();       
    }

    internal static void GameOver()
    {
        Debug.Log("Game Over");
    }
}
