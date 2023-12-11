using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bank : MonoBehaviour
{
    private int bankBalance = 0;
    public TextMeshProUGUI textBox;

    private void Awake()
    {
        UpdateBankDisplay();
    }
    
    public void AddToBank(int money)
    {
        bankBalance += money;
        UpdateBankDisplay();
    }

    public void UpdateBankDisplay()
    {
        textBox.text = bankBalance.ToString();
    }
}
