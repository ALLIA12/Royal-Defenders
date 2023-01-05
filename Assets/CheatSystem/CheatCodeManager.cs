using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodeManager : MonoBehaviour
{
    [SerializeField]
    private bool playerTyping = false;
    [SerializeField]
    private string currentString = "";
    [SerializeField]
    private List<CheatCodeInstance> cheatCodeInstances = new List<CheatCodeInstance>();
    // Update is called once per frame
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

    public void DawiCode()
    {
        print("I AM RACIST AND HORNY FOR SHOUTAS");
    }

    public void GiveGoldCode()
    {
        GameObject bankGO = GameObject.FindGameObjectWithTag("bank");
        Bank bank = bankGO.GetComponent<Bank>();
        bank.depostGold(99999);
    }

}
