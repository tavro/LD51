using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBox : MonoBehaviour
{
    [SerializeField]
    Letter activeLetter;

    Queue<Letter> letters = new Queue<Letter>();

    void Start()
    {
        activeLetter.SetTitle("Letter");
        activeLetter.SetContent("Content of letter");
    }

    void OnMouseOver() {
        if(Input.GetMouseButtonDown(0)) {
            activeLetter.gameObject.SetActive(true);
        }
    }
}
