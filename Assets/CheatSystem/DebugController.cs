using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    bool showConsole = false;
    bool showHelp = false;
    string input;
    public static DebugCommand nuke;
    public static DebugCommand gold;
    public static DebugCommand health;
    public static DebugCommand bing;
    public static DebugCommand help;
    public static DebugCommand aman;
    public List<object> commandList;
    private void Awake()
    {
        nuke = new DebugCommand("nuke", "Kills all the enemies currently in the scene", "nuke", () =>
        {
            NukeCode();
        });
        gold = new DebugCommand("gold", "Gain 99999 Gold", "gold", () =>
        {
            GiveGoldCode();
        });
        health = new DebugCommand("health", "Gain 99999 health", "health", () =>
        {
            GiveHealthCode();
        });
        bing = new DebugCommand("bingchilling", "Bing Chilling", "bingchilling", () =>
        {
            BingChillingCode();
        });
        help = new DebugCommand("help", "Shows a list of commands", "help", () =>
        {
            showHelp = true;
        });
        aman = new DebugCommand("aman", "By the grace of all mighty allah", "aman", () =>
        {
            showHelp = true;
        });
        commandList = new List<object>()
        {
            nuke,
            gold,
            health,
            bing,
            help
        };
    }
    VictoryMenu victoryMenu;
    public ParticleSystem explosion;
    public GameObject bingChilling;
    public GameObject amanUllah;
    // Update is called once per frame
    private void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("scoreTracking");
        victoryMenu = temp.GetComponent<VictoryMenu>();
    }
    public void OnToggleDebug()
    {
        showConsole = !showConsole;
    }
    public void OnReturn()
    {
        if (showConsole)
        {
            HandleInput();
            input = "";
        }
    }
    Vector2 scroll;
    private void OnGUI()
    {
        if (!showConsole) { return; }
        GUI.skin.label.fontSize = 30;
        GUI.skin.textField.fontSize = 30;
        float y = 0;
        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewPort = new Rect(0, 0, Screen.width - 30, 40 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 10f, Screen.width, 90), scroll, viewPort);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                string label = $"{command.commandFormat} - {command.commandDescreption}";

                Rect labelRect = new Rect(5, 40 * i, viewPort.width - 100, 40f);

                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();
            y += 100;
        }
        GUI.Box(new Rect(0, y, Screen.width, 50), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);

        input = GUI.TextField(new Rect(20f, y + 10f, Screen.width - 30f, 40f), input);
        Event e = Event.current;
    }
    private void HandleInput()
    {
        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if (input.Contains(commandBase.commandId))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    print("KEKO");
                    (commandList[i] as DebugCommand).Invoke();
                }
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
            // do nothing
        }
        else
        {
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

    public void BingChillingCode()
    {
        StartCoroutine(BingChilling());
    }
    IEnumerator BingChilling()
    {
        bingChilling.SetActive(true);
        yield return new WaitForSeconds(.1f);
        bingChilling.SetActive(false);
    }

    public void AmanCode() {
        StartCoroutine(AmanUllah());
    }
    IEnumerator AmanUllah()
    {
        amanUllah.SetActive(true);
        yield return new WaitForSeconds(.1f);
        amanUllah.SetActive(false);
    }

}
