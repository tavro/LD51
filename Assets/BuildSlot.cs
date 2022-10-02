using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSlot : MonoBehaviour, IInteractable
{
    [SerializeField]
    GameObject buildingsUI;

    public void OnInteraction() {
        BuildHandler bh = buildingsUI.GetComponent<BuildHandler>();
        bh.SetBuildSlot(this);
        buildingsUI.SetActive(true);
    }

    public string GetInteractionDesc() {
        return "open build screen";
    }
}
