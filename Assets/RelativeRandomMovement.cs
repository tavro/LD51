using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeRandomMovement : MonoBehaviour
{
    [SerializeField]
    float offset;
    
    [SerializeField]
    float speed;

    Vector2 target;
    Vector2 startPos;

    void GetRandomStartPosition() {
        float randomX = Random.Range(transform.position.x - offset, transform.position.x + offset);
        float randomY = Random.Range(transform.position.y - offset, transform.position.y + offset);
        transform.position = new Vector2(randomX, randomY);
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
