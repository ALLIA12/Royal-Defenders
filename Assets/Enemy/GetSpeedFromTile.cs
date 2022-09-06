using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSpeedFromTile : MonoBehaviour
{
    [SerializeField]
    public GameObject temp = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit objectHit;
        // Shoot raycast
        Debug.DrawRay(transform.position+new Vector3(0,1,0), transform.up - new Vector3(0, 5, 0), Color.green);
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.up - new Vector3(0, 5, 0), out objectHit, 50))
        {
            //Debug.Log( objectHit.);
            var gameObject = objectHit.collider.gameObject;
            if (gameObject == null) return;
            Tile tile = gameObject.GetComponent<Tile>();
            EnemyMover temp = GetComponent<EnemyMover>();
            temp.speedModifor = tile.tileSpeed;
        }
    }
}
