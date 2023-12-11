using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DayCounter : MonoBehaviour
{
    public int day = 0;
    public TextMeshProUGUI dayCounterText;

    public void AddDay()
    {
        day++;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        dayCounterText.text = day.ToString();
    }
}
