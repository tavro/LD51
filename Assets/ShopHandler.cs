using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] private GameObject egg;
    [SerializeField] private GameObject wool;
    [SerializeField] private GameObject milk;
    [SerializeField] private GameObject crop;

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
