using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField][Range(0f, 4f)] public float speedModifor = 1f;
    public float slowDownModifor = 1f;
    Enemy enemy;
    List<NodeClass> path = new List<NodeClass>();
    PathFinding pathFinding;
    GridManager gridManager;
    VictoryMenu victoryMenu;
    private void Awake()
    {
        pathFinding = FindObjectOfType<PathFinding>();
        gridManager = FindObjectOfType<GridManager>();
        GameObject temp = GameObject.FindGameObjectWithTag("scoreTracking");
        victoryMenu = temp.GetComponent<VictoryMenu>();
        enemy = GetComponent<Enemy>();
        pathFinding.OnMapChangeTrigger += RecalculatePath;
    }
    private void Update()
    {
        // change slowDownModifor back to normal if it isn't
        slowDownModifor = Mathf.Lerp(slowDownModifor, 1, Time.deltaTime / 8);
    }

    private void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void RecalculatePath(bool ressetPath)
    {
        if (!gameObject.activeInHierarchy) { return; }
        Vector2Int temp = new Vector2Int();
        if (ressetPath)
        {
            temp = pathFinding.getStartCoordinates();
        }
        else
        {
            temp = gridManager.getCoordiantesFromPos(this.transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        if (SettingsMenu.difficulty == 0)
        {
            path = pathFinding.getNewPath(temp);
        }
        else if (SettingsMenu.difficulty == 1)
        {
            path = pathFinding.getUniformPath(temp);
        }
        else
        {
            path = pathFinding.getAStarPath(temp);
        }
        // In the case that no path was found
        if (path.Count == 1 && temp.x != pathFinding.getEndCoordinates().x && temp.y != pathFinding.getEndCoordinates().y)
        {
            ReturnToStart();
            RecalculatePath(true);
        }
        else // if a path was found
        {
            StartCoroutine(FollowPath());
        }
    }

    void ReturnToStart()
    {
        transform.position = gridManager.getPosFromCoordinates(pathFinding.getStartCoordinates());
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.getPosFromCoordinates(path[i].coordinates);
            Tile tile = GameObject.Find(path[i].coordinates.ToString()).GetComponent<Tile>();
            speedModifor = tile.tileSpeed;
            float travelPercent = 0f;
            transform.LookAt(endPos);
            while (travelPercent < 1)
            {
                travelPercent += Time.deltaTime * speedModifor * slowDownModifor;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

    private void FinishPath()
    {
        victoryMenu.numberOfEnemiesNotKilled++;
        enemy.yoinkGoldOnExit();
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        pathFinding.OnMapChangeTrigger -= RecalculatePath;
    }
}
