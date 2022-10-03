using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager
{
    public const string COIN_NAME = "Coins";
    private int coinCount = 0;

    public void UpdateUI() {
        GameObject.Find("Money Text").GetComponent<TextMeshProUGUI>().text = GetCoinCount().ToString();
    }

    public void Buy(Buyable item)
    {
        if(coinCount >= item.price) {
            coinCount -= item.price;
            UpdateUI();
        }
    }

    public void Sell(Sellable item)
    {
        coinCount += item.price;
        UpdateUI();
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}
