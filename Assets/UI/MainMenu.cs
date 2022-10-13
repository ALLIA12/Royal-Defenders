using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    public float transationTime=1f;
    public void PlayGame() {
        StartCoroutine(StartGameRoutine());
    }
    public void QuitGame()
    {
        Debug.Log("QUIT KEKO YABAI");
        Application.Quit();
    }
    IEnumerator StartGameRoutine()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transationTime);
        SceneManager.LoadScene(1);
    }
}
