using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSlot : MonoBehaviour, IInteractable
{
    public void OnInteraction() {

    }

    public string GetInteractionDesc() {
        return "open build screen";
    }
}
