using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class CheatCodeManager : MonoBehaviour
{
    [SerializeField]
    private bool playerTyping = false;
    [SerializeField]
    private string currentString = "";
    [SerializeField]
    private List<CheatCodeInstance> cheatCodeInstances = new List<CheatCodeInstance>();
    VictoryMenu victoryMenu;
    public ParticleSystem explosion;
    // Update is called once per frame
    private void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("scoreTracking");
        victoryMenu = temp.GetComponent<VictoryMenu>();
    }
    void Update()
    {
        Cheater();
    }
    void Cheater()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (playerTyping)
            {
                CheckCheat(currentString);
            }
            playerTyping = !playerTyping;
        }
        if (playerTyping)
        {
            foreach (char c in Input.inputString)
            {
                if (c == '\b')
                {
                    if (currentString.Length > 0)
                    {
                        currentString = currentString.Substring(0, currentString.Length - 1);
                    }
                }
                else if (c == '\n' || c == '\r')
                {
                    currentString = "";
                }
                else
                {
                    currentString += c;
                }
            }
        }
    }

    private void CheckCheat(string currentString)
    {
        currentString = currentString.ToLower();
        foreach (CheatCodeInstance code in cheatCodeInstances)
        {
            if (currentString == code.code)
            {
                code.cheatEvent?.Invoke();
                break;
            }
        }
    }


    public void GiveGoldCode()
    {
        GameObject bankGO = GameObject.FindGameObjectWithTag("bank");
        Bank bank = bankGO.GetComponent<Bank>();
        bank.depostGold(99999);
        victoryMenu.score = -9999999;
    }
    public void GiveHealthCode()
    {
        GameObject bankGO = GameObject.FindGameObjectWithTag("bank");
        Bank bank = bankGO.GetComponent<Bank>();
        bank.depostHealth(99999);
        victoryMenu.score = -9999999;
    }
    public void NukeCode()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (enemies.Length == 0)
        {
            //
        }
        foreach (GameObject enemyGO in enemies)
        {
            Enemy enemy = enemyGO.GetComponent<Enemy>();
            victoryMenu.numberOfEnemiesDestroyed++;
            enemy.giveGoldOnDeath();
            Instantiate(explosion, enemy.transform.position, Quaternion.identity);
            Destroy(enemyGO);
        }
        victoryMenu.score = -9999999;
    }

}
