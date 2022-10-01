using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Cow targetCow;

    void OnMouseOver() {
        if(Input.GetMouseButtonUp(0)) {
            targetCow.Milk();
            Destroy(gameObject);
        }
    }
}
