using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float maxTime = 11.0f;
    public float startTime = 1.0f;
    float timePassed;

    void Start() {
        timePassed = startTime;
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed >= maxTime) {
            timePassed = startTime;
        }
        textMesh.text = ((int)timePassed).ToString();
    }
}
