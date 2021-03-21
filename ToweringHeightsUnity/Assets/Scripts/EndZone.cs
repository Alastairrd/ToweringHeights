using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    public int lives;

    private void Update()
    {
        
    }

    private void EndZoneOutOfLives()
    {
        GameMaster.GameOver();
    }

    public void SubtractLife()
    {
        
        lives -= 1;
        if (lives <= 0)
            EndZoneOutOfLives();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Collision detected : END");
            SubtractLife();
        }
    }
}
