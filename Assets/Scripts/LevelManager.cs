using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public string sceneLobby; 
    public string sceneLevel1; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
