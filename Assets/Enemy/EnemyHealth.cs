using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int MaxHealth;
    int currentHealth = 0;
    [Tooltip("This adds to max hp every time the enemy dies")]
    [SerializeField] int difficulityRamp = 60;
    Enemy enemy;
    private void OnEnable()
    {
        currentHealth = MaxHealth;
    }
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other)
    {
        currentHealth -= 20;
        if (currentHealth <= 0)
        {
            enemy.giveGoldOnDeath();
            MaxHealth += difficulityRamp;
            this.gameObject.SetActive(false);
        }
    }

}
