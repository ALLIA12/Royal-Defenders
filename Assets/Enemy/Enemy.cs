using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int worth = 20;
    [SerializeField] int yoink = 20;
    Bank bank;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }
    public void giveGoldOnDeath()
    {
        if (bank == null) { return; }
        bank.depostGold(worth);
    }
    public void yoinkGoldOnExit()
    {
        if (bank == null) { return; }
        bank.withdrawGold(yoink);
    }
}
