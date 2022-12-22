using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 50;
    [SerializeField] float DelayTimer = 1f;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject body;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject shooter;
    [SerializeField] GameObject upgradeBody;
    public Bank bank;

    private Tile tile;

    private void Start()
    {
        StartCoroutine(BuildTower());
    }

    public void CreateTower(out bool gotBuilt, out GameObject towerObject, Tower tower, Vector3 position, Tile tile)
    {
        bank = FindObjectOfType<Bank>();
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
        body.SetActive(false);
        weapon.SetActive(false);
        //shooter.SetActive(false);
        body.SetActive(true);
        yield return new WaitForSeconds(DelayTimer);
        weapon.SetActive(true);
        //shooter.SetActive(true);
    }

    public int getTowerPrice()
    {
        return cost;
    }

    public void ShowCanvas(bool active)
    {
        canvas.SetActive(active);
    }
    public void DestroyTower()
    {
        tile.isTaken = false;
        tile.hasTower = false;
        tile.currentTower = null;
        tile.tag = "selectable";
        tile.gridManager.unblockNode(tile.coordinates);
        tile.gridManager.changeCostOoNeighbors(tile.coordinates, -3);
        tile.pathFinding.notifiyReciviers();
        bank.depostGold(cost);
        Destroy(gameObject);
    }

    public void FullyUpgraded()
    {
        body.SetActive(false);
        upgradeBody.SetActive(true);
    }
}
