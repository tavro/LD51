using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Start()
    {
        activeKey = keyCodes[Random.Range(0, keyCodes.Count)];
        textMesh.text = activeKey.ToString();
    }

    void Update()
    {
        if(isActive) {
            /*if(Input.anyKey && !Input.GetKeyDown(activeKey)) {
                RemoveEgg();
            }
            else*/ 
            if(Input.GetKeyDown(activeKey)) {
                // Add egg to inventory
                RemoveEgg();
            }
        }
    }

    void RemoveEgg() {
        if(nextEgg) {
            nextEgg.isActive = true;
        }
        Destroy(gameObject);
    }

}
