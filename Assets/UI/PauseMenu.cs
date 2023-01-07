using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public Animator animator;
    public float transationTime = 1f;
    [SerializeField] public GameObject pauseMenuUI = null;
    [SerializeField] public GameObject retryMenuUI = null;
    [SerializeField] public GameObject victoryMenuUI = null;

    private void Update()
    {
        if (retryMenuUI.activeSelf) { return; }
        if (victoryMenuUI.activeSelf) { return; }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (gameIsPaused)
            {
                // Resume
                ResumeGame();
            }
            else
            {
                //Pause
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = SpeedChanger.currentSpeed;
        gameIsPaused = false;
    }

    public void LoadMenu()
    {
        pauseMenuUI.SetActive(false);
        SpeedChanger.currentSpeed = 1;
        Time.timeScale = SpeedChanger.currentSpeed;
        gameIsPaused = false;
        StartCoroutine(LoadCorutine());
    }

    IEnumerator LoadCorutine()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transationTime);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Game closed COPE");
        Application.Quit();
    }
}
