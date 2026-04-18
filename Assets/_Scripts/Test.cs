using UnityEngine;

public class Test : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    private Vector3 targetPosition;
    private Vector3 originalScale; // Store the original scale

    void Start()
    {
        targetPosition = transform.position; // Initialize targetPosition
        originalScale = transform.localScale; // Store the original scale
    }

    void Update()
    {
        // Move towards the target position
        targetPosition.y = transform.position.y; // Keep the Y position constant
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check for mouse input
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePosition.z = 0; // Set Z to 0 since we are in 2D
            // Set the target position to the mouse position
            targetPosition = mousePosition;

            // Keep the Y position constant
            targetPosition.y = transform.position.y;

            // Flip the player sprite based on movement direction
            if (targetPosition.x > transform.position.x)
            {
                // Move right (facing right)
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            }
            else if (targetPosition.x < transform.position.x)
            {
                // Move left (facing left)
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
        }
    }
}
