using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    //TODO make compatible with multiple wavespawners - some other scripts rely on 1 spawner like game master and UI
    [Header ("Attributes")]
    public GameObject enemyPrefab;
    public GameObject enemy2Prefab;
    public float timeUntilSpawn = 5f;
    public float enemiesPerWave = 0f;

    [Header ("Unity Controlled")]
    public float countdown;
    public bool countdownBool;
    public Text visualCountdown;
    private GameObject[] enemyCount;
    
    public int waveIndex { get; private set; }
    private bool waveIncoming;
    public GameMaster gameMaster;
    public string waveStatus { get; private set; }

   
   
    // Start is called before the first frame update
    void Start()
    {
        countdown = timeUntilSpawn;        
    }

    // Update is called once per frame
    void Update()
    {
        if(countdownBool == true)
        {
            if(countdown <= 0)
            {
                StartCoroutine("SpawnWave");
                InvokeRepeating("CheckForEnemies", 0f, 1f);
                countdown = timeUntilSpawn;
                countdownBool = false;
                return;
            }
            //TODO - call ui controller for this
            waveStatus = ($" Next wave in: {Math.Round(countdown).ToString()}");
            countdown -= Time.deltaTime;
        }       
    }
    private void CheckForEnemies()
    {
        enemyCount = (GameObject.FindGameObjectsWithTag("Enemy"));
        if (enemyCount.Length == 0 && waveIncoming == false)
        {
            gameMaster.StartCoroutine("WaveOver");
            CancelInvoke("CheckForEnemies");
            waveStatus = $"Wave {waveIndex} Complete";
        }
        else if (waveIncoming == false)
        {
            //TODO - call ui controller for this
            waveStatus = ($"There are {enemyCount.Length} enemies remaining");
        }
        else
            //TODO call ui controller
            waveStatus = "Wave Incoming";
    }

    IEnumerator SpawnWave()
    {
        waveIncoming = true;
        waveIndex++;
        enemiesPerWave = waveIndex * 2 + 2;

        //Determines which enemy to spawn this wave
        var enemyThisWave = enemyPrefab;
        if (waveIndex % 5 == 0)
        {
            enemyThisWave = enemy2Prefab;
        }

        //Spawns desired enemy as per enemiesPerWave
        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy(enemyThisWave);
            yield return new WaitForSeconds(1f);
        }
        waveIncoming = false;
    }
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }
}
