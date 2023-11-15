using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel; // Reference to the settings panel
    public Toggle isFullScreenToggle; // Reference to the fullscreen toggle UI component
    public Camera gameCamera;

    public float fullscreenFieldOfView = 60f; // Example value, adjust as needed
    public float windowedFieldOfView = 50f; // Example value, adjust as needed



    void Start()
    {
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

    // Start Game
    public void LoadSampleScene()
    {
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
