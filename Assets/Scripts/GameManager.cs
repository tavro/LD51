using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool IsPaused { set; get; }

    private List<Notification> notifications;
    public void AddNotification(Notification notification) {
        notifications.Add(notification);
    }

    private const float DAY_LENGTH = 10.0f;
    private float dayTimer;
    public int Day { private set; get; }
    
    private Dictionary<string, int> buildingInteractionDays;  
    public int DaysSinceInteraction { private set; get; }

    public Inventory Inventory { private set; get; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Inventory = new Inventory();
        notifications = new List<Notification>();
        Day = 1;
        buildingInteractionDays = new Dictionary<string, int>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
            IsPaused = !IsPaused;

        if (!IsPaused && GetState() == State.FARM)
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

    public void InteractWithBuilding(string key)
    {
        DaysSinceInteraction = Day - buildingInteractionDays.GetValueOrDefault(key, 1);
        buildingInteractionDays[key] = Day;
        Debug.Log($"Days since interaction: {DaysSinceInteraction}");
    }

    public State GetState() { 
        Scene currScene = SceneManager.GetActiveScene();
        if (currScene.name == "FarmScene")
            return State.FARM;
        return State.MINIGAME;
    }

    public enum State { FARM, MINIGAME }
}
