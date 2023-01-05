using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI[] levels;

    public Animator animator;
    public float transationTime = 1f;

    int[,] scores;
    private void Awake()
    {
        scores = new int[levels.Length, 3];
        // Get scores from the save system later, coooooope
        for (int i = 0; i < scores.GetLength(0); i++)
        {
            for (int j = 0; j < scores.GetLength(1); j++)
            {
                scores[i, j] = PlayerPrefs.GetInt("score" + i + "" + j, 0);
            }
        }
        UpdateTexts(0);
    }

    public void UpdateTexts(int difficulty)
    {
        // Maximum score is 999999999999
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].text = $"Level {i+1}\nBest Score: \n{scores[i, difficulty]}";
        }
    }
    public void StartLevel(int index)
    {
        // temp cope shit 
        if (index > levels.Length+1)
        {
            Debug.Log("COPE");
            return;
        }
        StartCoroutine(StartGameRoutine(index));
    }

    public void PlayGame()
    {
        StartCoroutine(StartGameRoutine(2));
    }
    IEnumerator StartGameRoutine(int index)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transationTime);
        SceneManager.LoadScene(index);
    }

}
