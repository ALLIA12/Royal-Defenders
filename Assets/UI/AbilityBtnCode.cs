using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBtnCode : MonoBehaviour
{
    [SerializeField] private Bank _bank;
    [SerializeField] int abilityPrice;
    [SerializeField] PlayerController _mouse;
    [SerializeField] GameObject _text;
    private int _keyNumber;
    public Color ChosenBtnClr;
    private ColorBlock _colorBlock;
    private ColorBlock originalColor;
    private Button btn;
    public Button[] brnA;
    private int towerSelect = 0;



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
        btn.onClick.AddListener(pickAbility);
    }
    
    private void Update()
    {
        if (_bank.getCurrentGold() < abilityPrice | Input.GetMouseButtonDown(1) | towerSelect>0)
        {
            ColorBlock lockedClr = btn.colors;
            lockedClr.normalColor = Color.black;
            lockedClr.highlightedColor = Color.gray;
            lockedClr.pressedColor = Color.gray;
            lockedClr.selectedColor = Color.gray;
            lockedClr.disabledColor = Color.gray;
            btn.colors = lockedClr;
            btn.colors = originalColor;
            if (_mouse.GetComponent<PlayerController>().getAbilityType() == _keyNumber)
            {
                unpickAbility();
            }
        }
    }

    private void unpickAbility()
    {
        _mouse.SendMessage("abilityPicker", -1);
    }
    
    
    private void pickAbility()
    {

        if (_bank.getCurrentGold() >= abilityPrice)
        {

            _mouse.SendMessage("abilityPicker", _keyNumber);

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
    
    public void TurnOff()
    {
        towerSelect = 1;
    }
    
    public void TurnOn()
    {
        towerSelect = 0;
    }
}
