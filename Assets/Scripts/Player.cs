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

    private void Awake()
    {
        controller = GetComponent<Controller2D>();
    }

    private void Start()
    {
        GameManager.Instance.PlaceBoughtBuildings();
        controller.SetTriggerListener(this);
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPaused)
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
            controller.Move(velocity * moveSpeed * Time.deltaTime);
        
            animator.SetFloat("velocity", velocity.magnitude);
            animator.SetBool("isWalking", velocity != Vector2.zero);
        }
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
}
