using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyCount : MonoBehaviour
{
    public int currency = 5000; 
    public Text currencyText;
    void Start()
    {
        UpdateCurrencyText();
    }
    public void AddCurrency(int amount)
    {
        currency += amount;
        UpdateCurrencyText(); 
    }

    
    public bool SubtractCurrency(int amount)
    {
        if (currency >= amount)
        {
            currency -= amount;
            UpdateCurrencyText(); 
            return true; 
        }
        else
        {
            return false; 
        }
    }

    void UpdateCurrencyText()
    {
        if (currencyText != null)
        {
            currencyText.text = "Currency: " + currency.ToString();
        }
    }
}
