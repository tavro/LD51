using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sheep : MonoBehaviour
{
    [SerializeField]
    GameObject woolPrefab;
    [SerializeField]
    float speed;
    [SerializeField]
    float growTime;

    float timePassed;
    int growth;
    int maxGrowth = 10;

    void Start() {
        float amount = GameManager.Instance.DaysSinceInteraction;
        if(amount > 0) {
            for(int i = 0; i < 10; i++) {
                InstantiateWool();
            }
        }
    }

    void InstantiateWool() {
        float woolOffset = 0.2f;
        Vector2 pos = new Vector2(transform.position.x + Random.Range(-woolOffset, woolOffset), transform.position.y + Random.Range(-woolOffset, woolOffset));
        GameObject temp = Instantiate(woolPrefab, pos, Quaternion.identity);
        temp.transform.parent = transform;
        growth++;
    }

    void Update() {
        if (!GameManager.Instance.IsPaused)
        {
            if(growth < maxGrowth) {
                timePassed += Time.deltaTime;
                if(timePassed >= growTime) {
                    timePassed = 0.0f;
                    InstantiateWool();
                }
            }
        }
    }

    void OnMouseOver() {
        if(!GameManager.Instance.IsPaused && transform.childCount > 1 && Input.GetMouseButtonDown(0))
        {
            Transform child;
            do {
                int childIndex = Random.Range(0, transform.childCount);
                child = transform.GetChild(childIndex);
            }
            while(child.gameObject.name == "sprite-sheep-body");
            child.parent = null;
            growth--;
            if(growth == 0) {
                SceneManager.LoadScene("FarmScene");
            }
        }
    }
}
