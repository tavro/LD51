using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    bool isFollowingMouse;

    void Update()
    {
        if(isFollowingMouse) {
            Vector2 mouse_pos = Input.mousePosition;
            Vector2 object_pos = Camera.main.WorldToScreenPoint(transform.position);
            mouse_pos.x = mouse_pos.x - object_pos.x;
            mouse_pos.y = mouse_pos.y - object_pos.y;
            float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90.0f));

            if(mouse_pos.y >= 200.0f) {
                GameManager.Instance.Inventory.cropAmount++;
                Destroy(gameObject);
            }
        }
    }

    void OnMouseOver() {
        if(Input.GetMouseButtonDown(0)) {
            isFollowingMouse = true;
        }
        else if(Input.GetMouseButtonUp(0)) {
            isFollowingMouse = false;
        }
    }
}
