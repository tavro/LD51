using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSlot : MonoBehaviour, IInteractable
{
    GameObject buildingsUI;

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
        buildingsUI = FindInactiveObjectByName("Buildings");
    }

    public void OnInteraction() {
        BuildHandler bh = buildingsUI.GetComponent<BuildHandler>();
        bh.SetBuildSlot(this);
        buildingsUI.SetActive(true);
    }

    public string GetInteractionDesc() {
        return "open build screen";
    }
}
