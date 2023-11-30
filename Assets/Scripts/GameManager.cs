using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool isGamePaused = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0; // This stops the game time
        // Add any other code here to pause the game, like disabling scripts
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1; // This resumes the game time
        // Add any other code here to resume the game, like enabling scripts
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }
}
