using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockAbilityCode : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            print("entered");
        }
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Debug.Log("stay");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Debug.Log("exit");
        }
    }
}
