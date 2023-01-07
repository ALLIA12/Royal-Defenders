using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PriceOftower : MonoBehaviour
{
    [SerializeField] Tower _tower;
    TextMeshProUGUI displayDetails;

    private void Start()
    {
        displayDetails = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        displayDetails.text = _tower.getTowerPrice().ToString();
    }
}
