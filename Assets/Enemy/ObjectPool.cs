using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
