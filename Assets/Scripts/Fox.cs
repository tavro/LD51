using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fox : MonoBehaviour
{
    [SerializeField] private float approachSpeed, fleeSpeed;
    [SerializeField] private Particle dustEffectPrefab;

    private GameObject target;
    private Vector2 startPos;
    private bool isApproaching;

    private Vector2 swipeStartPos, swipePrevPos;

    private new BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void Start() {
        target = GameObject.Find("Farm");
        startPos = transform.position;
        isApproaching = true;
    }

    void Update() {
            if (!GameManager.Instance.IsPaused)
        {
            if (isApproaching)
            {
                float step = approachSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
                if (Vector2.Distance(transform.position, target.transform.position) <= 0.01f)
                {
                    if (target.transform.childCount > 0)
                        Destroy(target.transform.GetChild(0).gameObject);
                    Destroy(gameObject);
                    SceneManager.LoadScene("FarmScene");
                }

                if (Input.GetMouseButtonDown(0))
                {
                    swipeStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                else if (Input.GetMouseButton(0))
                {
                    Vector2 swipeCurrPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (Vector2.Distance(swipeStartPos, swipeCurrPos) > 0.3f  && collider.OverlapPoint(swipeCurrPos))
                    {
                        isApproaching = false;
                        Particle dustParticle = Instantiate(dustEffectPrefab, transform.position, Quaternion.identity);
                        dustParticle.SetAngle(Vector2.SignedAngle(Vector2.right, swipeCurrPos - swipePrevPos));
                        Debug.Log(Vector2.SignedAngle(Vector2.right, swipeCurrPos - swipePrevPos));
                    }
                    swipePrevPos = swipeCurrPos;
                }
            }
            else
            {
                float step = fleeSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, startPos, step);
                if (Vector2.Distance(transform.position, startPos) <= 0.01f)
                    Destroy(gameObject);
            }
        }
    }

    // - Swipable if approaching crop
    // - 
}
