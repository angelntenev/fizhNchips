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

    }

    void Update()
    {
        scoreText.text = "$" + totalMoney.ToString();
    }

    // Method to add money and update the total
    public void AddMoney(int amount)
    {
        totalMoney += amount;
    }

    // Method to deduct money if enough is available
    public bool SpendMoney(int amount)
    {
        if (amount <= totalMoney)
        {
            totalMoney -= amount;
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
}
