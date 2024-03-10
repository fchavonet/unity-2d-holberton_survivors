using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexCoin : MonoBehaviour
{
    // Reference to the PlayerController script
    private PlayerController player;
    // Original pickup range of the player
    private float originalPickupRange;
    // Reference to the Animator component
    private Animator animator;

    // Increase in pickup range when the player interacts with the VortexCoin
    public float increasePickupRange = 120f;
    // Duration of the effect
    public float effectDuration = 5f;

    void Start()
    {
        // Assign the PlayerController instance
        player = PlayerController.instance;
        // Store the original pickup range of the player
        originalPickupRange = player.pickupRange;
        // Get the Animator component of the VortexCoin
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collides with the VortexCoin and the chest is closed
        if (collision.CompareTag("Player") && PlayerController.instance.isChestClosed)
        {
            // Open the chest and increase the player's pickup range
            PlayerController.instance.isChestClosed = false;
            player.pickupRange = increasePickupRange;

            // Trigger animation if animator is available
            if (animator != null)
            {
            animator.SetBool("IsTouched", true);
            }

            // Reset pickup range and destroy the VortexCoin after a delay
            StartCoroutine(ResetPickRangeAfterDelay());
        }
    }

    // Coroutine to reset the pickup range and destroy the VortexCoin
    private IEnumerator ResetPickRangeAfterDelay()
    {
        // Wait for the effect duration
        yield return new WaitForSeconds(effectDuration);

        // Reset the player's pickup range, destroy the VortexCoin, and reset chest status
        player.pickupRange = originalPickupRange;
        Destroy(gameObject);
        PlayerController.instance.isChestClosed = true;
        PlayerController.instance.isChestSpawned = false;
    }
}
