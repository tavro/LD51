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
    Vector2 startPos;

    [SerializeField]
    GameObject spenePivot;
    bool isSpening;

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
    
    void ResetSpening() {
        isSpening = false;
        spenePivot.transform.localScale = new Vector2(1.0f, 1.0f);
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
                if(Input.GetMouseButtonUp(0)) {
                    ResetSpening();
                }
            }
            else {
                if(Input.GetMouseButtonUp(0)) {
                    if(temp) {
                        Destroy(temp);
                    }
                    isReady = false;
                    ResetSpening();
                }
            }

            if(isSpening) {
                Vector2 mouse_pos = Input.mousePosition;
                Vector2 object_pos = Camera.main.WorldToScreenPoint(spenePivot.transform.position);
                mouse_pos.x = mouse_pos.x - object_pos.x;
                mouse_pos.y = mouse_pos.y - object_pos.y;
                float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
                spenePivot.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90.0f));

                Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float distance = Vector2.Distance(spenePivot.transform.position, currentMousePos);
                spenePivot.transform.localScale = new Vector2(1.0f, distance*10.0f);
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
                isSpening = true;
            }
        }
    }
}
