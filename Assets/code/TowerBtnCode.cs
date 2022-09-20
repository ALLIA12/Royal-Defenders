using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtnCode : MonoBehaviour
{
    [SerializeField] private Bank _bank;
    [SerializeField] Tower _tower;
    [SerializeField] GameObject _text;
    private Button yourButton;
    private int _keyNumber;


    private void Start()
    {
        _text.SetActive(false);
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(pickTower);
    }

    private void OnValidate()
    {
        _keyNumber = transform.GetSiblingIndex() + 1;
    }
    

    private void pickTower()
    {
        if (_bank.getCurrentGold()>=_tower.getTowerPrice(_keyNumber))
        {
            Debug.Log(_keyNumber);
        }
        else
        {
            
            StartCoroutine(ShowText());
        }

        IEnumerator ShowText()
        {
            _text.SetActive(true);
            yield return new WaitForSeconds(.3f);
            _text.SetActive(false);
        }
        
    }

    
    
}
