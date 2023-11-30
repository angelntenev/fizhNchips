using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int totalMoney; // Initial amount of money the player starts with
    public TextMeshProUGUI scoreText;

    void Start()
    {
        LoadMoney(); // Load money from PlayerPrefs
    }

    void Update()
    {
        scoreText.text = "$" + totalMoney.ToString();
    }

    // Method to add money and update the total
    public void AddMoney(int amount)
    {
        totalMoney += amount;
        SaveMoney(); // Save the updated amount
    }

    // Method to deduct money if enough is available
    public bool SpendMoney(int amount)
    {
        if (amount <= totalMoney)
        {
            totalMoney -= amount;
            SaveMoney(); // Save the updated amount
            return true; // The spend was successful
        }
        else
        {
            return false; // Not enough money to spend
        }
    }

    // Getter to retrieve the current amount of money
    public int GetTotalMoney()
    {
        return totalMoney;
    }

    // Method to save money to PlayerPrefs
    private void SaveMoney()
    {
        PlayerPrefs.SetInt("TotalMoney", totalMoney);
    }

    // Method to load money from PlayerPrefs
    private void LoadMoney()
    {
        totalMoney = PlayerPrefs.GetInt("TotalMoney", 0); // Load with a default of 0 if not previously saved
    }
}
