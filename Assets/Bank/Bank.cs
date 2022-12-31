using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingGold = 100;
    [SerializeField] int startingHealth = 100;
    [SerializeField] int currentGold;
    [SerializeField] int currentHealth;
    [SerializeField] TextMeshProUGUI displayGold;
    [SerializeField] TextMeshProUGUI displayHealth;
    [SerializeField] RetryMenu retryMenu;
    private void Awake()
    {
        currentGold = startingGold;
        currentHealth = startingHealth;
        updateGoldDisplay();
    }

    public int getCurrentGold()
    {
        return currentGold;
    }
    public int getCurrentHealth()
    {
        return currentHealth;
    }
    public void depostGold(int gold)
    {
        currentGold += Mathf.Abs(gold);
        updateGoldDisplay();
    }
    public void withdrawGold(int gold)
    {
        currentGold -= Mathf.Abs(gold);
        updateGoldDisplay();
    }
    public void withdrawHealth(int health)
    {
        currentHealth -= Mathf.Abs(health);
        updateHealthDisplay();
        if (currentHealth <= 0)
        {
            retryMenu.ShowMenu();
        }
    }

    void updateGoldDisplay()
    {
        displayGold.text = $"Gold: {currentGold}";
    }

    void updateHealthDisplay()
    {
        displayHealth.text = $"Health: {currentHealth}";
    }



}
