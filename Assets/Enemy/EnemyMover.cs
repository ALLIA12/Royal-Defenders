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
    private void Awake()
    {
        pathFinding = FindObjectOfType<PathFinding>();
        gridManager = FindObjectOfType<GridManager>();
        enemy = GetComponent<Enemy>();

    }
    private void Update()
    {
        // change slowDownModifor back to normal if it isn't
        slowDownModifor = Mathf.Lerp(slowDownModifor, 1, Time.deltaTime/8);
    }
    private void OnEnable()
    {
        retunrToStart();
        RecalculatePath(true);
    }

    void RecalculatePath(bool ressetPath)
    {
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
        StartCoroutine(FollowPath());
    }

    void retunrToStart()
    {
        transform.position = gridManager.getPosFromCoordinates(pathFinding.getStartCoordinates());
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.getPosFromCoordinates(path[i].coordinates);
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
        enemy.yoinkGoldOnExit();
        // Destroy in case it is spawned with via wave, add check later
        Destroy(this.gameObject);
    }
}
