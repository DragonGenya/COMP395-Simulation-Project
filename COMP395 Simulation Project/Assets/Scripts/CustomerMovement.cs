using System;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    [Tooltip("How much time the customer will take to get to a node from a starting point. ")]
    [SerializeField]
    private float moveDuration;
    private Transform targetPoint;
    private float timer; // Internal timer
    private bool isMoving = false;
    private Vector3 startPos;

    private Action onEndMovementAction;
    void StartMovement()
    {
        startPos = transform.position;
        timer = 0f;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving && targetPoint != null)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / moveDuration); // Normalize time (0 to 1)
            transform.position = Vector3.Lerp(startPos, targetPoint.position, t);

            if (t >= 1f)
            {
                isMoving = false; // Stop movement when finished
                onEndMovementAction?.Invoke();
            }
        }
    }

    public void SetNewDestination(Transform newTarget, Action onArrival = null)
    {
        targetPoint = newTarget;
        onEndMovementAction = onArrival;
        StartMovement();
    }
}
