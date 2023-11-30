using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementManager : MonoBehaviour
{
    public GameObject prefabToClone; 
    public GameObject carnivoreToClone; 
    private List<GameObject> clonedPrefabs = new List<GameObject>();
    private List<GameObject> clonedCarnivoresPrefabs = new List<GameObject>();
    public MoneyManager moneyManager;
    private Dictionary<GameObject, float> greenTimeTracker = new Dictionary<GameObject, float>();
    public string sceneLobby; 
    private HungerActivity hungerActiviyClownfish;

    public GameObject SettingsPanel;

   public Canvas mainCanvas; 
    public Canvas settingsCanvas; 

   

    void Start()
    { 
        //hungerActiviyClownfish = GetComponent<HungerActivity>();
        LoadPositions();
        LoadCarnivorePositions();
        moneyManager = GameObject.Find("GameManager").GetComponent<MoneyManager>();
    }

    void Update()
    {
        for (int i = clonedPrefabs.Count - 1; i >= 0; i--)
        {
            if (prefabToClone == null)
            {
                GameObject prefab = GetPrefabReference();
                if (prefab != null) {
                    Instantiate(prefab);
                } else {
                    Debug.LogError("Failed to load prefab from Resources.");
                }
            }


            //Deletes dead fish
            if (clonedPrefabs[i] == null)
            {
                clonedPrefabs.RemoveAt(i);
                 
            }

        }

        for (int i = clonedCarnivoresPrefabs.Count - 1; i >= 0; i--)
        {
            if (carnivoreToClone == null)
            {
                GameObject Carnivoreprefab = GetCarnivorePrefabReference();
                if (Carnivoreprefab != null) {
                    Instantiate(Carnivoreprefab);
                } else {
                    Debug.LogError("Failed to load prefab from Resources.");
                }
            }
        }        
    }

 

    GameObject GetPrefabReference() 
    {
    return Resources.Load<GameObject>("Assets/Objects/Fish/Clownfish.prefab");
    }

    public void ClonePrefab()
    {
        bool didSpend = moneyManager.SpendMoney(100);
        if (didSpend)
        {
            GameObject clonedPrefab = Instantiate(prefabToClone);
            clonedPrefabs.Add(clonedPrefab);
            Debug.Log("Total cloned ClownFish: " + clonedPrefabs.Count);
        } 
        else
        {
            Debug.Log("No money");
        }
    }


    GameObject GetCarnivorePrefabReference() 
    {
        return Resources.Load<GameObject>("Assets/Objects/Fish/Carnivore.prefab");
    }

    public void CloneCarnivorePrefab()
    {
        bool didSpend = moneyManager.SpendMoney(100);
        if (didSpend)
        {
                GameObject clonedCarnivore = Instantiate(carnivoreToClone);
                clonedCarnivoresPrefabs.Add(clonedCarnivore);
                Debug.Log("Total cloned Carnivores: " + clonedCarnivoresPrefabs.Count);
    
        }else
        {
            Debug.Log("No money");
        }
    }


    public void SavingPositioning()
    {
        SavePositions();
        SaveCarnivorePositions();
        Debug.Log("Positions SAVED!");
    }

    public void LoadingPositioning()
    {
        LoadPositions();
        LoadCarnivorePositions();
        Debug.Log("Positions LOADED!");
    }

    //Save ClownFish
    private void SavePositions()
    {
        for (int i = 0; i < clonedPrefabs.Count; i++)
        {
            
            PlayerPrefs.SetString("PrefabPosition_" + i, SerializeVector(clonedPrefabs[i].transform.position));
            Debug.Log("Saved positions for Clown Fish: " + i + ", at: " + clonedPrefabs[i].transform.position);
        }
        PlayerPrefs.SetInt("ClonedPrefabCount", clonedPrefabs.Count);
        Debug.Log("Saved positions for " + clonedPrefabs.Count + " prefabs." );
    }

    //Load ClownFish
    private void LoadPositions()
    {

        int count = PlayerPrefs.GetInt("ClonedPrefabCount", 0);
        for (int i = 0; i < count; i++)
        {
            GameObject loadedPrefab = Instantiate(prefabToClone);
            Vector3 position = DeserializeVector(PlayerPrefs.GetString("PrefabPosition_" + i));
            loadedPrefab.transform.position = position;
            clonedPrefabs.Add(loadedPrefab);
            Debug.Log("Loaded positions for Prefab number " + i + ", at: " + loadedPrefab.transform.position ) ;
        }
        Debug.Log("Loaded positions for " + count + " prefabs.");
    }

    //CARNIVORE 
    public void SavingCarnivorePositioning()
    {
        SaveCarnivorePositions();
        Debug.Log("Carnivore Positions SAVED!");
    }
    public void LoadingCarnivorePositioning()
    {
        LoadCarnivorePositions();
        Debug.Log("Carnivore Positions LOADED!");
    }
    //Save Carnivore
    private void SaveCarnivorePositions()
    {
        for (int i = 0; i < clonedCarnivoresPrefabs.Count; i++)
        {
            PlayerPrefs.SetString("CarnivorePrefabPosition_" + i, SerializeVector(clonedCarnivoresPrefabs[i].transform.position));
            Debug.Log("Saved positions for Carnivore: " + i + ", at: " + clonedCarnivoresPrefabs[i].transform.position);
        }
        PlayerPrefs.SetInt("ClonedCarnivoreCount", clonedCarnivoresPrefabs.Count);
        Debug.Log("Saved positions for " + clonedCarnivoresPrefabs.Count + " Carnivore." );
    }
    //Load Carnivore 
    private void LoadCarnivorePositions()
    {

        int count = PlayerPrefs.GetInt("ClonedCarnivoreCount", 0);
        for (int i = 0; i < count; i++)
        {
            GameObject loadedCarnivorePrefab = Instantiate(carnivoreToClone);
            Vector3 position = DeserializeVector(PlayerPrefs.GetString("CarnivorePrefabPosition_" + i));
            loadedCarnivorePrefab.transform.position = position;
            clonedCarnivoresPrefabs.Add(loadedCarnivorePrefab);
            Debug.Log("Loaded positions for Carnivore number " + i + ", at: " + loadedCarnivorePrefab.transform.position ) ;
        }
        Debug.Log("Loaded positions for " + count + " Carnivores.");
    }

    private string SerializeVector(Vector3 vector)
    {
        return vector.x + "," + vector.y + "," + vector.z;
    }

    private Vector3 DeserializeVector(string data)
    {
        string[] values = data.Split(',');
        if (values.Length == 3)
        {
            return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        }
        return Vector3.zero;
    }


    public void changeToLobby()
    {
        SaveCarnivorePositions();
        SavePositions();
        return;
    }


    public void onMenuClicked()
    {
        // Pause the game
        Time.timeScale = 0;

        settingsCanvas.sortingOrder = mainCanvas.sortingOrder + 1;
        // Activate the Settings Panel
        if (SettingsPanel != null)
        {
            SettingsPanel.SetActive(true);
            SettingsPanel.transform.SetAsLastSibling();
        }


        // Disable rendering or interaction of cloned objects
        foreach (GameObject prefab in clonedPrefabs)
        {
            MeshRenderer renderer = prefab.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
          
        }

        foreach (GameObject prefab in clonedCarnivoresPrefabs)
        {
            MeshRenderer renderer = prefab.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
          
        }


         // Set each cloned prefab as the first sibling
        foreach (GameObject prefab in clonedPrefabs)
        {
            if (prefab != null)
            {
                prefab.transform.SetAsFirstSibling();
            }
        }

        // Set each cloned carnivore as the first sibling
        foreach (GameObject carnivore in clonedCarnivoresPrefabs)
        {
            if (carnivore != null)
            {
                carnivore.transform.SetAsFirstSibling();
            }
        }

    }

    public void ResumeGame()
    {
        // Resume the game
        Time.timeScale = 1;

        // Deactivate the Settings Panel
        if (SettingsPanel != null)
        {
            SettingsPanel.SetActive(false);
        }

    }





    public void ResetAndCloneFirstPrefab()
    {
        // Clear the list of cloned prefabs
        foreach (GameObject prefab in clonedPrefabs)
        {
            Destroy(prefab); // Destroy each prefab instance
        }
        clonedPrefabs.Clear();

        // Reset PlayerPrefs related to prefabs
        int count = PlayerPrefs.GetInt("ClonedPrefabCount", 0);
        for (int i = 0; i < count; i++)
        {
            PlayerPrefs.DeleteKey("PrefabPosition_" + i);
        }
        PlayerPrefs.SetInt("ClonedPrefabCount", 0);

        // Instantiate and add the first new prefab
        GameObject firstPrefab = Instantiate(prefabToClone);
        clonedPrefabs.Add(firstPrefab);

        Debug.Log("Prefabs list reset. One new prefab cloned.");
    }




}


