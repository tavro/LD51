using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject winImage, loseImage;

	private void Start()
    {
        bool hasWon = GameManager.Instance.CoinManager.GetCoinCount() >= 10000; // Based on 605 coins in 80 seconds
        winImage.SetActive(hasWon);
        loseImage.SetActive(!hasWon);
    }
}
