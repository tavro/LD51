using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager
{
    public const string COIN_NAME = "Coins";
    private int coinCount = 0;

    public void Buy(Buyable item)
    {
        coinCount -= item.price;
    }

    public void Sell(Sellable item)
    {
        coinCount += item.price;
    }
}
