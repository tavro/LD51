using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    // Start is called before the first frame update
    public const string Coins = "Coins";
    public static int coins = 0;
    void Start()
    {
        coins = PlayerPrefs.GetInt(Coins);
        UpdateCoins();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void UpdateCoins()
    {
        PlayerPrefs.SetInt(Coins, coins);
        coins = PlayerPrefs.GetInt(Coins);
        PlayerPrefs.Save();
    }
    public static void Buy(Buyable item)
    {
        coins -= item.price;
        UpdateCoins();
    }
    public static void Sell(Buyable item)
    {
        coins += item.price;
        UpdateCoins();
    }
}
