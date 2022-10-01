using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggHandler : MonoBehaviour
{
    [SerializeField]
    GameObject eggPrefab;
    Egg lastEgg;

    public int amount = 3;

    void Start()
    {
        for(int i = 0; i < amount; i++) {
            Vector2 position = new Vector2(-(amount/2.0f) + i, 0.0f);
            GameObject temp = Instantiate(eggPrefab, position, Quaternion.identity);
            if(lastEgg) {
                lastEgg.SetNextEgg(temp.GetComponent<Egg>());
            }
            else {
                temp.GetComponent<Egg>().isActive = true;
            }
            lastEgg = temp.GetComponent<Egg>();
        }
        lastEgg.isActive = false;
    }
}
