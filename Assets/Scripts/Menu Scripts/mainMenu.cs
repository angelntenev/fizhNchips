using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public Toggle isFullScreenToggle;
    public Camera gameCamera;

    public float fullscreenFieldOfView = 60f; 
    public float windowedFieldOfView = 50f;


    ScreenSettingsManager screenSettingsManager;

    
    public AudioMixer audioMixer;

    private Resolution[] resolutions;

    public Dropdown resolutionDropdown;

  

    void Start()
    {     

       resolutions =  Screen.resolutions;

       resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
       
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

                if(resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i]. height == Screen.currentResolution.height)
                {
                    resolutionDropdown.value = i;
                    currentResolutionIndex = i;
                }
        }
       
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        if (screenSettingsManager != null)
        {
          
            Screen.fullScreen = screenSettingsManager.getFullScreen();
          
        }

        // Ensure the settings panel is initially hidden
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        // Initialize the toggle state to reflect the current fullscreen mode
        if (isFullScreenToggle != null)
            isFullScreenToggle.isOn = Screen.fullScreen;
        
        // Add listener to the fullscreen toggle
        if (isFullScreenToggle != null)
            isFullScreenToggle.onValueChanged.AddListener(ToggleFullScreen);
    }

    // Update is called once per frame
    void Update()
    {
        // Optional: Implement functionality that requires constant updates
    }



    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        Debug.Log(volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }




    // Start Game
    public void LoadSampleScene()
    {

        screenSettingsManager = GetComponent<ScreenSettingsManager>();
        screenSettingsManager.SetFullScreen(isFullScreenToggle.isOn);
        SceneManager.LoadScene("SampleScene");

    }

    // Toggle the settings panel
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

    // Hide the settings panel
    public void HideSettingsPanel()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void ToggleFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;

        if (gameCamera != null)
        {
            if (isFullScreen)
            {
                // Adjust the camera for fullscreen mode
                gameCamera.fieldOfView = fullscreenFieldOfView;
            }
            else
            {
                // Adjust the camera for windowed mode
                gameCamera.fieldOfView = windowedFieldOfView;
            }
        }
    }








    public void QuitGame()
    {
        Application.Quit();
    }
}
