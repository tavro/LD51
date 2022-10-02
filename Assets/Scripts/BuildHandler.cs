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
        Destroy(buildSlot.gameObject);
        CloseUI();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.S)) {
            Instantiate(sheepFarmPrefab, buildSlot.transform.position, Quaternion.identity);
            DestroyBuildSlot();
        }
        else if(Input.GetKeyDown(KeyCode.C)) {
            Instantiate(cowFarmPrefab, buildSlot.transform.position, Quaternion.identity);
            DestroyBuildSlot();
        }
        else if(Input.GetKeyDown(KeyCode.F)) {
            Instantiate(farmPrefab, buildSlot.transform.position, Quaternion.identity);
            DestroyBuildSlot();
        }
    }
}
