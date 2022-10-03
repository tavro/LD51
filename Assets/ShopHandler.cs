using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject egg;
    [SerializeField] private GameObject wool;
    [SerializeField] private GameObject milk;
    [SerializeField] private GameObject crop;

    public void OnInteraction() {
        while(GameManager.Instance.Inventory.eggAmount > 0) {
            Sellable sellable = new Sellable(null, 30, "");
            GameManager.Instance.CoinManager.Sell(sellable); // 30
            GameManager.Instance.Inventory.eggAmount--;
        }
        while(GameManager.Instance.Inventory.woolAmount > 0) {
            Sellable sellable = new Sellable(null, 20, "");
            GameManager.Instance.CoinManager.Sell(sellable); // 30
            GameManager.Instance.Inventory.woolAmount--;
        }
        while(GameManager.Instance.Inventory.milkAmount > 0) {
            Sellable sellable = new Sellable(null, 100, "");
            GameManager.Instance.CoinManager.Sell(sellable); // 50
            GameManager.Instance.Inventory.milkAmount--;
        }
        while(GameManager.Instance.Inventory.cropAmount > 0) {
            Sellable sellable = new Sellable(null, 20, "");
            GameManager.Instance.CoinManager.Sell(sellable); // 20
            GameManager.Instance.Inventory.cropAmount--;
        }
    }

    bool HasItems() {
        return GameManager.Instance.Inventory.eggAmount > 0 || GameManager.Instance.Inventory.woolAmount > 0 || GameManager.Instance.Inventory.milkAmount > 0 || GameManager.Instance.Inventory.cropAmount > 0;
    }

    public string GetInteractionDesc() {
        if(HasItems()) {
            return "sell all items";
        }
        else {
            return "sell nothing";
        }
    }

    void Hide(GameObject obj) {
        if(obj.activeSelf) {
            obj.SetActive(false);
        }
    }

    void Show(GameObject obj) {
        if(!obj.activeSelf) {
            obj.SetActive(true);
        }
    }

    void Update()
    {
        if(GameManager.Instance.Inventory.eggAmount > 0) {
            Show(egg);
        }
        else {
            Hide(egg);
        }

        if(GameManager.Instance.Inventory.woolAmount > 0) {
            Show(wool);
        }
        else {
            Hide(wool);
        }

        if(GameManager.Instance.Inventory.milkAmount > 0) {
            Show(milk);
        }
        else {
            Hide(milk);
        }

        if(GameManager.Instance.Inventory.cropAmount > 0) {
            Show(crop);
        }
        else {
            Hide(crop);
        }
    }
}
