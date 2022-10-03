using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        private set => _instance = value;
        get
        {
            if (_instance == null)
            {
                _instance = (GameManager)FindObjectOfType(typeof(GameManager));
                if (_instance == null) // AKA if non-existant
                    _instance = new GameObject("Game Manager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    public PauseState CurrPauseState { get; private set; }
    public PauseState PrevPauseState { get; private set; }

    private List<Notification> notifications;
    public void AddNotification(Notification notification) {
        notifications.Add(notification);
    }

    private const float DAY_LENGTH = 10.0f;
    private float dayTimer;
    public int Day { private set; get; }
    
    public Dictionary<string, Vector2> boughtBuildings;
    private Dictionary<string, int> buildingInteractionDays;  
    public int DaysSinceInteraction { private set; get; }

    public Inventory Inventory { private set; get; }
    public CoinManager CoinManager { private set; get; }
    public Vector3 LastPlayerPos { set; get; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Inventory = new Inventory();

        CoinManager = new CoinManager();
        CoinManager.SetCoinCount(100);

        notifications = new List<Notification>();
        Day = 1;
        buildingInteractionDays = new Dictionary<string, int>();

        boughtBuildings = new Dictionary<string, Vector2>();
        boughtBuildings["BuildSlot0:-3"] = new Vector2(0.0f, -3.0f);
        boughtBuildings["BuildSlot6:2"] = new Vector2(6.0f, 2.0f);
        boughtBuildings["BuildSlot-5:-3"] = new Vector2(-5.0f, -3.0f);

        CurrPauseState = PauseState.NONE;
        PrevPauseState = PauseState.NONE;
    }

    public void SetPauseState(PauseState newPauseState)
    {
        PrevPauseState = CurrPauseState;
        CurrPauseState = newPauseState;
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

    private void Start()
    {
        if (FindObjectOfType<GameManager>() != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (CurrPauseState != PauseState.FULL)
                SetPauseState(PauseState.FULL);
            else
                SetPauseState(PrevPauseState);
        }

        if (CurrPauseState == PauseState.NONE && GetState() == State.FARM)
        {
            dayTimer += Time.deltaTime;
            if (dayTimer >= DAY_LENGTH)
            {
                dayTimer %= DAY_LENGTH; // Keeps the extra remainder
                Day += 1;

                if(GameObject.Find("Day Text"))
                    GameObject.Find("Day Text").GetComponent<TextMeshProUGUI>().text = "Day: " + Day.ToString();

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

    public enum PauseState { NONE, MENU, FULL }
}
