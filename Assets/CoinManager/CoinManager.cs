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
        Debug.Log(coins);
    }
    public static void Buy(int price)
    {
        coins -= price;

        UpdateCoins();
    }
    public static void Sell(int price)
    {
        coins += price;
        UpdateCoins();
    }
}
