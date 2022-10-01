using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI titleTextMesh;

    [SerializeField]
    TextMeshProUGUI contentTextMesh;

    public void SetTitle(string title) {
        titleTextMesh.text = title;
    }

    public void SetContent(string content) {
        contentTextMesh.text = content;
    }
}
