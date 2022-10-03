using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Cow targetCow;
    public GameObject milkPrefab;

    void OnMouseOver() {
        if(GameManager.Instance.CurrPauseState == GameManager.PauseState.NONE && Input.GetMouseButtonUp(0)) {
            targetCow.Milk();
            Instantiate(milkPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
