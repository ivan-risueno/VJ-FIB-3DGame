using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsBehaviour : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public static int totalCoins;

    void Start()
    {
        if (totalCoins < PlayerPrefs.GetInt("TotalCoins", 0))
        {
            totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        }
    }

    void Update()
    {
        coinsText.text = totalCoins.ToString();
    }

    void OnApplicationQuit() {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
    }
}
