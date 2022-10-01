using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    [SerializeField]
    GameObject targetPrefab;
    [SerializeField]
    float offset;
    [SerializeField]
    float speed;
    [SerializeField]
    float timeout;

    float timePassed;
    bool isReady;
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
        if(!isReady) {
            timePassed += Time.deltaTime;
            if(timePassed >= timeout) {
                timePassed = 0.0f;
                isReady = true;
            }
        }

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
        if(Vector2.Distance(transform.position, target) <= 0.01f) {
            GetNewTargetPosition();
        }
    }

    public void Milk() {
        GameObject.Find("Inventory").GetComponent<Inventory>().milkAmount++;
        isReady = false;
    }

    void OnMouseOver() {
        if(isReady) {
            if(Input.GetMouseButtonDown(0)) {
                Vector2 pos = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y - 1.0f);
                GameObject temp = Instantiate(targetPrefab, pos, Quaternion.identity);
                temp.GetComponent<Target>().targetCow = this;
            }
            if(Input.GetMouseButtonUp(0)) {
                //Destroy target
            }
        }
    }
}
