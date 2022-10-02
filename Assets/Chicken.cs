using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField]
    float offset;
    [SerializeField]
    float speed;

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
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
        if(Vector2.Distance(transform.position, target) <= 0.01f) {
            GetNewTargetPosition();
        }
    }
}
