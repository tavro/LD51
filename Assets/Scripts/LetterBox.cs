using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBox : MonoBehaviour, IInteractable
{
    [SerializeField]
    Letter activeLetter;

    [SerializeField]
    Sprite openSprite;
    Sprite originalSprite;

    Queue<Letter> letters = new Queue<Letter>();

    void Start()
    {
        originalSprite = GetComponent<SpriteRenderer>().sprite;
        activeLetter.SetTitle("Letter");
        activeLetter.SetContent("Content of letter");
    }

    public void OnInteraction()
    {
        activeLetter.gameObject.SetActive(!activeLetter.gameObject.activeSelf);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(sr.sprite == originalSprite) {
            sr.sprite = openSprite;
        }
        else {
            sr.sprite = originalSprite;
        }
    }

    public string GetInteractionDesc()
    {
        return "Read Letter";
    }
}
