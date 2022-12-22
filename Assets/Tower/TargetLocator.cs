using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Transform shooter;
    [SerializeField] ParticleSystem bullet;
    [SerializeField] float shootingRange = 15f;
    Transform target;
    [SerializeField] bool shootAoe = false;
    [SerializeField] public ParticleSystem sound;
    [SerializeField] public int upgradePenelty = 5;
    [SerializeField] public int maxUpgradePrice = 100;
    [SerializeField] public GameObject upgradeOne;
    [SerializeField] public GameObject upgradeTwo;
    VictoryMenu victoryMenu;
    Tower tower;
    Bank bank;
    private int upgradePrice = 20;
    float timer = 0;
    bool bulletSoundChecker = false;
    float targetDistance;
    private void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("scoreTracking");
        victoryMenu = temp.GetComponent<VictoryMenu>();
        tower = GetComponent<Tower>();
        bank = FindObjectOfType<Bank>();
    }
    void Update()
    {

        if (!shootAoe)
        {
            FindClosesetEnemy();
            AimWeapon();
            if (timer > 40 && bulletSoundChecker)
            {
                timer = 0;
                Instantiate(sound, transform.position, Quaternion.identity);
            }
        }
        else
        {
            // shoot bursts
        }
    }

    void FixedUpdate()
    {
        if (bulletSoundChecker)
        {
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
        weapon.LookAt(target);
        //shooter.LookAt(target);
        targetDistance = Vector3.Distance(transform.position, target.position);
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
        if (bank.getCurrentGold() >= upgradePrice)
        {
            victoryMenu.numberOfTowerUpgrades++;
            bullet.GetComponent<ParticleHandler>().IncreaseDmage(increase);
            bank.withdrawGold(upgradePrice);
            int newPrice = upgradePrice + upgradePenelty;
            upgradePrice = Math.Min(maxUpgradePrice, newPrice);
        }
    }
    public void IncreaseRate(float increase)
    {
        if (bank.getCurrentGold() >= upgradePrice)
        {
            victoryMenu.numberOfTowerUpgrades++;
            var emission = bullet.emission;
            emission.rateOverTime = emission.rateOverTime.constant + increase;
            bank.withdrawGold(upgradePrice);
            int newPrice = upgradePrice + upgradePenelty;
            upgradePrice = Math.Min(maxUpgradePrice, newPrice);
        }
    }

    public void IncreaseRange(float increase)
    {
        if (bank.getCurrentGold() >= upgradePrice)
        {
            victoryMenu.numberOfTowerUpgrades++;
            shootingRange += increase;
            bank.withdrawGold(upgradePrice);
            int newPrice = upgradePrice + upgradePenelty;
            upgradePrice = Math.Min(maxUpgradePrice, newPrice);
        }
    }
    public void DecreaseDuration(float decrease)
    {
        Button button = upgradeOne.GetComponent<Button>();
        if (!button.interactable)
        {
            return;
        }
        if (bank.getCurrentGold() >= upgradePrice)
        {
            victoryMenu.numberOfTowerUpgrades++;
            bullet.Stop();
            bullet.Clear();
            var main = bullet.main;
            float newDuration = main.duration - decrease;
            main.duration = newDuration;
            bank.withdrawGold(upgradePrice);
            int newPrice = upgradePrice + upgradePenelty;
            upgradePrice = Math.Min(maxUpgradePrice, newPrice);
            if (newDuration <= 1)
            {
                main.duration = 1;
                button.interactable = false;
                CheckFullyUpgraded();
            }
            bullet.Play();
        }
    }
    public void IncreaseSlowDown(float increase)
    {
        Button button = upgradeTwo.GetComponent<Button>();
        if (!button.interactable)
        {
            return;
        }
        if (bank.getCurrentGold() >= upgradePrice)
        {
            victoryMenu.numberOfTowerUpgrades++;
            bullet.GetComponent<ParticleHandler>().IncreaseSlowDownModifier(increase);
            bank.withdrawGold(upgradePrice);
            int newPrice = upgradePrice + upgradePenelty;
            upgradePrice = Math.Min(maxUpgradePrice, newPrice);
            float temp = bullet.GetComponent<ParticleHandler>().getSlowDownModifier();
            // if we go past this point, its too powerful
            if (temp >= .90f)
            {
                bullet.GetComponent<ParticleHandler>().SetSlowDownModifier(.90f);
                button.interactable = false;
                CheckFullyUpgraded();
            }
        }
    }

    public void CheckFullyUpgraded()
    {
        Button buttonTwo = upgradeTwo.GetComponent<Button>();
        Button buttonOne = upgradeOne.GetComponent<Button>();
        if (!buttonOne.interactable && !buttonTwo.interactable)
        {
            tower.FullyUpgraded();
        }
        print(buttonOne.interactable);
    }
    private void OnDestroy()
    {
        bank.depostGold(upgradePenelty);
    }
}
