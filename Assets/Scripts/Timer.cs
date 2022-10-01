using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float maxTime = 10.0f;
    float timePassed = 0.0f;

    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed >= maxTime) {
            timePassed = 0.0f;
        }
        textMesh.text = timePassed.ToString();
    }
}
