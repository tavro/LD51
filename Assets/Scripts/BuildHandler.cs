using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildHandler : MonoBehaviour
{
    BuildSlot buildSlot;

    [SerializeField]
    GameObject sheepFarmPrefab;
    [SerializeField]
    GameObject cowFarmPrefab;
    [SerializeField]
    GameObject farmPrefab;

    bool canBuySheepFarm = true;
    bool canBuyCowFarm = true;
    bool canBuyFarmFarm = true;

    [SerializeField] GameObject sheepUI;
    [SerializeField] GameObject cowUI;
    [SerializeField] GameObject farmUI;

    void Start() {
        SetCanBuyBools();
        SetUIDisplays();
    }

    void SetCanBuyBools() {
        canBuySheepFarm = !CheckIfBuilt("SheepFarm");
        canBuyCowFarm = !CheckIfBuilt("CowFarm");
        canBuyFarmFarm = !CheckIfBuilt("FarmFarm");
    }

    void SetUIDisplays() {
        sheepUI.SetActive(canBuySheepFarm);
        cowUI.SetActive(canBuyCowFarm);
        farmUI.SetActive(canBuyFarmFarm);
    }

    bool CheckIfBuilt(string name) {
        return GameManager.Instance.boughtBuildings.ContainsKey(name);
    }

    public void SetBuildSlot(BuildSlot slot) {
        buildSlot = slot;
    }

    void CloseUI() {
        gameObject.SetActive(false);
    }

    void DestroyBuildSlot() {
        string postfix = (int)buildSlot.transform.position.x + ":" + (int)buildSlot.transform.position.y;
        GameManager.Instance.RemoveBuilding("BuildSlot" + postfix);
        Destroy(buildSlot.gameObject);
        CloseUI();
    }

    void Build(GameObject prefab, Vector2 position, string name) {
        Instantiate(prefab, position, Quaternion.identity);
        GameManager.Instance.AddBuilding(name, position);
        SetCanBuyBools();
        SetUIDisplays();
        DestroyBuildSlot();
    }

    float GetBuildOffset() {
        float offset = -1.0f;
        if(buildSlot.transform.position.y > 0.0f)
            offset *= -1;
        return offset;
    }

    void Update() {
        if(canBuySheepFarm && Input.GetKeyDown(KeyCode.S)) {
            Buyable buyable = new Buyable(null, 25, "SheepFarm");
            if(GameManager.Instance.CoinManager.Buy(buyable)) {
                Vector2 pos = new Vector2(buildSlot.transform.position.x, buildSlot.transform.position.y + GetBuildOffset());
                Build(sheepFarmPrefab, pos, buyable.name);
            }
        }
        else if(canBuyCowFarm && Input.GetKeyDown(KeyCode.C)) {
            Buyable buyable = new Buyable(null, 25, "CowFarm");
            if(GameManager.Instance.CoinManager.Buy(buyable)) {
                Vector2 pos = new Vector2(buildSlot.transform.position.x, buildSlot.transform.position.y - GetBuildOffset());
                Build(cowFarmPrefab, pos, buyable.name);
            }
        }
        else if(canBuyFarmFarm && Input.GetKeyDown(KeyCode.F)) {
            Buyable buyable = new Buyable(null, 25, "FarmFarm");
            if(GameManager.Instance.CoinManager.Buy(buyable)) {
                Build(farmPrefab, buildSlot.transform.position, buyable.name);
            }
        }
    }
}
