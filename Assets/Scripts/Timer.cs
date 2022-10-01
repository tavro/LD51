using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    List<Notification> notifications = new List<Notification>();

    [SerializeField]
    TextMeshProUGUI textMesh;

    [SerializeField]
    float maxTime = 11.0f;
    [SerializeField]
    float startTime = 1.0f;

    float timePassed;
    int day = 1;

    void Start() {
        timePassed = startTime;
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed >= maxTime) {
            timePassed = startTime;
            day++;
            foreach (Notification notification in notifications) {
                notification.Notify();
            }
        }
        textMesh.text = "Day " + day + ":" + ((int)timePassed).ToString();
    }

    public void AddNotification(Notification notification) {
        notifications.Add(notification);
    }
}
