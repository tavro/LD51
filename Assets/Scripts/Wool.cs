using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wool : MonoBehaviour
{
    SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if(!GameManager.Instance.IsPaused && transform.parent == null) {
            if(sr.color.a > 0.0f) {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime);
            }
            else {
                Inventory inventory = GameManager.Instance.Inventory;
                inventory.woolAmount++;
                Destroy(gameObject);
            }
        }
    }
}