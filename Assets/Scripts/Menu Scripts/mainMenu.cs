using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Collections.Generic;


public class MainMenu : MonoBehaviour
{

    public TMP_InputField nameInputField;
    public TMP_Text nameDisplayText;

    private const string UserNamePrefKey = "UserName";

    public GameObject menuOptions;

    public GameObject userNameRegister;
    public GameObject settingsPanel;
    public Toggle isFullScreenToggle;
    public TMP_Dropdown resolutionDropdown;

    public Slider volumeSlider; 

    public AudioMixer audioMixer;
   
    private Resolution[] resolutions;



    void Start()
    {
        // Load the saved name and display it
        string savedName = PlayerPrefs.GetString(UserNamePrefKey, "");
        nameDisplayText.text = savedName;

        if (string.IsNullOrEmpty(savedName))
        {
            // No name saved, show only the name input
            menuOptions.SetActive(false);
            userNameRegister.SetActive(true);
        }
        else
        {
            // Name already saved, show menu options
            userNameRegister.SetActive(false);
            menuOptions.SetActive(true);
            nameDisplayText.text = savedName;

            LoadSettings();
            isFullScreenToggle.onValueChanged.AddListener(HandleFullScreenToggle);
        }

        settingsPanel.SetActive(false);

        LoadSettings();

        isFullScreenToggle.onValueChanged.AddListener(HandleFullScreenToggle);
    }

    public void SaveName()
    {
        string userName = nameInputField.text;
        PlayerPrefs.SetString(UserNamePrefKey, userName);
        PlayerPrefs.Save();

                if (!string.IsNullOrEmpty(userName))
                {
                    PlayerPrefs.SetString(UserNamePrefKey, userName);
                    PlayerPrefs.Save();

                    nameDisplayText.text = userName;

                    // Hide name input and show menu options
                    userNameRegister.SetActive(false);
                    menuOptions.SetActive(true);

                    LoadSettings();
                    isFullScreenToggle.onValueChanged.AddListener(HandleFullScreenToggle);
                }
       
        nameDisplayText.text = userName;
    }

 

   
    private void HandleFullScreenToggle(bool isFullScreen)
    {
        SetFullScreen(isFullScreen);

        if (isFullScreen)
        {
            SetFullScreen(true);
            SetBestResolutionForFullScreen();
        }
        else
        {
            SetFullScreen(false);
            SetResolutionToWindowed();
        }
    }

 

     private void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        settingsPanel.SetActive(false);
    }


    private void SetBestResolutionForFullScreen()
    {
        Resolution bestResolution = GetBestResolution();
        Screen.SetResolution(bestResolution.width, bestResolution.height, true);
    }

    private Resolution GetBestResolution()
    {
        Resolution bestResolution = resolutions[resolutions.Length - 1]; 
        return bestResolution;
    }

     private void SetResolutionToWindowed()
    {
        Resolution bestResolution = GetBestResolution();
        Screen.SetResolution(bestResolution.width, bestResolution.height, false);
    }

    private void LoadSettings()
    {
        // Load saved settings from PlayerPrefs
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);

        bool isFullScreenSaved = PlayerPrefs.GetInt("IsFullScreen", 0) == 1;

        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.75f);
        volumeSlider.value = savedVolume; // Set the volume slider's value

        LoadResolutions(savedResolutionIndex);
        isFullScreenToggle.isOn = isFullScreenSaved;
  
    }

    private void LoadResolutions(int savedResolutionIndex)
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = savedResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void OnApplyButtonClicked()
    {
        // Update PlayerPrefs
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
        PlayerPrefs.SetInt("IsFullScreen", isFullScreenToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value); // Save the volume slider's value
        PlayerPrefs.Save();

        // Apply settings immediately
    
        // Apply resolution
        int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        // Apply fullscreen
        bool isFullScreen = PlayerPrefs.GetInt("IsFullScreen", 0) == 1;
        Screen.fullScreen = isFullScreen;

        // Apply volume
        float volume = PlayerPrefs.GetFloat("Volume", 0.75f);
        audioMixer.SetFloat("volume", volume);
  

        settingsPanel.SetActive(false);
    }

    public void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
        LoadSettings();
    }

    public void ToggleSettingsPanel()
    {
        if (settingsPanel != null)
        {
            bool isActive = settingsPanel.activeSelf;
            settingsPanel.SetActive(!isActive);
        }
        else
        {
            Debug.LogError("Settings panel not set in the inspector");
        }
    }

    public void HideSettingsPanel()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

  
}
