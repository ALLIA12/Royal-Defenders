using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.ParticleSystem;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Transform upgradedWeapon;
    [SerializeField] Transform shooter;
    [SerializeField] ParticleSystem bullet;
    [SerializeField] float shootingRange = 15f;
    Transform target;
    Transform farTarget;
    [SerializeField] bool shootAoe = false;
    [SerializeField] public ParticleSystem sound;
    [SerializeField] public int upgradePenelty = 5;
    [SerializeField] public int maxUpgradePrice = 100;
    [SerializeField] public GameObject upgradeOne;
    [SerializeField] public GameObject upgradeTwo;
    [SerializeField] ParticleSystem bullet2;
    [SerializeField] ParticleSystem bullet3;
    [SerializeField] ParticleSystem bullet4;
    [SerializeField] ParticleSystem bullet5;
    [SerializeField] int maximumDmage = 0;
    [SerializeField] int maximumRange = 0;
    [SerializeField] int maximumRate = 0;
    VictoryMenu victoryMenu;
    Tower tower;
    Bank bank;
    private int upgradePrice = 20;
    float timer = 0;
    float timerSnow = 0;
    double snowSound = 250;
    bool bulletSoundChecker = false;
    float targetDistance;
    private bool fullyUpgraded = false;
    private void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("scoreTracking");
        victoryMenu = temp.GetComponent<VictoryMenu>();
        tower = GetComponent<Tower>();
        bank = FindObjectOfType<Bank>();
        if (shootAoe)
        {
            Instantiate(sound, transform.position, Quaternion.identity);
        }
    }
    void Update()
    {

        if (!shootAoe)
        {
            FindEnemies();
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
            if (timerSnow > snowSound)
            {
                timerSnow = 0;
                Instantiate(sound, transform.position, Quaternion.identity);
            }
        }
    }

    void FixedUpdate()
    {
        if (bulletSoundChecker)
        {
            ++timer;
        }
        ++timerSnow;
    }
    void FindEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (enemies.Length == 0)
        {
            target = null;
            farTarget = null;
            AttackToogle(false);
            return;
        }
        Transform closestEnemy = null;
        Transform secondToClosestEnemy = null;
        float maxDistance = Mathf.Infinity;
        float secondToMaxDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float targetDistance = Vector3.Distance(this.transform.position, enemy.transform.position);
            if (maxDistance > targetDistance)
            {
                secondToClosestEnemy = closestEnemy;
                secondToMaxDistance = maxDistance;
                closestEnemy = enemy.transform;
                maxDistance = targetDistance;
            }
            else if (targetDistance < secondToMaxDistance && targetDistance != maxDistance)
            {
                secondToClosestEnemy = enemy.transform;
                secondToMaxDistance = targetDistance;
            }
        }
        target = closestEnemy;
        farTarget = secondToClosestEnemy;

    }

    void AimWeapon()
    {
        if (target == null) return;
        if (fullyUpgraded && tower.type == 0)
        {
            upgradedWeapon.LookAt(target);
        }
        else if (tower.type == 0)
        {
            weapon.LookAt(target);
        }
        shooter.LookAt(target);
        if (fullyUpgraded && tower.type == 1 && farTarget != null)
        {
            upgradedWeapon.LookAt(farTarget);
        }
        if (farTarget == null)
        {
            upgradedWeapon.LookAt(target);

        }
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
        var emmision = bullet.emission;
        emmision.enabled = isActive;
        if (fullyUpgraded)
        {
            var emmision2 = bullet2.emission; emmision2.enabled = isActive;
            var emmision3 = bullet3.emission; emmision3.enabled = isActive;
        }
        bulletSoundChecker = isActive;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    public void IncreaseDamage(float increase)
    {
        Button button = upgradeTwo.GetComponent<Button>();
        if (!button.interactable)
        {
            return;
        }
        if (bank.getCurrentGold() >= upgradePrice)
        {
            victoryMenu.numberOfTowerUpgrades++;
            ParticleHandler particle = bullet.GetComponent<ParticleHandler>();
            particle.IncreaseDmage(increase);
            if (particle.getDamage() >= maximumDmage)
            {
                button.interactable = false;
                CheckFullyUpgraded();
            }
            bank.withdrawGold(upgradePrice);
            int newPrice = upgradePrice + upgradePenelty;
            upgradePrice = Math.Min(maxUpgradePrice, newPrice);
        }
    }
    public void IncreaseRate(float increase)
    {
        Button button = upgradeOne.GetComponent<Button>();
        if (!button.interactable)
        {
            return;
        }
        if (bank.getCurrentGold() >= upgradePrice)
        {
            victoryMenu.numberOfTowerUpgrades++;
            var emission = bullet.emission;
            emission.rateOverTime = emission.rateOverTime.constant + increase;
            bank.withdrawGold(upgradePrice);
            int newPrice = upgradePrice + upgradePenelty;
            upgradePrice = Math.Min(maxUpgradePrice, newPrice);
            if (emission.rateOverTime.constant >= maximumRate)
            {
                button.interactable = false;
                CheckFullyUpgraded();
            }
        }
    }

    public void IncreaseRange(float increase)
    {
        Button button = upgradeOne.GetComponent<Button>();
        if (!button.interactable)
        {
            return;
        }
        if (bank.getCurrentGold() >= upgradePrice)
        {
            victoryMenu.numberOfTowerUpgrades++;
            shootingRange += increase;

            bank.withdrawGold(upgradePrice);
            int newPrice = upgradePrice + upgradePenelty;
            upgradePrice = Math.Min(maxUpgradePrice, newPrice);
            if (shootingRange >= maximumRange)
            {
                button.interactable = false;
                CheckFullyUpgraded();
            }
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
            main = bullet2.main;
            main.duration = newDuration;
            main = bullet3.main;
            main.duration = newDuration;
            main = bullet4.main;
            main.duration = newDuration;
            main = bullet5.main;
            main.duration = newDuration;
            bank.withdrawGold(upgradePrice);
            int newPrice = upgradePrice + upgradePenelty;
            upgradePrice = Math.Min(maxUpgradePrice, newPrice);
            if (newDuration <= .7f)
            {
                main.duration = .7f;
                button.interactable = false;
                CheckFullyUpgraded();
            }
            timerSnow = 0;
            snowSound -= 12.5;
            bullet.Play();
            Instantiate(sound, transform.position, Quaternion.identity);
        }
    }
    public void IncreaseSlowDown(float increase)
    {
        Button button = upgradeOne.GetComponent<Button>();
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
            if (temp >= .75f)
            {
                bullet.GetComponent<ParticleHandler>().SetSlowDownModifier(.75f);
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
            fullyUpgraded = true;
        }
        print(buttonOne.interactable);
    }
    private void OnDestroy()
    {
        bank.depostGold(upgradePenelty);
    }
}
