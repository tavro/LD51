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

    void Update() {
        if(canBuySheepFarm && Input.GetKeyDown(KeyCode.S)) {
            Buyable buyable;
            buyable.price = 0;
            buyable.name = "Sheep Farm";
            if(GameManager.Instance.CoinManager.Buy(buyable)) {
                Instantiate(sheepFarmPrefab, buildSlot.transform.position, Quaternion.identity);
                GameManager.Instance.AddBuilding("SheepFarm", buildSlot.transform.position);
                DestroyBuildSlot();
                canBuySheepFarm = false;
                sheepUI.gameObject.SetActive(false);
            }
        }
        else if(canBuyCowFarm && Input.GetKeyDown(KeyCode.C)) {
            Instantiate(cowFarmPrefab, buildSlot.transform.position, Quaternion.identity);
            GameManager.Instance.AddBuilding("CowFarm", buildSlot.transform.position);
            DestroyBuildSlot();
            canBuyCowFarm = false;
            cowUI.gameObject.SetActive(false);
        }
        else if(canBuyFarmFarm && Input.GetKeyDown(KeyCode.F)) {
            Instantiate(farmPrefab, buildSlot.transform.position, Quaternion.identity);
            GameManager.Instance.AddBuilding("FarmFarm", buildSlot.transform.position);
            DestroyBuildSlot();
            canBuyFarmFarm = false;
            farmUI.gameObject.SetActive(false);
        }
    }
}
