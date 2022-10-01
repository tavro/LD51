using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: could change script name, if applicable on interactables other than buildings
public class Building : MonoBehaviour, IInteractable
{
	public void OnInteraction()
    {
        Debug.Log($"Interacted with: {name}");
    }
}
