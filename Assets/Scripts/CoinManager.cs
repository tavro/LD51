using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager
{
    public const string COIN_NAME = "Coins";
    private int coinCount = 0;

    public void UpdateUI() {
        GameObject moneyUI = GameObject.Find("Money Text");
        if (moneyUI != null) // Added null-check here to avoid error outside of FarmScene
            moneyUI.GetComponent<TextMeshProUGUI>().text = GetCoinCount().ToString();
    }

    public bool Buy(Buyable item) // returns true if you have enough money to buy, else false
    {
        if(coinCount >= item.price) {
            coinCount -= item.price;
            UpdateUI();
            return true;
        }
        return false;
    }

    public void Sell(Sellable item)
    {
        coinCount += item.price;
        UpdateUI();
    }

    public void SetCoinCount(int count) {
        coinCount = count;
        UpdateUI();
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}
