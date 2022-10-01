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

	private Controller2D controller;

    private void Awake()
    {
        controller = GetComponent<Controller2D>();
    }

    private void Start()
    {
        controller.SetTriggerListener(this);
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPaused)
        {
            float horizontalMoveDir = Input.GetAxisRaw("Horizontal");
            float verticalMoveDir = Input.GetAxisRaw("Vertical");

            velocity = Vector2.right * horizontalMoveDir + Vector2.up * verticalMoveDir;
            velocity.Normalize();
            controller.Move(velocity * moveSpeed * Time.deltaTime);
        }
    }

    public void TriggerEnter(GameObject obj)
    {
        if (obj.tag == "Interactable")
        {
            IInteractable interactable = obj.GetComponent<IInteractable>();
            interactionTextUI.gameObject.SetActive(true);
            interactionTextUI.SetText($"E - {interactable.GetInteractionDesc()}"); // TODO: Get interaction description instead.
        }
    }

    public void TriggerStay(GameObject obj)
    {
        if (obj.tag == "Interactable" && Input.GetButtonDown("Interact"))
        {
            IInteractable interactable = obj.GetComponent<IInteractable>();
            interactable.OnInteraction();
        }
    }

    public void TriggerExit(GameObject obj)
    {
        if (obj.tag == "Interactable")
        {
            interactionTextUI.gameObject.SetActive(false);
        }
    }
}
