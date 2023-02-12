using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockAbilityCode : MonoBehaviour
{
    public ParticleSystem explosion;
    public LayerMask layer;

    int timer = 0;

    //if timer has reached make particle explosion
    void Update(){
        if(timer >= 142){
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }

    void FixedUpdate(){
        timer++;
        if(timer >= 143) timer = 0;
    }
    
    //if meteor hitbox touches enemy hitbox destroy enemy instantly
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            GameObject temp = GameObject.FindGameObjectWithTag("scoreTracking");
            VictoryMenu victoryMenu = temp.GetComponent<VictoryMenu>();
            victoryMenu.numberOfEnemiesDestroyed++;
            enemy.giveGoldOnDeath();
            Destroy(enemy.gameObject);
        }

        if(other.gameObject.layer == layer.value){
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        
    }
    
}
