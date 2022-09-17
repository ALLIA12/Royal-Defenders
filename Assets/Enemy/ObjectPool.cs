using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] [Range(0, 50)] int poolSize = 10;
    [SerializeField] [Range(0.1f, 10f)] float spawnTimer = 1f;
    GameObject[] pool;
    // Start is called before the first frame update
    private void Awake()
    {
        FillPool();
    }

    void Start()
    {
        StartCoroutine(PopEnemies());
    }
    void FillPool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemy, transform);
            pool[i].SetActive(false);
        }
    }

    void enableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy) 
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator PopEnemies()
    {
        while (true)
        {
            enableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
    public void CallSpawnWaves()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        GameObject UI = GameObject.FindGameObjectWithTag("UI");
        if (UI == null) yield return null ;
        Button button = UI.GetComponentInChildren<Button>();
        button.interactable = false;

        for (int i = 0; i < 5; i++)
        {
            Instantiate(enemy, transform);
            Debug.Log($"item  {i} has been built");
            yield return new WaitForSeconds(1f);
        }
        button.interactable = true;

    }

}
