using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour, ITriggerListener
{
    [SerializeField] private float moveSpeed;
    private Vector2 velocity;

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
        float horizontalMoveDir = Input.GetAxisRaw("Horizontal");
        float verticalMoveDir = Input.GetAxisRaw("Vertical");

        velocity = Vector2.right * horizontalMoveDir + Vector2.up * verticalMoveDir;
        velocity.Normalize();
        controller.Move(velocity * moveSpeed * Time.deltaTime);
    }

    public void TriggerEnter(GameObject obj)
    {
        // TODO: if entering interactable, show UI instruction (e.g. "E - Read Letter")
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
        // TODO: if exiting interactable, hide UI instruction
    }
}
