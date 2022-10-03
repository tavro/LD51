using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crop : MonoBehaviour
{
    [SerializeField] private float pullAngleMargin = 45.0f;
    [SerializeField] private float pullDistance = 1.0f;

    bool isFollowingMouse;

    void Update()
    {
        if (!GameManager.Instance.IsPaused)
        {
            if (isFollowingMouse) 
            {
                Vector2 mousePosGlobal = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePosRelative = mousePosGlobal - (Vector2)transform.position;
                float pullAngle = Vector2.SignedAngle(Vector2.up, mousePosRelative);
                transform.rotation = Quaternion.Euler(0, 0, pullAngle); // TODO: clamp to margins 
                // TODO: pull out further from the ground the higher up you pull (pullRaise or smth)
                // TODO: stick back in if outside of margins (pullRaiseAngleMargins or smth)

                if (Mathf.Abs(pullAngle) <= pullAngleMargin && mousePosRelative.magnitude >= pullDistance)
                {
                    GameManager.Instance.Inventory.cropAmount++; // TODO: make differ if days past is greater (maybe via more crops)
                    SceneManager.LoadScene("FarmScene"); // TODO: take care of in a CropHandler instead
                    Destroy(gameObject); // TODO: maybe just make invisible and marked as pulled instead
                }

                if (Input.GetMouseButtonUp(0))
                    isFollowingMouse = false;
            }
        }
    }

    void OnMouseOver() {
        if (!GameManager.Instance.IsPaused)
        {
            if (Input.GetMouseButtonDown(0))
                isFollowingMouse = true;
        }
    }
}
