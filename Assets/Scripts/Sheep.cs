using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void GetRandomStartPosition() {
        transform.position = new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f));
    }

    void GetNewTargetPosition() {
        float randomX = Random.Range(transform.position.x - offset, transform.position.x + offset);
        float randomY = Random.Range(transform.position.y - offset, transform.position.y + offset);
        target = new Vector2(randomX, randomY);
    }

    void Start() {
        GetRandomStartPosition();
        GetNewTargetPosition();
    }

    void Update() {
        if (!GameManager.Instance.IsPaused)
        {
            if(growth < maxGrowth) {
                timePassed += Time.deltaTime;
                if(timePassed >= growTime) {
                    timePassed = 0.0f;
                    Vector2 pos = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
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
        if(!GameManager.Instance.IsPaused && transform.childCount > 0 && Input.GetMouseButtonDown(0))
        {
            int childIndex = Random.Range(0, transform.childCount);
            Transform child = transform.GetChild(childIndex);
            child.parent = null;
        }
    }
}
