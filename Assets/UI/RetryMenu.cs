using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;

public class RetryMenu : MonoBehaviour
{
    public Animator animator;
    public float transationTime = 1f;
    [SerializeField] public GameObject retryMenuUI = null;


    public void ShowMenu()
    {
        retryMenuUI.SetActive(true);
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
        SceneManager.LoadScene(0);
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
