using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField]
    private TMP_Text volumeTextValue = null;

    [SerializeField]
    private Slider volumeSlider = null;

    [SerializeField]
    private float defaultVolume = 0.5f;

    [Header("Gameplay Settings")]
    [SerializeField]
    private TMP_Text controllerSensTextValue = null;

    [SerializeField]
    private Slider controllerSensSlider = null;

    [SerializeField]
    private int defaultSens = 5;
    public int mainControllerSens = 5;

    [Header("Toggle Settings")]
    [SerializeField]
    private Toggle invertYToggle = null;

    [Header("Graphics Settings")]
    [SerializeField]
    private Slider brightnessSlider = null;

    [SerializeField]
    private TMP_Text brightnessTextValue = null;

    [SerializeField]
    private float defaultBrightnessValue = 1f;

    [Space(10)]
    [SerializeField]
    private TMP_Dropdown qualityDropdown;

    [SerializeField]
    private Toggle fullScreenToggle;

    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;

    [Header("Confirmation")]
    [SerializeField]
    private GameObject confirmationPrompt = null;

    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;

    [SerializeField]
    private GameObject noSavedGameDialog = null;

    [Header("Resolutions Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        // Get all resolutions
        resolutions = Screen.resolutions;
        // Clear the list
        resolutionDropdown.ClearOptions();
        // Create a list
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            // Check to see if the resolution we found is equal to our screen width and height
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                // Set to our current res
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void NewGameDialogYes()
    {
        Debug.Log("New Game");
        // Delete player positions from playerprefs
        PlayerPrefs.DeleteKey("playerX");
        PlayerPrefs.DeleteKey("playerY");
        PlayerPrefs.DeleteKey("playerZ");

        // Delete ship positions from playerprefs
        PlayerPrefs.DeleteKey("shipX");
        PlayerPrefs.DeleteKey("shipY");
        PlayerPrefs.DeleteKey("shipZ");

        SceneManager.LoadScene(_newGameLevel);
    }

    public void LoadGameDialogYes()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        ShipData shipData = SaveSystem.LoadShip();

        if (playerData == null || shipData == null)
        {
            noSavedGameDialog.SetActive(true);
        }
        else
        {
            // load the game
            Debug.Log("Loading game...!!!");
            // set x, y, z for player in playerprefs
            PlayerPrefs.SetFloat("playerX", playerData.position[0]);
            PlayerPrefs.SetFloat("playerY", playerData.position[1]);
            PlayerPrefs.SetFloat("playerZ", playerData.position[2]);

            // set x, y, z for ship in playerprefs
            PlayerPrefs.SetFloat("shipX", shipData.position[0]);
            PlayerPrefs.SetFloat("shipY", shipData.position[1]);
            PlayerPrefs.SetFloat("shipZ", shipData.position[2]);

            PlayerPrefs.Save();
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        // Show Prompt
        StartCoroutine(ConfirmationBox());
    }

    public void SetControllerSens(float sensitivity)
    {
        mainControllerSens = Mathf.RoundToInt(sensitivity);
        controllerSensTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply()
    {
        if (invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
            // do the inverting here
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
            // ! do the inverting here
        }
        PlayerPrefs.SetFloat("masterSens", mainControllerSens);
        StartCoroutine(ConfirmationBox());
    }

    // setter methods that sets the variables that we can apply later in the GraphicsApply
    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        // Here we can change brightness with post processing or whatever else it might be - not implemented yet

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;

        StartCoroutine(ConfirmationBox());
    }

    public void ResetButton(string MenuType)
    {
        if (MenuType == "Graphics")
        {
            // Reset the brightness (here we have to add the actual functionality, which isn't implemented yet)
            brightnessSlider.value = defaultBrightnessValue;
            brightnessTextValue.text = defaultBrightnessValue.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(
                currentResolution.width,
                currentResolution.height,
                Screen.fullScreen
            );
            resolutionDropdown.value = resolutions.Length; // last resolution in the list
            GraphicsApply();
        }

        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }

        if (MenuType == "Gameplay")
        {
            controllerSensTextValue.text = defaultSens.ToString("0");
            controllerSensSlider.value = defaultSens;
            mainControllerSens = defaultSens;
            invertYToggle.isOn = false;
            GameplayApply();
        }
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }
}
