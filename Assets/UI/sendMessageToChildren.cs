using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sendMessageToChildren : MonoBehaviour
{

    private void turnOff()
    {
        foreach (Transform child in transform)
        {
            child.SendMessage("TurnOff");
        }
        
    }
    
    private void turnOn()
    {
        foreach (Transform child in transform)
        {
            child.SendMessage("TurnOn");
        }
    }
}
