
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class UIController : MonoBehaviour
{
    public Text waveStatus;
    public Text pressEnter;
    public Text moneyCount;
    public Text killCount;
    public WaveSpawner waveSpawner;
    public GameObject fpsPanel;
    public GameObject buildPanel;

    
    public static UIController instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetBuildPhase();
    }

    // Update is called once per frame
    // Could change this to methods called by Game Master
    private void Update()
    {
        if (GameMaster.buildPhase == true) // start of game and in between waves
        {
            pressEnter.text = $"PRESS 'ENTER' TO BEGIN WAVE: {waveSpawner.waveIndex + 1}";
                //Testing Raycast
            

        }
        else //Game is in FPS mode
        {
            waveStatus.text = waveSpawner.waveStatus;

        }
    }

    public void SetBuildPhase()
    {
        buildPanel.SetActive(true);
        fpsPanel.SetActive(false);
    }

    public void SetFPSPhase()
    {
        buildPanel.SetActive(false);
        fpsPanel.SetActive(true);
    }

    public void UpdateCounts()
    {
        killCount.text = $"Kills: {GameMaster.playerKills}";
        moneyCount.text = $"Money: {GameMaster.playerMoney}";
    }

   
}
