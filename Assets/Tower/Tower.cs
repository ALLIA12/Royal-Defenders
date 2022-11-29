using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 50;
    [SerializeField] float DelayTimer = 1f;
    [SerializeField] GameObject canvas;
    private Tile tile;

    private void Start()
    {
        StartCoroutine(BuildTower());
    }

    public void CreateTower(out bool gotBuilt, out GameObject towerObject, Tower tower, Vector3 position, Tile tile)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            gotBuilt = false;
            towerObject = null;
            return;
        }
        if (bank.getCurrentGold() >= cost)
        {
            towerObject = Instantiate(tower.gameObject, position, new Quaternion());
            towerObject.GetComponent<Tower>().tile = tile;
            bank.withdrawGold(cost);
            gotBuilt = true;
        }
        else
        {
            gotBuilt = false;
            towerObject = null;
        }
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

    public int getTowerPrice()
    {
        return cost;
    }

    public void ShowCanvas()
    {
        canvas.SetActive(true);
    }
    public void DestroyTower()
    {
        tile.isTaken = false;
        tile.hasTower = false;
        tile.currentTower = null;
        tile.gridManager.unblockNode(tile.coordinates);
        tile.gridManager.changeCostOoNeighbors(tile.coordinates, -3);
        tile.pathFinding.notifiyReciviers();
        Destroy(this.gameObject);
    }

}
