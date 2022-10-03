using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fox : MonoBehaviour
{
    [SerializeField] private float approachSpeed, fleeSpeed;
    [SerializeField] private Particle dustEffectPrefab;

    private Crop targetCrop;
    private Vector2 startPos;
    private bool isApproaching;

    private Vector2 swipeStartPos, swipePrevPos;

    private new BoxCollider2D collider;
    private CropManager cropManager;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        cropManager = GameObject.FindWithTag("CropManager").GetComponent<CropManager>();
    }

    void Start() {
        targetCrop = cropManager.GetRandomCrop();
        startPos = transform.position;
        isApproaching = true;
        FindObjectOfType<AudioManager>().PlaySound("Attack");
    }

    void Update() {
        if (GameManager.Instance.CurrPauseState == GameManager.PauseState.NONE)
        {
            if (isApproaching)
            {
                // TODO: if crop IsGone or IsPulled, try getting new target

                float step = approachSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetCrop.transform.position, step);
                if (Vector2.Distance(transform.position, targetCrop.transform.position) <= 0.01f)
                {
                    targetCrop.PullOut();
                    Destroy(gameObject);
                    FindObjectOfType<AudioManager>().PlaySound("Happy");
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
                        FindObjectOfType<AudioManager>().PlaySound("Sad");
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
}
