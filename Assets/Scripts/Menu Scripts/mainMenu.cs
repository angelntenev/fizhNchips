using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Collections.Generic;



public class MainMenu : MonoBehaviour
{

   
    public TMP_Text nameDisplayText;

    private const string UserNamePrefKey = "UserName";
   
    public GameObject settingsPanel;
    public Toggle isFullScreenToggle;
    public TMP_Dropdown resolutionDropdown;

    public Slider volumeSlider; 

    public AudioMixer audioMixer;
   
    private Resolution[] resolutions;

    public string sceneLobby; 
    public string sceneLevel1; 

    void Start()
    {
        string savedName = PlayerPrefs.GetString(UserNamePrefKey, "");
        nameDisplayText.text = savedName;
        settingsPanel.SetActive(false);
        LoadSettings();
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

    private void HandleFullScreenToggle(bool isFullScreen)
    {
        SetFullScreen(isFullScreen);
    }

    private void SetFullScreen(bool isFullScreen)
    {
        // Just set the full screen mode, don't change the resolution here
        Screen.fullScreen = isFullScreen;
    }

    private void LoadResolutions(int savedResolutionIndex)
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        List<Resolution> desiredResolutions = new List<Resolution>
        {
            new Resolution { width = 1920, height = 1080 },
            new Resolution { width = 2560, height = 1440 },
            new Resolution { width = 3840, height = 2160 }
        };

        foreach (var res in desiredResolutions)
        {
            string option = res.width + " x " + res.height;
            if (!options.Contains(option))
            {
                options.Add(option);
            }
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

        // Apply settings
        ApplyResolution();
        ApplyFullScreen();
        ApplyVolume();

        settingsPanel.SetActive(false);
    }

    private void ApplyResolution()
    {
        Resolution selectedResolution = MapDropdownIndexToResolution(resolutionDropdown.value);
        selectedResolution = ClampResolutionToScreenSize(selectedResolution);
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }

    private void ApplyFullScreen()
    {
        bool isFullScreen = PlayerPrefs.GetInt("IsFullScreen", 0) == 1;
        Screen.fullScreen = isFullScreen;
    }

    private void ApplyVolume()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 0.75f);
        audioMixer.SetFloat("volume", volume);
    }

     private Resolution MapDropdownIndexToResolution(int dropdownIndex)
    {
        List<Resolution> desiredResolutions = new List<Resolution>
        {
            new Resolution { width = 1920, height = 1080 },
            new Resolution { width = 2560, height = 1440 },
            new Resolution { width = 3840, height = 2160 }
        };

        if (dropdownIndex >= 0 && dropdownIndex < desiredResolutions.Count)
        {
            return desiredResolutions[dropdownIndex];
        }
        else
        {
            // Return a default resolution or handle the error appropriately
            return new Resolution { width = 1920, height = 1080 };
        }
    }

    private Resolution ClampResolutionToScreenSize(Resolution resolution)
    {
        Resolution currentScreenResolution = Screen.currentResolution;
        resolution.width = Mathf.Min(resolution.width, currentScreenResolution.width);
        resolution.height = Mathf.Min(resolution.height, currentScreenResolution.height);
        return resolution;
    }

  


    public void LoadSampleScene()
    {
        SceneManager.LoadScene(sceneLevel1);
        LoadSettings();
    }

    public void LoadLobbyScene()
    {
        SceneManager.LoadScene(sceneLobby);
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

    public void onExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting game! !;-( ");
    }


  

  
}
