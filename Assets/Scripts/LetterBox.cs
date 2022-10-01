using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBox : MonoBehaviour, IInteractable
{
    [SerializeField]
    Letter activeLetter;

    Queue<Letter> letters = new Queue<Letter>();

    void Start()
    {
        activeLetter.SetTitle("Letter");
        activeLetter.SetContent("Content of letter");
    }

    public void OnInteraction()
    {
        activeLetter.gameObject.SetActive(true);
    }

    public string GetInteractionDesc()
    {
        return "Read Letter";
    }
}
