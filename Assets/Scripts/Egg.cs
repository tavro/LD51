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
        if(isActive) {
            /*if(Input.anyKey && !Input.GetKeyDown(activeKey)) {
                RemoveEgg();
            }
            else*/
            if(Input.GetKeyDown(activeKey)) {
                AddToInventory();
                RemoveEgg();
            }
        }
    }

    void AddToInventory() {
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        inventory.eggAmount++;
    }

    void RemoveEgg() {
        if(nextEgg) {
            nextEgg.isActive = true;
        }
        else {
            SceneManager.LoadScene("FarmScene");
        }
        Destroy(gameObject);
    }

}
