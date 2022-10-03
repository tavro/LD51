using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crop : MonoBehaviour
{
    [SerializeField] private float pullAngleMargin = 45.0f;
    [SerializeField] private float distToPullOut = 2.5f;

    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private float visualPullDist = 0.1f;
    private Vector2 visualOrigPos;

    bool isFollowingMouse;
    
    public bool IsPulled { get; private set; }
    public bool IsGone { get; private set; }

    private void Start()
    {
        visualOrigPos = renderer.transform.position;
    }

    void Update()
    {
        if (!GameManager.Instance.IsPaused)
        {
            if (isFollowingMouse && !IsPulled) 
            {
                Vector2 mousePosGlobal = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePosRelative = mousePosGlobal - (Vector2)transform.position;

                float pullAngle = Vector2.SignedAngle(Vector2.up, mousePosRelative);
                renderer.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(pullAngle, -pullAngleMargin, pullAngleMargin)); 

                float pullDist = mousePosRelative.magnitude;
                renderer.transform.position = Vector2.Lerp(visualOrigPos, visualOrigPos + (Vector2)renderer.transform.up * visualPullDist, pullDist / distToPullOut);

                if (Mathf.Abs(pullAngle) <= pullAngleMargin && pullDist >= distToPullOut)
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
