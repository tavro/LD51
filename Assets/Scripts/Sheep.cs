using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sheep : MonoBehaviour
{
    [SerializeField]
    GameObject woolPrefab;
    [SerializeField]
    float offset;
    [SerializeField]
    float speed;
    [SerializeField]
    float growTime;

    float timePassed;
    int growth;
    int maxGrowth = 10;
    Vector2 target;
    Vector2 startPos;

    void GetRandomStartPosition() {
        transform.position = new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f));
    }

    void GetNewTargetPosition() {
        float randomX = Random.Range(startPos.x - offset, startPos.x + offset);
        float randomY = Random.Range(startPos.y - offset, startPos.y + offset);
        target = new Vector2(randomX, randomY);
    }

    void Start() {
        GetRandomStartPosition();
        startPos = transform.position; 
        GetNewTargetPosition();
    }

    void Update() {
        if (!GameManager.Instance.IsPaused)
        {
            if(growth < maxGrowth) {
                timePassed += Time.deltaTime;
                if(timePassed >= growTime) {
                    timePassed = 0.0f;
                    float woolOffset = 0.35f;
                    Vector2 pos = new Vector2(transform.position.x + Random.Range(-woolOffset, woolOffset), transform.position.y + Random.Range(-woolOffset, woolOffset));
                    GameObject temp = Instantiate(woolPrefab, pos, Quaternion.identity);
                    temp.transform.parent = transform;
                    growth++;
                }
            }

            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, step);
            if(Vector2.Distance(transform.position, target) <= 0.01f) {
                GetNewTargetPosition();
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
