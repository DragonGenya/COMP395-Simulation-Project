using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public Transform targetPoint; // Assign in inspector
    public float moveDuration;
    public Transform[] queuePositions; // Assign queue spots in Inspector GD
    private int queueIndex = -1; // Each customer gets a unique spot GD
    public float timer; // Internal timer
    private bool isMoving = false;
    private Vector3 startPos;

    public void SetQueuePosition(int index)// GD code
    {
        if (index < queuePositions.Length)
        {
            queueIndex = index;
            targetPoint = queuePositions[queueIndex];
            StartMovement();
        }
    }
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
            }
        }
    }

    public void SetNewDestination(Transform newTarget)
    {
        targetPoint = newTarget;
        StartMovement();
    }
}
