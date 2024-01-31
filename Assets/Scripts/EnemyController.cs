using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    [Space(10)]
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer sp;

    [Space(10)]
    public float moveSpeed = 2f;
    public float damage = 5f;
    public float hitWaitTime = 0.5f;

    private Transform target;
    private float hitCounter;

    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        rb.velocity = (target.position - transform.position).normalized * moveSpeed;
        FlipTowardsPlayer();

        if (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    private void FlipTowardsPlayer()
    {
        if (transform.position.x < target.position.x)
        {
            sp.flipX = false;
        }
        else if (transform.position.x > target.position.x)
        {
            sp.flipX = true;
        }
    }

    private IEnumerator ResetHitAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isHit", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter <= 0f)
        {
            PlayerHealthController.instance.TakeDamage(damage);
            
            animator.SetBool("isHit", true);
            StartCoroutine(ResetHitAnimation());

            hitCounter = hitWaitTime;
        }
    }
}
