using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSettingsManager : MonoBehaviour
{

    public bool isFullScreen;

    public static ScreenSettingsManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetFullScreen(bool fullscreen)
    {
        isFullScreen = fullscreen;
    }

    public bool getFullScreen(){
        return isFullScreen;
    }


}
