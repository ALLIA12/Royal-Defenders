using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class TowerBtnCode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Bank _bank;
    [SerializeField] Tower _tower;
    [SerializeField] PlayerController _mouse;
    [SerializeField] GameObject _text;
    [SerializeField] GameObject goldUI;
    private int _keyNumber;
    public Color ChosenBtnClr;
    private ColorBlock _colorBlock;
    private ColorBlock originalColor;
    private Button btn;
    public Button[] brnA;
    private int abilitySelect = 0;

    private void Start()
    {
        _keyNumber = transform.GetSiblingIndex() + 1;
        _text.SetActive(false);
        btn = GetComponent<Button>();
        _colorBlock = btn.colors;
        originalColor = btn.colors;
        int ChildNum = btn.transform.parent.childCount;
        brnA = new Button[ChildNum];
        for (int i = 0; i < ChildNum; i++)
        {
            brnA[i] = btn.transform.parent.transform.GetChild(i).GetComponent<Button>();
        }
        btn.onClick.AddListener(pickTower);
    }

    private void Update()
    {
        if (_bank.getCurrentGold() < _tower.getTowerPrice() | Input.GetMouseButtonDown(1) | abilitySelect>0)
        {
            ColorBlock lockedClr = btn.colors;
            lockedClr.normalColor = Color.black;
            lockedClr.highlightedColor = Color.gray;
            lockedClr.pressedColor = Color.gray;
            lockedClr.selectedColor = Color.gray;
            lockedClr.disabledColor = Color.gray;           
            btn.colors = lockedClr;
            btn.colors = originalColor;
            
            if (_mouse.GetComponent<PlayerController>().gettowerType() == _keyNumber)
            {
                unpickTower();
            }
        }
    }

    private void unpickTower()
    {
        _mouse.SendMessage("TowerPicker", -1);
    }

    //if tower is selected change the button color
    //if you have no money for the tower and you select a tower a message will be shown
    private void pickTower()
    {

        if (_bank.getCurrentGold() >= _tower.getTowerPrice())
        {

            _mouse.SendMessage("TowerPicker", _keyNumber);

            for (int i = 0; i < brnA.Length; i++)
            {
                if (i == _keyNumber - 1)
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
                    brnA[i].colors = originalColor;
                }
            }
        }
        else
        {
            btn.colors = originalColor;
            StartCoroutine(ShowText());
        }

        IEnumerator ShowText()
        {
            _text.SetActive(true);
            yield return new WaitForSeconds(3f);
            _text.SetActive(false);
        }

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (goldUI == null) { return; }
        goldUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (goldUI == null)
        {
            return;
        }

        goldUI.SetActive(false);
    }

    public void TurnOff()
    {
        abilitySelect = 1;
    }
    
    public void TurnOn()
    {
        abilitySelect = 0;
    }
}
