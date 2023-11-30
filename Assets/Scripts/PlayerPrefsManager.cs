using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;





public class PlayerPrefsManager : MonoBehaviour
{

public TMP_InputField nameInputField;
public TMP_Text nameDisplayText;

public GameObject menuOptions;
public GameObject settingsPanel;
public GameObject userNameRegister;

private const string UserNamePrefKey = "UserName";


    public string sceneLobby; 
    public string sceneLevel1; 


    // Start is called before the first frame update
    void Start()
    {
        // Load the saved name and display it
        string savedName = PlayerPrefs.GetString(UserNamePrefKey, "");
        nameDisplayText.text = savedName;

            if (string.IsNullOrEmpty(savedName))
            {
                // No name saved, show only the name input
                userNameRegister.SetActive(true);
                menuOptions.SetActive(false);
            }
            else
            {
            userNameRegister.SetActive(false);
            menuOptions.SetActive(true);
            nameDisplayText.text = savedName;
            }
        settingsPanel.SetActive(false);
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
                }
       
        nameDisplayText.text = userName;
    }

    public void changeToLevel1()
    {
        SceneManager.LoadScene(sceneLevel1);
       
    }

    public void changeToLobby()
    {
        SceneManager.LoadScene(sceneLobby);
     
    }




}
