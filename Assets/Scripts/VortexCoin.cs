using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexCoin : MonoBehaviour
{
    private PlayerController player;
    private float originalPickupRange;
    private Animator animator;
    public float increasePickupRange = 120f;
    public float effectDuration = 5f;

    void Start()
    {
        player = PlayerController.instance;
        originalPickupRange = player.pickupRange; 
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerController.instance.isChestClosed)
        {
            PlayerController.instance.isChestClosed = false;
            player.pickupRange = increasePickupRange;
            if (animator != null)
            {
            animator.SetBool("IsTouched", true);
            }
            StartCoroutine(ResetPickRangeAfterDelay());
        }
    }

    private IEnumerator ResetPickRangeAfterDelay()
    {
        yield return new WaitForSeconds(effectDuration);

        player.pickupRange = originalPickupRange;
        Destroy(gameObject);
        PlayerController.instance.isChestClosed = true;
    }
}
