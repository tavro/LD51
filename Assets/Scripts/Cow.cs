using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    [SerializeField]
    GameObject targetPrefab;
    [SerializeField]
    float timeout;
    [SerializeField]
    private Vector2 minPos, maxPos;

    float timePassed;
    bool isReady;

    [SerializeField]
    GameObject spenePivot;
    bool isSpening;

    void ResetSpening() {
        isSpening = false;
        spenePivot.transform.localScale = new Vector2(1.0f, 1.0f);
    }

    void SetScale() {
        if(isReady) {
            SetMilkReadyScale();
        }
        else {
            SetOriginScale();
        }
    }

    void SetOriginScale() {
        if(transform.localScale.x != 1.0f) {
            transform.localScale = new Vector2(1.0f, 1.0f);
        }
    }

    void SetMilkReadyScale() {
        if(transform.localScale.x != 1.25f) {
            transform.localScale = new Vector2(1.25f, 1.25f);
        }
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

            SetScale();

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
                Vector2 pos = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y - 0.25f);
                temp = Instantiate(targetPrefab, pos, Quaternion.identity);
                temp.GetComponent<Target>().targetCow = this;
                isSpening = true;
            }
        }
    }
}
