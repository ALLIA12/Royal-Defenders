using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtnCode : MonoBehaviour
{
    [SerializeField] Bank _bank;
    [SerializeField] Tower _tower;
    [SerializeField] mousePosition3D _mouse;
    [SerializeField] GameObject _text;
    private int _keyNumber;
    public Color ChosenBtnClr;
    private ColorBlock _colorBlock;
    private ColorBlock originalColor;
    private Button btn;


    private void Start()
    {
        _keyNumber = transform.GetSiblingIndex() + 1;
        _text.SetActive(false);
        btn = GetComponent<Button>();
        _colorBlock = btn.colors;
        originalColor = btn.colors;
        btn.onClick.AddListener(pickTower);
    }
    
    private void pickTower()
    {
        
        if (_bank.getCurrentGold()>=_tower.getTowerPrice(_keyNumber))
        {
            
            _mouse.SendMessage("TowerPicker", _keyNumber);
            
            int ChildNum = btn.transform.parent.childCount;
            for (int i = 0; i < ChildNum; i++)
            {
                if (i==_keyNumber-1)
                {
                    _colorBlock.normalColor = ChosenBtnClr;
                    _colorBlock.highlightedColor = ChosenBtnClr;
                    _colorBlock.pressedColor = ChosenBtnClr;
                    _colorBlock.selectedColor = ChosenBtnClr;
                    _colorBlock.disabledColor = ChosenBtnClr;
                    btn.colors = _colorBlock;
                }
                else
                {
                    int towerNum = i + 1;
                    String childName = "Tower button (" + towerNum + ")";
                    btn.transform.parent.Find(childName).GetComponent<TowerBtnCode>().btn.colors =
                        originalColor;
                }
            }
        }
        else
        {
            
            StartCoroutine(ShowText());
        }

        IEnumerator ShowText()
        {
            _text.SetActive(true);
            yield return new WaitForSeconds(3f);
            _text.SetActive(false);
        }
        
    }
    
}
