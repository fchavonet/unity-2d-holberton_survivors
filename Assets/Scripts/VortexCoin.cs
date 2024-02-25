using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexCoin : MonoBehaviour
{
    private PlayerController player;
    private float originalPickupRange;
    private bool hasTrigerredEffect;
    private Animator animator;
    private bool isEffectActive = false;

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
        if (collision.CompareTag("Player") && !hasTrigerredEffect)
        {
            hasTrigerredEffect = true;
            player.pickupRange = increasePickupRange;
            isEffectActive = true;
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
        isEffectActive = false;
        Destroy(gameObject);
    }
}
