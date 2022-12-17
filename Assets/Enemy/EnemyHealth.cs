using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    public ParticleSystem explosion;
    [SerializeField] int MaxHealth;
    float currentHealth = 0;
    //[Tooltip("This adds to max hp every time the enemy dies")]
    //[SerializeField] int difficulityRamp = 60;
    Enemy enemy;
    VictoryMenu victoryMenu;
    private void OnEnable()
    {
        //MaxHealth += difficulityRamp;
        currentHealth = MaxHealth;
    }
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        GameObject temp = GameObject.FindGameObjectWithTag("scoreTracking");
        victoryMenu = temp.GetComponent<VictoryMenu>();
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("HIT");
        ParticleHandler particleHandler = other.GetComponent<ParticleHandler>();
        if (!particleHandler.getSlowsDown()) // doesn't slow down
        {
            currentHealth -= particleHandler.getDamage();
            if (currentHealth <= 0)
            {
                victoryMenu.numberOfEnemiesDestroyed++;
                enemy.giveGoldOnDeath();
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        else
        {
            // change slow down modifor
            enemy.gameObject.GetComponent<EnemyMover>().slowDownModifor = 1 - particleHandler.getSlowDownModifier();
        }
    }

}
