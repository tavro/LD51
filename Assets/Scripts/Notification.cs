using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    SpriteRenderer sr;
    bool isNotified;

    [SerializeField]
    float appearTime = 2.0f;

    float timePassed = 0.0f;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if(isNotified) {
            if(sr.color.a <= 1.0f) {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + Time.deltaTime);
            }
            else {
                timePassed += Time.deltaTime;
                if(timePassed >= appearTime) {
                    isNotified = false;
                    timePassed = 0.0f;
                }
            }
        } else {
            if(sr.color.a >= 0.0f) {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime);
            }
        }
    }

    public void Notify() {
        isNotified = true;
    }
}
