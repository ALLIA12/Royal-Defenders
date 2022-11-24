using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int MaxHealth;
    float currentHealth = 0;
    //[Tooltip("This adds to max hp every time the enemy dies")]
    //[SerializeField] int difficulityRamp = 60;
    Enemy enemy;
    private void OnEnable()
    {
        //MaxHealth += difficulityRamp;
        currentHealth = MaxHealth;
    }
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticleHandler particleHandler = other.GetComponent<ParticleHandler>();
        if (!particleHandler.getSlowsDown()) // doesn't slow down
        {
            currentHealth -= particleHandler.getDamage();
            if (currentHealth <= 0)
            {
                enemy.giveGoldOnDeath();
                Destroy(this.gameObject);
            }
        }
        else
        {
            // change slow down modifor
            // TDL make it variable
            enemy.gameObject.GetComponent<EnemyMover>().slowDownModifor = .5f; 
        }
    }

}
