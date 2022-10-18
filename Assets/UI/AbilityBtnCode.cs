using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBtnCode : MonoBehaviour
{
    [SerializeField] private Bank _bank;
    [SerializeField] PlayerController _mouse;
    private int _keyNumber;
    private KeyCode _keycode;
    private ColorBlock _colorBlock;
    private ColorBlock originalColor;
    private Button btn;


    private void Start()
    {
        _keyNumber = transform.GetSiblingIndex() + 1;
        _keycode = KeyCode.Alpha0 + _keyNumber;
        btn = GetComponent<Button>();
        _colorBlock = btn.colors;
        originalColor = btn.colors;
        btn.onClick.AddListener(pickAbility);
    }
    

    //pick ability using keyboard
    private void Update()
    {
        if (Input.GetKeyDown(_keycode))
        {
            pickAbility();
        }
    }

    private void pickAbility()
    {
        Debug.Log("aaa"+_keyNumber);
        StartCoroutine(AbilityChosen());
    }
    
    IEnumerator AbilityChosen()
    {
        _colorBlock.normalColor = Color.red;
        btn.colors = _colorBlock;
        yield return new WaitForSeconds(.2f);
        btn.colors = originalColor;
    }
}
