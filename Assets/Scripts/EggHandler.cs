using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggHandler : MonoBehaviour
{
    [SerializeField]
    GameObject eggPrefab;
    Egg lastEgg;

    public int amount = 3;
    public int maxAmount = 8;

    string word = "";
    int wordIndex = 0;

    void Start()
    {
        amount = amount + GameManager.Instance.DaysSinceInteraction;
        if(amount > maxAmount) {
            amount = maxAmount;
        }

        for(int i = 0; i < amount; i++) {
            Vector2 position = new Vector2(-(amount/2.0f) + i * 1.5f, 0.0f);
            GameObject temp = Instantiate(eggPrefab, position, Quaternion.identity);
            if(lastEgg) {
                lastEgg.SetNextEgg(temp.GetComponent<Egg>());
            }
            else {
                temp.GetComponent<Egg>().isActive = true;
                if(word == "") {
                    word = DictionaryProcessor.GetWordOfLength(amount);
                }
            }
            lastEgg = temp.GetComponent<Egg>();
            KeyCode code = DictionaryProcessor.StringToKeyCode(word[wordIndex].ToString());
            lastEgg.SetKeyCode(code);
            wordIndex++;
        }
        lastEgg.isActive = false;
    }
}
