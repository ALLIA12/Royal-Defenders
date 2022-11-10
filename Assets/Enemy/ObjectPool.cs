using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField][Range(0, 50)] int poolSize = 10;
    [SerializeField][Range(0.1f, 10f)] float spawnTimer = 1f;
    GameObject[] pool;
    // Start is called before the first frame update
    private void Awake()
    {
        FillPool();
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

    private void Update()
    {
        if (transform.childCount == 0)
        {
            gameObject.SetActive(false);
        }
    }

    public IEnumerator SpawnWave()
    {
        int i = 0;
        while (pool.Length != i)
        {
            pool[i].SetActive(true);
            yield return new WaitForSeconds(spawnTimer);
            i++;
        }

    }

}
