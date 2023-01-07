using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColorNew = Color.white;
    [SerializeField] Color blockedColorNew = Color.black;
    [SerializeField] Color exploredColorNew = Color.yellow;
    [SerializeField] Color pathColorNew = Color.red;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;


    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();
        gridManager = FindObjectOfType<GridManager>();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }
        SetLabelColour();
        ToogleLable();
    }
    void ToogleLable()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.enabled;
        }
    }
    void SetLabelColour()
    {
        if (gridManager == null) { return; }
        NodeClass temp = gridManager.getGridNode(coordinates);
        if (temp == null) { return; }
        if (!temp.isWalkable)
        {
            label.color = blockedColorNew;
        }
        else if (temp.isPath)
        {
            label.color = pathColorNew;
        }
        else if (temp.isExplored)
        {
            label.color = exploredColorNew;
        }
        else
        {
            label.color = defaultColorNew;
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / 12);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / 12);
        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
