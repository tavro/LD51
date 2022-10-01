using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string[] temp = text.text.Split(':');
        text.text = temp[0] + ": " + CoinManager.coins;
    }
}
