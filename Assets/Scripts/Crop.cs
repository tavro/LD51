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
    [SerializeField] Texture2D handyDandy;
    [SerializeField] Texture2D broomBroom;

    CursorMode cursorMode = CursorMode.ForceSoftware;
    Vector2 handyHotSpot = Vector2.zero;
    Vector2 broomHotSpot = Vector2.zero;

    bool isFollowingMouse;


    public bool IsPulled { get; private set; }
    public bool IsGone { get; private set; }

    private void Start()
    {
        visualOrigPos = renderer.transform.position;
        handyHotSpot = new Vector2(handyDandy.width / 2, 0);
        broomHotSpot = new Vector2(broomBroom.width / 2, 0);
    }

    void Update()
    {
        if (GameManager.Instance.CurrPauseState == GameManager.PauseState.NONE)
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
                {
                    Cursor.SetCursor(broomBroom, broomHotSpot, cursorMode);
                    isFollowingMouse = false;
                }
                    
            }

            if (IsPulled)
            {
                renderer.sprite = null;
                IsGone = true; // TODO: animated before setting to true
            }
        }
    }

    void OnMouseOver()
    {
        if (GameManager.Instance.CurrPauseState == GameManager.PauseState.NONE)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isFollowingMouse = true;
            }
            if(!IsPulled)
            {
                Cursor.SetCursor(handyDandy, handyHotSpot, cursorMode);
            }
        }
    }
    private void OnMouseExit()
    {
        if (!isFollowingMouse)
        {
            Cursor.SetCursor(broomBroom, broomHotSpot, cursorMode);
        }
    }



    public void PullOut()
    {
        IsPulled = true;
        Cursor.SetCursor(broomBroom, broomHotSpot, cursorMode);
    }
}





