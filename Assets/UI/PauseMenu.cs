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

    private void Update()
    {
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
        ResumeGame();
        StartCoroutine(LoadCorutine());
    }

    IEnumerator LoadCorutine()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transationTime);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Game closed COPE");
        Application.Quit();
    }
}
