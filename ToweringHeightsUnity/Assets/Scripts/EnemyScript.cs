using System;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header ("Attributes")]
    public float speed;
    public float health;
    public int moneyReward;

    [Header ("Unity Controlled")]
    public Transform targetWaypoint;
    private int waypointIndex = 0;
    private GameMaster gameMaster;

    

    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameMaster.instance;
        targetWaypoint = Waypoints.points[0];

        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            gameMaster.enemyDeath(moneyReward);
            Destroy(gameObject);
                return;
        }    

        //Finds direction to next waypoint, moves at smooth speed, finds next waypoint when close
        Vector3 dir = targetWaypoint.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.2f) 
        { 
            GetNextWaypoint(); 
        }
       
    }

    void GetNextWaypoint()
    {
        //Destroys if at last waypoint, otherwise gets next waypoint
        if (waypointIndex == Waypoints.points.Length - 1)
        {
        
            Destroy(gameObject);
            
        }
        else
        {
            waypointIndex++;
            targetWaypoint = Waypoints.points[waypointIndex];
        }
            
        
    }


}
