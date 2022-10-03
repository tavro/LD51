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

    bool canBuySheepFarm = true;
    bool canBuyCowFarm = true;
    bool canBuyFarmFarm = true;

    [SerializeField] GameObject sheepUI;
    [SerializeField] GameObject cowUI;
    [SerializeField] GameObject farmUI;

    void Build(GameObject prefab, string name) {
        Instantiate(prefab, buildSlot.transform.position, Quaternion.identity);
        GameManager.Instance.AddBuilding(name, buildSlot.transform.position);
        DestroyBuildSlot();
    }

    void Update() {
        if(canBuySheepFarm && Input.GetKeyDown(KeyCode.S)) {
            Buyable buyable = new Buyable(null, 0, "SheepFarm");
            if(GameManager.Instance.CoinManager.Buy(buyable)) {
                Build(sheepFarmPrefab, buyable.name);
                canBuySheepFarm = false;
                sheepUI.gameObject.SetActive(false);
            }
        }
        else if(canBuyCowFarm && Input.GetKeyDown(KeyCode.C)) {
            Buyable buyable = new Buyable(null, 0, "CowFarm");
            if(GameManager.Instance.CoinManager.Buy(buyable)) {
                Build(cowFarmPrefab, buyable.name);
                canBuyCowFarm = false;
                cowUI.gameObject.SetActive(false);
            }
        }
        else if(canBuyFarmFarm && Input.GetKeyDown(KeyCode.F)) {
            Buyable buyable = new Buyable(null, 0, "FarmFarm");
            if(GameManager.Instance.CoinManager.Buy(buyable)) {
                Build(farmPrefab, buyable.name);
                canBuyFarmFarm = false;
                farmUI.gameObject.SetActive(false);
            }
        }
    }
}
