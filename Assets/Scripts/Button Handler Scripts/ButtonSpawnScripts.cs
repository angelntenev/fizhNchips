using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawnScripts : MonoBehaviour
{
    public GameObject clownFish;
    public GameObject carnivore;
    public MoneyManager moneyManager;

    void Start()
    {
        // Find the GameManager object and get the GameManager script attached to it
        moneyManager = GameObject.Find("GameManager").GetComponent<MoneyManager>();
        
    }
    public void SpawnClownfish()
    {
        bool didSpend = moneyManager.SpendMoney(100);
        if (didSpend)
        {
            Instantiate(clownFish);
        }
        else
        {
            Debug.Log("No money");
        }
    }

    public void SpawnCarnivore()
    {
        bool didSpend = moneyManager.SpendMoney(100);
        if (didSpend)
        {
            Instantiate(carnivore);
        }
        else
        {
            Debug.Log("No money");
        }
    }
}
