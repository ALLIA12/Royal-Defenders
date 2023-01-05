using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockAbilityCode : MonoBehaviour
{
    public ParticleSystem explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            GameObject temp = GameObject.FindGameObjectWithTag("scoreTracking");
            VictoryMenu victoryMenu = temp.GetComponent<VictoryMenu>();
            victoryMenu.numberOfEnemiesDestroyed++;
            enemy.giveGoldOnDeath();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(enemy.gameObject);
        }
        
    }
    
}
