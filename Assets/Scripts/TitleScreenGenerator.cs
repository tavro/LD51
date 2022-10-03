using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenGenerator : MonoBehaviour
{
    [SerializeField] private int animalAmount;
    [SerializeField] private int decorationAmount;

    [SerializeField] private List<GameObject> decorations = new List<GameObject>();
    [SerializeField] private List<GameObject> animals = new List<GameObject>();

    void Start() {
        for(int i = 0; i < animalAmount; i++)
            Instantiate(animals[Random.Range(0, animals.Count)]);
        for(int i = 0; i < decorationAmount; i++)
            Instantiate(decorations[Random.Range(0, decorations.Count)]);
    }
}
