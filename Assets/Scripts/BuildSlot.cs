using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSlot : MonoBehaviour, IInteractable
{
    BuildHandler buildHandler;

    GameObject FindInactiveObjectByName(string name) {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

    void Start() {
        buildHandler = FindInactiveObjectByName("Shop").GetComponent<BuildHandler>();
    }

    public void OnInteraction() {
        if (GameManager.Instance.CurrPauseState != GameManager.PauseState.FULL)
        {
            if (buildHandler.gameObject.activeSelf)
            {
                buildHandler.gameObject.SetActive(false);
                GameManager.Instance.SetPauseState(GameManager.PauseState.NONE);
            }
            else
            {
                buildHandler.gameObject.SetActive(true);
                buildHandler.SetBuildSlot(this);
                GameManager.Instance.SetPauseState(GameManager.PauseState.MENU);
            }
        }
    }

    public string GetInteractionDesc() {
        if (buildHandler.gameObject.activeSelf)
            return "Close build menu";
        return "Open build menu";
    }
}
