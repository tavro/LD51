using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wool : MonoBehaviour
{
    SpriteRenderer sr;

    [SerializeField] List<Sprite> sprites = new List<Sprite>();

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[Random.Range(0, sprites.Count)];
    }

    void Update() {
        if(GameManager.Instance.CurrPauseState == GameManager.PauseState.NONE && transform.parent == null) {
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
