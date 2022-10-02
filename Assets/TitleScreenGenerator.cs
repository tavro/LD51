using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class TitleScreenGenerator : MonoBehaviour
{
    [SerializeField] private int animalAmount;
    [SerializeField] private int decorationAmount;

    [SerializeField] private List<GameObject> decorations = new List<GameObject>();
    [SerializeField] private List<AnimatorController> animations = new List<AnimatorController>();
    
    [SerializeField] private GameObject animalPrefab;

    void Start() {
        for(int i = 0; i < animalAmount; i++) {
            GameObject temp = Instantiate(animalPrefab);
            Animator anim = temp.GetComponent<Animator>();
            anim.runtimeAnimatorController = animations[Random.Range(0, animations.Count)];
        }
        for(int i = 0; i < decorationAmount; i++)
            Instantiate(decorations[Random.Range(0, decorations.Count)]);
    }
}
