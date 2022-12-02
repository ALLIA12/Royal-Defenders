using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem bullet;
    [SerializeField] float shootingRange = 15f;
    Transform target;
    [SerializeField] bool shootAoe = false;
    [SerializeField] public ParticleSystem sound;
    float timer = 0;
    bool bulletSoundChecker = false;

    void Update()
    {
        if (!shootAoe)
        {
            FindClosesetEnemy();
            AimWeapon();
            if(timer > 30 && bulletSoundChecker){
                timer = 0;
                Instantiate(sound, transform.position, Quaternion.identity);
            }
        }
        else
        {
            // shoot bursts
        }
    }

void FixedUpdate(){
    if(bulletSoundChecker){
        ++timer;
    }
}
    void FindClosesetEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (enemies.Length == 0)
        {
            target = null;
            AttackToogle(false);
            return;
        }
        Transform closestEnemy = null;
        float maxDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float targetDistance = Vector3.Distance(this.transform.position, enemy.transform.position);
            if (maxDistance > targetDistance)
            {
                closestEnemy = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestEnemy;
    }

    void AimWeapon()
    {
        if (target == null) return;
        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);
        if (targetDistance <= shootingRange)
        {
            AttackToogle(true);
        }
        else
        {
            AttackToogle(false);
        }
    }
    void AttackToogle(bool isActive)
    {
        var temp = bullet.emission;
        temp.enabled = isActive;
        bulletSoundChecker = isActive;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    public void IncreaseDamage(float increase)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            return;
        }
        if (bank.getCurrentGold() >= 20)
        {
            bullet.GetComponent<ParticleHandler>().IncreaseDmage(increase);
            bank.withdrawGold(20);
        }
    }

    public void IncreaseRange(float increase)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            return;
        }
        if (bank.getCurrentGold() >= 20)
        {
            shootingRange += increase;
            bank.withdrawGold(20);
        }
    }
}
