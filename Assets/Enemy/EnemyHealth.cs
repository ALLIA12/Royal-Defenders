using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int MaxHealth;
    float currentHealth = 0;
    [Tooltip("This adds to max hp every time the enemy dies")]
    [SerializeField] int difficulityRamp = 60;
    Enemy enemy;
    private void OnEnable()
    {
        MaxHealth += difficulityRamp;
        currentHealth = MaxHealth;
    }
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other)
    {
        currentHealth -= other.GetComponent<Damage>().getDamage();
        if (currentHealth <= 0)
        {
            enemy.giveGoldOnDeath();
            this.gameObject.SetActive(false);
        }
    }

}
