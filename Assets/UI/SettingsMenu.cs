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
    public static int difficulty = 0;
    public Slider mSlider;
    public Slider seSlider;
    private void Start()
    {
        ChangeGraphisUI();
        AddOptions();
        mSlider.value = PlayerPrefs.GetFloat("musicVolumeSlider", 1);
        seSlider.value = PlayerPrefs.GetFloat("seVolumeSlider", 1);
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

    public void SetMusic(float decimalVolume)
    {
        var dbVolume = Mathf.Log10(decimalVolume) * 20;
        if (decimalVolume == 0.0f)
        {
            dbVolume = -80.0f;
        }
        AudioManager.instance.musicMixerGroup.audioMixer.SetFloat("Music Volume", dbVolume);
        PlayerPrefs.SetFloat("musicVolume", dbVolume);
        PlayerPrefs.SetFloat("musicVolumeSlider", decimalVolume);
    }

    public void SetSE(float decimalVolume)
    {
        var dbVolume = Mathf.Log10(decimalVolume) * 20;
        if (decimalVolume == 0.0f)
        {
            dbVolume = -80.0f;
        }
        AudioManager.instance.soundEffectsMixerGroup.audioMixer.SetFloat("Sound Effects Volume", dbVolume);
        PlayerPrefs.SetFloat("seVolume", dbVolume);
        PlayerPrefs.SetFloat("seVolumeSlider", decimalVolume);
    }

    public void SetGraphics(int quailtyIndex)
    {
        QualitySettings.SetQualityLevel(quailtyIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution temp = resolutions[resolutionIndex];
        Screen.SetResolution(temp.width, temp.height, Screen.fullScreen);
    }

    public static void SetDifficulty(int difficultySettings)
    {
        difficulty = difficultySettings;
    }


}
