using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    [SerializeField]
    float speed;

    GameObject target;

    void Start() {
        target = GameObject.Find("Farm");
    }

    void Update() {
        if (!GameManager.Instance.IsPaused)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
            if(Vector2.Distance(transform.position, target.transform.position) <= 0.01f) {
                if(target.transform.childCount > 0) {
                    Destroy(target.transform.GetChild(0).gameObject);
                }
                Destroy(gameObject);
            }
        }
    }
}
