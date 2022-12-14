using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour, ITriggerListener
{
    [SerializeField] private float moveSpeed;
    private Vector2 velocity;

    [SerializeField] private TextMeshProUGUI interactionTextUI;

    [SerializeField] private Animator animator;
    private Controller2D controller;

    private GameManager gm;
    private Coroutine walkSound;
    private Coroutine gruntSound;

    private void Awake()
    {
        controller = GetComponent<Controller2D>();
        gm = FindObjectOfType<GameManager>();
        if (gm is not null)
        {
            gameObject.transform.position = gm.LastPlayerPos;
        }
    }

    private void Start()
    {
        GameManager.Instance.PlaceBoughtBuildings();
        controller.SetTriggerListener(this);
        gruntSound ??= StartCoroutine(MakeSound.SoundPlayer("Grunt", 3f, 6f, GameManager.Instance));
    }

    private void Update()
    {
        if (GameManager.Instance.CurrPauseState == GameManager.PauseState.NONE)
        {
            float horizontalMoveDir = Input.GetAxisRaw("Horizontal");
            float verticalMoveDir = Input.GetAxisRaw("Vertical");

            if (horizontalMoveDir == 0 && verticalMoveDir != 0) // Moving horizontally only
            {
                animator.SetFloat("horizontalDirection", 0);
                animator.SetFloat("verticalDirection", verticalMoveDir);
            }
            else if (horizontalMoveDir != 0 && verticalMoveDir == 0) // Moving vertically only
            {
                animator.SetFloat("horizontalDirection", horizontalMoveDir);
                animator.SetFloat("verticalDirection", 0);
            }
            else if (velocity == Vector2.zero && horizontalMoveDir != 0 && verticalMoveDir != 0) // Starting moving diagonally
            {
                animator.SetFloat("horizontalDirection", horizontalMoveDir);
                animator.SetFloat("verticalDirection", 0);
            }

            velocity = Vector2.right * horizontalMoveDir + Vector2.up * verticalMoveDir;
            velocity.Normalize();
            
            if(horizontalMoveDir != 0 || verticalMoveDir != 0)
            {
                walkSound ??= StartCoroutine(MakeSound.SoundPlayer("Step", 0.5f, 0.5f, GameManager.Instance));
            }
            else
            {
                if (walkSound != null)
                { 
                    StopCoroutine(walkSound);
                    walkSound = null;
                }
            }
        }
        else
        {
            velocity = Vector2.zero;
        }

        controller.Move(velocity * moveSpeed * Time.deltaTime);

        animator.SetFloat("velocity", velocity.magnitude);
        animator.SetBool("isWalking", velocity != Vector2.zero);
    }

    public void TriggerEnter(GameObject obj)
    {

    }

    public void TriggerStay(GameObject obj)
    {
        if (obj.tag == "Interactable")
        {
            IInteractable interactable = obj.GetComponent<IInteractable>();

            if (Input.GetButtonDown("Interact"))
                interactable.OnInteraction();

            interactionTextUI.gameObject.SetActive(true);
            interactionTextUI.SetText($"E - {interactable.GetInteractionDesc()}");
        }
    }

    public void TriggerExit(GameObject obj)
    {
        if (!obj || obj.tag == "Interactable")
        {
            interactionTextUI.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (gm is not null)
        {
            gm.LastPlayerPos = gameObject.transform.position;
        }
        if(gruntSound != null)
        {
            StopCoroutine(gruntSound);
            gruntSound = null;
        }
    }
}
