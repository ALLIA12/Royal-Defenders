using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class VictoryMenu : MonoBehaviour
{
    public Animator animator;
    public float transationTime = 1f;
    public int numberOfEnemiesDestroyed = 0;
    public int numberOfTowersBuilt = 0;
    public int numberOfTowerUpgrades = 0;
    public int numberOfEnemiesNotKilled = 0;

    [SerializeField] TextMeshProUGUI outputText;
    [SerializeField] public GameObject victoryMenuUI = null;
    [SerializeField] public Bank bank = null;
    public int score = 0;

    public void ShowMenu()
    {
        score += numberOfEnemiesDestroyed * 20;
        score -= numberOfTowersBuilt * 2;
        score -= numberOfTowerUpgrades * 1;
        score -= numberOfEnemiesNotKilled * 30;
        score += (int)(bank.getCurrentGold() * .1f);
        int diff = SettingsMenu.difficulty;
        int i = SceneManager.GetActiveScene().buildIndex;
        int oldScore = PlayerPrefs.GetInt("score" + (i - 1) + "" + diff, 0);
        if (score > oldScore)
        {
            PlayerPrefs.SetInt("score" + (i - 1) + "" + diff, score);
            outputText.text = $"Score: {score}\nNew High Score!!!";
        }
        else
        {
            outputText.text = "Score: " + score.ToString();
        }
        Debug.Log(score);
        StartCoroutine(WaitingForAFewSeconds());

    }
    IEnumerator WaitingForAFewSeconds()
    {
        yield return new WaitForSeconds(2);
        victoryMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.gameIsPaused = true;
    }

    public void ReloadGame()
    {
        //retryMenuUI.SetActive(false);
        SpeedChanger.currentSpeed = 1;
        Time.timeScale = SpeedChanger.currentSpeed;
        //gameIsPaused = false;
        PauseMenu.gameIsPaused = false;
    }

    public void LoadMenu()
    {
        ReloadGame();
        StartCoroutine(LoadCorutine());
    }
    public void ReloadLevel()
    {
        ReloadGame();
        StartCoroutine(ReloadCorutine());
    }

    IEnumerator LoadCorutine()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transationTime);
        SceneManager.LoadScene(1);
    }

    IEnumerator ReloadCorutine()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transationTime);
        int i = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(i);
    }

    public void QuitGame()
    {
        Debug.Log("Game closed COPE");
        Application.Quit();
    }
}
