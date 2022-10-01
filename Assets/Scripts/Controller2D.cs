using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    private const float SKIN_WIDTH = 0.01f;

    [SerializeField] private LayerMask triggerMask;
    private List<GameObject> prevTriggerObjects, currTriggerObjects;
    private ITriggerListener triggerListener;

    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float maxRayInterval;
    private float horizontalRayInterval, verticalRayInterval;
    private int horizontalRayCount, verticalRayCount;

    private RayOrigins rayOrigins;
    private new BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        prevTriggerObjects = new List<GameObject>();
        currTriggerObjects = new List<GameObject>();
    }

    private void Start()
    {
        CalculateRayIntervals();
    }

    public void SetTriggerListener(ITriggerListener listener)
    {
        triggerListener = listener;
    }

    private void CalculateRayIntervals()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(-2 * SKIN_WIDTH);

        horizontalRayCount = Mathf.CeilToInt(bounds.size.y / maxRayInterval) + 1;
        verticalRayCount = Mathf.CeilToInt(bounds.size.x / maxRayInterval) + 1;

        horizontalRayInterval = bounds.size.y / (horizontalRayCount - 1);
        verticalRayInterval = bounds.size.x / (verticalRayCount - 1);
    }

    private void CalculateRayOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(-2 * SKIN_WIDTH);

        rayOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        rayOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        rayOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        rayOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
    }

    public void Move(Vector2 velocity)
    {
        CalculateRayOrigins();

        // Calculate collisions
        if (velocity.x != 0)
            HorizontalCollisions(ref velocity);
        if (velocity.y != 0)
            VerticalCollisions(ref velocity);

        // Calculate triggers
        CalculateTriggers();

        // Handle collisions
        transform.Translate(velocity);

        // Handle triggers
        if (triggerListener != null)
        {
            foreach (GameObject obj in currTriggerObjects)
            {
                if (prevTriggerObjects.Contains(obj))
                    triggerListener.TriggerStay(obj);
                else
                    triggerListener.TriggerEnter(obj);
            }

            foreach (GameObject obj in prevTriggerObjects)
            {
                if (!currTriggerObjects.Contains(obj))
                    triggerListener.TriggerExit(obj);
            }
        }
    }

    private void HorizontalCollisions(ref Vector2 velocity)
    {
        float rayDistance = Mathf.Abs(velocity.x) + SKIN_WIDTH;
        float rayDirection = Mathf.Sign(velocity.x);

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (rayDirection == 1) ? rayOrigins.bottomRight : rayOrigins.bottomLeft;
            rayOrigin += Vector2.up * horizontalRayInterval * i;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * rayDirection, rayDistance, collisionMask);

            if (hit)
            {
                velocity.x = rayDirection * (hit.distance - SKIN_WIDTH);
                rayDistance = hit.distance;
            }
        }
    }

    private void VerticalCollisions(ref Vector2 velocity)
    {
        float rayDistance = Mathf.Abs(velocity.y) + SKIN_WIDTH;
        float rayDirection = Mathf.Sign(velocity.y);

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (rayDirection == 1) ? rayOrigins.topLeft : rayOrigins.bottomLeft;
            rayOrigin += Vector2.right * verticalRayInterval* i;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * rayDirection, rayDistance, collisionMask);

            if (hit)
            {
                velocity.y = rayDirection * (hit.distance - SKIN_WIDTH);
                rayDistance = hit.distance;
            }
        }
    }

    private void CalculateTriggers()
    {
        prevTriggerObjects = new List<GameObject>(currTriggerObjects);
        currTriggerObjects.Clear();

        Bounds bounds = collider.bounds;
        Collider2D[] hits = Physics2D.OverlapBoxAll(bounds.center, bounds.size, 0, triggerMask);
        foreach (Collider2D hit in hits)
            currTriggerObjects.Add(hit.gameObject);
    }

    private struct RayOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}
