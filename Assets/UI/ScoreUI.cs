using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI level1;
    public TextMeshProUGUI level2;
    public TextMeshProUGUI level3;
    public TextMeshProUGUI level4;

    public Animator animator;
    public float transationTime = 1f;

    int[,] scores = new int[4, 3];
    private void Awake()
    {
        // Get scores from the save system later, coooooope
        for (int i = 0; i < scores.GetLength(0); i++)
        {
            for (int j = 0; j < scores.GetLength(1); j++)
            {
                scores[i, j] = i + j;
            }
        }
        UpdateTexts(0);
    }

    public void UpdateTexts(int difficulty)
    {
        // Maximum score is 999999999999
        level1.text = $"Level 1\nBEST SCORE: \n{scores[0, difficulty]}";
        level2.text = $"Level 2\nBEST SCORE: \n{scores[1, difficulty]}";
        level3.text = $"Level 3\nBEST SCORE: \n{scores[2, difficulty]}";
        level4.text = $"Level 4\nBEST SCORE: \n{scores[3, difficulty]}";
    }
    public void StartLevel(int index)
    {
        // temp cope shit 
        if (index > 1)
        {
            Debug.Log("COPE");
            return;
        }
        StartCoroutine(StartGameRoutine(index));
    }

    public void PlayGame()
    {
        StartCoroutine(StartGameRoutine(1));
    }
    IEnumerator StartGameRoutine(int index)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transationTime);
        SceneManager.LoadScene(index);
    }

}
