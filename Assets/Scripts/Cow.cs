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
    
    GameObject temp;
    void Update() {
        if (!GameManager.Instance.IsPaused)
        {
            if(!isReady) {
                timePassed += Time.deltaTime;
                if(timePassed >= timeout) {
                    timePassed = 0.0f;
                    isReady = true;
                }
            }
            else {
                if(Input.GetMouseButtonUp(0)) {
                    if(temp) {
                        Destroy(temp);
                    }
                    isReady = false;
                }
            }

            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, step);
            if(Vector2.Distance(transform.position, target) <= 0.01f) {
                GetNewTargetPosition();
            }
        }
    }

    public void Milk() {
        Inventory inventory = GameManager.Instance.Inventory;
        inventory.milkAmount++;
        isReady = false;
    }

    void OnMouseOver() {
        if(!GameManager.Instance.IsPaused && isReady) {
            if(Input.GetMouseButtonDown(0)) {
                Vector2 pos = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y - 1.0f);
                temp = Instantiate(targetPrefab, pos, Quaternion.identity);
                temp.GetComponent<Target>().targetCow = this;
            }
        }
    }
}
