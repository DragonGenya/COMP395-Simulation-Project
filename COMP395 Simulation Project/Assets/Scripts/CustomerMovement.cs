using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public Transform targetPoint; // Assign in inspector
    public float moveDuration;

    public float timer; // Internal timer
    private bool isMoving = false;
    Vector3 startPos;
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
