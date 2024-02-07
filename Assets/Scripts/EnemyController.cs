using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    [Space(10)]
    public Rigidbody2D rb;
    public SpriteRenderer sp;

    [Space(10)]
    public float health = 5f;
    public float moveSpeed = 2f;
    public float damage = 5f;

    [Space(10)]
    public float hitWaitTime = 0.5f;
    private float hitCounter;

    [Space(10)]
    public float knockBackTime = .5f;
    private float knockBackCounter;

    private Transform target;

    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }

            if (knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * 0.5f);
            }
        }

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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter <= 0f)
        {
            PlayerHealthController.instance.TakeDamage(damage);

            StartCoroutine(ResetHitAnimation());

            hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        DamageController.instance.SpawnDamage(damageToTake, transform.position);
    }

    public void TakeDamage(float damageToTake, bool shouldKnockBack)
    {
        TakeDamage(damageToTake);

        if (shouldKnockBack == true)
        {
            knockBackCounter = knockBackTime;
        }
    }
}
