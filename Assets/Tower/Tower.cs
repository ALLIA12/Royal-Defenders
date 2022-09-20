using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 50;
    [SerializeField] float DelayTimer = 1f;
    private int TowerPrice;

    private void Start()
    {
        StartCoroutine(BuildTower());
    }
    
    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null) { return false; }
        if (bank.getCurrentGold() >= cost)
        {
            Instantiate(tower.gameObject, position, new Quaternion());
            bank.withdrawGold(cost);
            return true;
        }
        else { return false; }
    }
    
    IEnumerator BuildTower()
    {
        GameObject temp = this.transform.GetChild(0).gameObject;
        GameObject temp2 = this.transform.GetChild(1).gameObject;
        temp.SetActive(false);
        temp2.SetActive(false);
        temp.SetActive(true);
        yield return new WaitForSeconds(DelayTimer);
        temp2.SetActive(true);
    }

    public int getTowerPrice(int towerNO)
    {
        return cost;
    }
}
