using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBtnCode : MonoBehaviour
{
    [SerializeField] private Bank _bank;
    private int _keyNumber;
    private KeyCode _keycode;


    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(pickAbility);
    }

    private void OnValidate()
    {
        _keyNumber = transform.GetSiblingIndex() + 1;
        _keycode = KeyCode.Alpha0 + _keyNumber;
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
    }
}
