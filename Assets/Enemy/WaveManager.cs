using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{

    int currWave = 0;
    private void Update()
    {
        if (ChildCountActive(transform) == 0)
        {
            Debug.Log("YOU WON");
        }
    }
    public int ChildCountActive(Transform t)
    {
        int k = 0;
        foreach (Transform c in t)
        {
            if (c.gameObject.activeSelf)
                k++;
        }
        return k;
    }

    public void StartWaveButton()
    {
        StartCoroutine(SpawnWave());
    }


    private IEnumerator SpawnWave()
    {
        int numberOfWaves = transform.childCount;
        GameObject UI = GameObject.FindGameObjectWithTag("UI");
        if (UI == null) yield return null;
        Button button = UI.GetComponentInChildren<Button>();
        button.interactable = false;
        Transform waveN = this.gameObject.transform.GetChild(currWave);
        ObjectPool objectPool = waveN.gameObject.GetComponent<ObjectPool>();
        yield return StartCoroutine(objectPool.SpawnWave());
        currWave++;
        if (currWave != numberOfWaves) button.interactable = true;
    }
}
