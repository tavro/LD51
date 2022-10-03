using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crop : MonoBehaviour
{
    [SerializeField] private float pullAngleMargin = 45.0f;
    [SerializeField] private float pullDistance = 1.0f;

    bool isFollowingMouse;
    
    public bool IsPulled { get; private set; }
    public bool IsGone { get; private set; }

    [SerializeField] private new SpriteRenderer renderer;

    void Update()
    {
        if (!GameManager.Instance.IsPaused)
        {
            if (isFollowingMouse && !IsPulled) 
            {
                Vector2 mousePosGlobal = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePosRelative = mousePosGlobal - (Vector2)transform.position;
                float pullAngle = Vector2.SignedAngle(Vector2.up, mousePosRelative);
                transform.rotation = Quaternion.Euler(0, 0, pullAngle); // TODO: clamp to margins 
                // TODO: pull out further from the ground the higher up you pull (pullRaise or smth)
                // TODO: stick back in if outside of margins (pullRaiseAngleMargins or smth)

                if (Mathf.Abs(pullAngle) <= pullAngleMargin && mousePosRelative.magnitude >= pullDistance)
                {
                    GameManager.Instance.Inventory.cropAmount++;
                    PullOut();
                }

                if (Input.GetMouseButtonUp(0))
                    isFollowingMouse = false;
            }

            if (IsPulled)
            {
                renderer.sprite = null;
                IsGone = true; // TODO: animated before setting to true
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

    public void PullOut()
    {
        IsPulled = true;
    }
}
