using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Egg : MonoBehaviour
{
    [SerializeField]
    List<KeyCode> keyCodes = new List<KeyCode>();

    [SerializeField]
    TextMeshProUGUI textMesh;

    KeyCode activeKey;

    [SerializeField]
    Egg nextEgg;

    public bool isActive;
    bool isRemoved;

    Vector2 offsetPos;

    SpriteRenderer sr;

    void Start() {
        offsetPos = new Vector2(transform.position.x, transform.position.y - 0.15f);
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetNextEgg(Egg egg) {
        nextEgg = egg;
    }

    public void SetKeyCode(KeyCode key) {
        activeKey = key;
        textMesh.text = activeKey.ToString();
    }

    public KeyCode GetKeyCode() {
        return activeKey;
    }

    void Update()
    {
        if(isRemoved) {
            transform.position = new Vector2(transform.position.x, transform.position.y + Time.deltaTime);
            if(sr.color.a > 0.0f) {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime);
            }
            else {
                if(!nextEgg) {
                    SceneManager.LoadScene("FarmScene");
                }
                Destroy(gameObject);
            }
        }
        else {
            if(isActive) {
                if(Input.GetKeyUp(activeKey)) {
                    AddToInventory();
                    RemoveEgg();
                }
                if(Input.anyKey) {
                    if(transform.position.y != offsetPos.y) {
                        transform.position = offsetPos;
                    }
                }
            }
        }
    }

    void AddToInventory() {
        Inventory inventory = GameManager.Instance.Inventory;
        inventory.eggAmount++;
    }

    void RemoveEgg() {
        if(nextEgg) {
            nextEgg.isActive = true;
        }
        isRemoved = true;
    }

}
