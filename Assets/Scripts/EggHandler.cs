using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggHandler : MonoBehaviour
{
    [SerializeField] private Egg eggPrefab;
    [SerializeField] private float eggInterval;
    private Egg lastEgg;

    public int amount = 3;
    public int maxAmount = 13;

    string word = "";
    int wordIndex = 0;

    void Start()
    {
        amount = amount + GameManager.Instance.DaysSinceInteraction;
        if(amount > maxAmount)
            amount = maxAmount;

        for(int i = 0; i < amount; i++) 
        {
            Vector2 eggPos = Vector2.right * (-eggInterval * (amount - 1) / 2 + i * eggInterval);
            Egg newEgg = Instantiate(eggPrefab, eggPos, Quaternion.identity) as Egg;
            if (lastEgg) 
            {
                lastEgg.SetNextEgg(newEgg);
            }
            else 
            {
                newEgg.isActive = true;
                if(word == "")
                    word = DictionaryProcessor.GetWordOfLength(amount);
            }
            lastEgg = newEgg;
            KeyCode code = DictionaryProcessor.StringToKeyCode(word[wordIndex].ToString());
            lastEgg.SetKeyCode(code);
            wordIndex++;
        }
        lastEgg.isActive = false;
    }
}
