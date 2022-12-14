using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedChanger : MonoBehaviour
{
    public static int currentSpeed = 1;

    public void ChangeSpeed()
    {
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();

        if (currentSpeed == 1)
        {
            currentSpeed = 2;
            Time.timeScale = currentSpeed;
        }
        else
        {
            currentSpeed = 1;
            Time.timeScale = currentSpeed;
        }
        text.text = currentSpeed.ToString() + "X";
    }
}
