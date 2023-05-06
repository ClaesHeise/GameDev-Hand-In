using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPrefs : MonoBehaviour
{
    [Header("General Setting")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MenuController menuController;

    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Brightness Setting")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;

    [Header("Quality Level Setting")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("Fullscreen Setting")]
    [SerializeField] private Toggle fullScreenToggle;

    [Header("Sensitivity Setting")]
    [SerializeField] private TMP_Text controllerSensTextValue = null;
    [SerializeField] private Slider controllerSensSlider = null;

    [Header("Invert Y Setting")]
    [SerializeField] private Toggle invertYToggle = null;


    // run this method before any other methods in the game
    private void Awake()
    {
        if(canUse)
        {
             if(PlayerPrefs.HasKey("masterVolume"))
             {
                float localVolume = PlayerPrefs.GetFloat("masterVolume");
                volumeTextValue.text = localVolume.ToString("0.0");
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
             }
             else
             {
                menuController.ResetButton("Audio");
             }

             if(PlayerPrefs.HasKey("masterQuality"))
             {
                int localQuality = PlayerPrefs.GetInt("masterQuality");
                qualityDropdown.value = localQuality;
                QualitySettings.SetQualityLevel(localQuality);
             }

             if(PlayerPrefs.HasKey("masterFullscreen"))
             {
                int localFullScreen = PlayerPrefs.GetInt("masterFullscreen");
                if(localFullScreen == 1)
                {
                    Screen.fullScreen = true;
                    fullScreenToggle.isOn = true;
                }
                else
                {
                    Screen.fullScreen = false;
                    fullScreenToggle.isOn = false;
                }
             }

             if(PlayerPrefs.HasKey("masterBrightness"))
             {
                float localBrightness = PlayerPrefs.GetFloat("masterBrightness");
                brightnessTextValue.text = localBrightness.ToString("0.0");
                brightnessSlider.value = localBrightness;
                // Change the brightness here the way we eventually do it
             }

             if(PlayerPrefs.HasKey("masterSens"))
             {
                float localSens = PlayerPrefs.GetFloat("masterSens");
                controllerSensTextValue.text = localSens.ToString("0");
                controllerSensSlider.value = localSens;
                menuController.mainControllerSens = Mathf.RoundToInt(localSens);
             }

             if(PlayerPrefs.HasKey("masterInvertY"))
             {
                if(PlayerPrefs.GetInt("masterInvertY") == 1)
                {
                    invertYToggle.isOn = true;
                }
                else
                {
                    invertYToggle.isOn = false;
                }
             }
        }
    }
}
