using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem bullet;
    [SerializeField] float shootingRange = 15f;
    Transform target;


    void Update()
    {
        FindClosesetEnemy();
        AimWeapon();
    }
    void FindClosesetEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestEnemy = null;
        float maxDistance = Mathf.Infinity;
        foreach (Enemy enemy in enemies)
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
    }
}
