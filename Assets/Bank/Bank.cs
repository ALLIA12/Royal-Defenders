using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingGold = 100;
    [SerializeField] int currentGold;
    [SerializeField] TextMeshProUGUI displayGold;
    [SerializeField] RetryMenu retryMenu;
    private void Awake()
    {
        currentGold = startingGold;
        updateGoldDisplay();
    }

    public int getCurrentGold()
    {
        return currentGold;
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
        if (currentGold < 0)
        {
            retryMenu.ShowMenu();
        }
    }
    //void reLoadScene()
    //{
    //    Scene scene = SceneManager.GetActiveScene();
    //    SceneManager.LoadScene(scene.buildIndex);
    //}

    void updateGoldDisplay()
    {
        displayGold.text = $"Gold: {currentGold}";
    }



}
