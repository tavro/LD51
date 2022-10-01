using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        private set => _instance = value;
        get
        {
            if (_instance == null)
                _instance = (GameManager)FindObjectOfType(typeof(GameManager));
            return _instance;
        }
    }

    private List<Notification> notifications;

    private const float DAY_LENGTH = 10.0f;
    private float dayTimer;
    public int Day { private set; get; } // TODO: integrate timer fully

    public Inventory Inventory { private set; get; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Inventory = new Inventory();
        notifications = new List<Notification>();
        Day = 1;
    }

    private void Update()
    {
        dayTimer += Time.deltaTime;
        if (dayTimer >= DAY_LENGTH)
        {
            dayTimer %= DAY_LENGTH; // Keeps the extra remainder
            Day += 1;

            foreach (Notification notification in notifications)
                notification.Notify();

            Debug.Log($"Current day: {Day}"); // TODO: remove (debug purposes)
        }
    }
}
