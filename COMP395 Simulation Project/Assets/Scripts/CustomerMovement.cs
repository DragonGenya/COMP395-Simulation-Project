using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public GameObject startPoint; // Assign in inspector
    public GameObject targetPoint; // Assign in inspector
    public float moveDuration = 3f; // Time it takes to reach target

    private float timer; // Internal timer
    private bool isMoving = false;

    void Start()
    {
        // Optional: Automatically start moving on play
        StartMovement();
    }

    public void StartMovement()
    {
        timer = 0f;
        transform.position = startPoint.transform.position;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving && startPoint != null && targetPoint != null)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / moveDuration); // Normalize time (0 to 1)
            transform.position = Vector3.Lerp(startPoint.transform.position, targetPoint.transform.position, t);

            if (t >= 1f)
            {
                isMoving = false; // Stop movement when finished
            }
        }
    }

    public void SetNewDestination(GameObject newTarget)
    {
        targetPoint = newTarget;
        StartMovement();
    }
}
