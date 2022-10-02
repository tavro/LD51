using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: could change script name, if applicable on interactables other than buildings
public class Building : MonoBehaviour, IInteractable
{
    [SerializeField] private string minigameSceneName; 
    [SerializeField] private string interactionDesc;
    [SerializeField] private string interactionKey;

	public void OnInteraction()
    {
        GameManager.Instance.InteractWithBuilding(interactionKey);
        SceneManager.LoadScene(minigameSceneName);
    }

    public string GetInteractionDesc()
    {
        return interactionDesc;
    }
}
