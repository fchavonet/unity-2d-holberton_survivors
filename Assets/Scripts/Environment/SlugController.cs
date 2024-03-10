using System.Collections;
using UnityEngine;

public class Slug : MonoBehaviour
{
    [Space(10)]
    // Reference to the SpriteRenderer component.
    public SpriteRenderer spriteRenderer;

    [Space(10)]
    // Speed of movement.
    public float movementSpeed = 0.1f;
    // Width of movement area.
    public float movementWidth = 4f;
    // Height of movement area.
    public float movementHeight = 4f;

    // Initial position of the slug.
    private Vector2 initialPosition;
    // Flag to control slug movement.
    private bool isMoving = true;

    private void Start()
    {
        // Store initial position.
        initialPosition = transform.position;
        // Start moving randomly.
        MoveRandomly();
    }

    private void Update()
    {
        // Check if the dialogue is open and stop the slug if it is
        if (DialogueManager.instance.isDialogueOpen)
        {
            // Stop the slug movement.
            StopMoving();
        }
        else
        {
            // If dialogue is closed and the slug is not moving, resume movement
            if (!isMoving)
            {
                // Set the moving flag to true.
                isMoving = true;
                // Start moving randomly again.
                MoveRandomly();
            }
        }
    }

    private void MoveRandomly()
    {
        // Return early if the slug is not supposed to move.
        if (!isMoving)
            return;

        // Calculate new random position within movement area.
        float newPosX = initialPosition.x + Random.Range(-movementWidth / 2f, movementWidth / 2f);
        float newPosY = initialPosition.y + Random.Range(-movementHeight / 2f, movementHeight / 2f);
        Vector2 newPosition = new Vector2(newPosX, newPosY);

        // Flip the sprite if necessary.
        spriteRenderer.flipX = (newPosition.x < transform.position.x);

        // Move to the new position.
        StartCoroutine(MoveToPosition(newPosition));
    }

    // Coroutine to move the slug to a specific position.
    private IEnumerator MoveToPosition(Vector2 destination)
    {
        // Move the slug until it reaches the destination.
        while ((Vector2)transform.position != destination)
        {
            Vector2 movement = Vector2.ClampMagnitude(destination - (Vector2)transform.position, movementSpeed * Time.deltaTime);
            transform.position += (Vector3)movement;
            yield return null;
        }

        // Wait for a brief moment.
        yield return new WaitForSeconds(1f);

        // Move randomly again.
        MoveRandomly();
    }

    private void StopMoving()
    {
        // Stop all movement coroutines.
        StopAllCoroutines();
        // Set the moving flag to false.
        isMoving = false;
    }
}
