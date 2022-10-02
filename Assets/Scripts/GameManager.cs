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
    
    private Dictionary<string, Vector2> boughtBuildings;
    private Dictionary<string, int> buildingInteractionDays;  
    public int DaysSinceInteraction { private set; get; }

    public Inventory Inventory { private set; get; }
    public CoinManager CoinManager { private set; get; }
    public Vector3 LastPlayerPos { set; get; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Inventory = new Inventory();
        notifications = new List<Notification>();
        Day = 1;
        boughtBuildings = new Dictionary<string, Vector2>();
        buildingInteractionDays = new Dictionary<string, int>();

        boughtBuildings["BuildSlot0:-4"] = new Vector2(0.0f, -4.0f);
        boughtBuildings["BuildSlot8:4"] = new Vector2(8.0f, 4.0f);
        boughtBuildings["BuildSlot-8:-4"] = new Vector2(-8.0f, -4.0f);
    }

    public void AddBuilding(string name, Vector2 position) {
        boughtBuildings[name] = position;
    }

    public void RemoveBuilding(string key) {
        boughtBuildings.Remove(key);
    }

    [SerializeField] private GameObject cowFarmPrefab;
    [SerializeField] private GameObject sheepFarmPrefab;
    [SerializeField] private GameObject farmFarmPrefab;
    [SerializeField] private GameObject buildSlotPrefab;
    public void PlaceBoughtBuildings() {
        foreach(var item in boughtBuildings) {
            switch(item.Key) {
                case "CowFarm":
                    Instantiate(cowFarmPrefab, item.Value, Quaternion.identity);
                    break;
                case "SheepFarm":
                    Instantiate(sheepFarmPrefab, item.Value, Quaternion.identity);
                    break;
                case "FarmFarm":
                    Instantiate(farmFarmPrefab, item.Value, Quaternion.identity);
                    break;
                default:
                    if(item.Key.Contains("BuildSlot")) {
                        Instantiate(buildSlotPrefab, item.Value, Quaternion.identity);
                    }
                    break;
            }
        }
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
