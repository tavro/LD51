using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    [SerializeField]
    private Vector2 minPos, maxPos;

    void GetRandomStartPosition() {
        transform.position = new Vector2(Random.Range((int)minPos.x, (int)maxPos.x), Random.Range((int)minPos.y, (int)maxPos.y));
    }

    void Start() {
        GetRandomStartPosition();
    }
}
