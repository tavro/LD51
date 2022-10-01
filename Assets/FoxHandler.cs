using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxHandler : MonoBehaviour
{
    [SerializeField]
    GameObject foxPrefab;
    [SerializeField]
    float spawnTime = 3.0f;
    float timePassed = 0.0f;

    void Update()
    {
        if(GameObject.Find("Farm").transform.childCount > 0) {
            timePassed += Time.deltaTime;
            if(timePassed >= spawnTime) {
                timePassed = 0.0f;
                Instantiate(foxPrefab, new Vector2(Random.Range(-8.0f, 8.0f), 6.0f), Quaternion.identity);

            }
        }
    }
}
