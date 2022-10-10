using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown graphicsDropDown;
    public Resolution[] resolutions;
    public TMP_Dropdown resolutionDropDown;

    private void Start()
    {
        ChangeGraphisUI();
        AddOptions();
    }

    private void ChangeGraphisUI()
    {
        graphicsDropDown.value = QualitySettings.GetQualityLevel();
        graphicsDropDown.RefreshShownValue();
    }

    private void AddOptions()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        // temp value for now
        int temp = -1;
        for (int i = 0; i < resolutions.Length; i++)
        {
            Resolution item = resolutions[i];
            string option = item.width + " x " + item.height;
            options.Add(option);
            if (item.width == Screen.currentResolution.width &&
                item.height == Screen.currentResolution.height)
            {
                temp = i; 
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = temp;
        resolutionDropDown.RefreshShownValue();
    }

    public void SetVolume(float decimalVolume) {
        var dbVolume = Mathf.Log10(decimalVolume) * 20;
        if (decimalVolume == 0.0f)
        {
            dbVolume = -80.0f;
        }
        audioMixer.SetFloat("Volume", dbVolume);
    }

    public void SetGraphics(int quailtyIndex) {
        // This might need work later on.
        QualitySettings.SetQualityLevel(quailtyIndex);
    }

    public void SetFullScreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen; 
    }

    public void SetResolution(int resolutionIndex) {
        Resolution temp= resolutions[resolutionIndex];
        Screen.SetResolution(temp.width,temp.height, Screen.fullScreen);
    }
}
