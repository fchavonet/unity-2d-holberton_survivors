using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Space(10)]
    public Rigidbody2D rigidbody2d;
    public SpriteRenderer spriteRenderer;
    public GameObject deathEffect;
    private Transform target;

    [Space(10)]
    public float health = 10000;
    public float damage = 100;
    public float moveSpeed = 2f;

    [Space(10)]
    public float knockBackTime = 0.5f;
    public float hitWaitTime = 0.5f;
    private float knockBackCounter;
    private float hitCounter;

    [Space(10)]
    private Animator animator;
    public float timer;
    public float secondTimer;
    public float thirdTimer;
    private Vector3 playerLastPosition;


    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        target = FindObjectOfType<PlayerController>().transform;
        animator = GetComponentInChildren<Animator>();
        playerLastPosition = target.position;
        timer = 5f;
        secondTimer = 1f;
        thirdTimer = 2f;
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
        
        BehaviorBoss();
        FlipTowardsPlayer();
        
        if (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

        if (health <= 0)
        {
            LevelController.instance.IncrementEnemiesDefeated();
            Destroy(gameObject);
            Instantiate(deathEffect, transform.position,  Quaternion.identity);
        }
        DamageController.instance.SpawnDamage(damageToTake, transform.position);
    }

    public void TakeDamage(float damageToTake, bool shouldKnockBack)
    {
        TakeDamage(damageToTake);

        if (shouldKnockBack == true)
        {
            knockBackCounter = knockBackTime;
            hitCounter = hitWaitTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.TakeDamage(damage);

            hitCounter = hitWaitTime;
        }
    }

    private void FlipTowardsPlayer()
    {
        if (transform.position.x < target.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x > target.position.x)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void StopBoss()
    {
        rigidbody2d.velocity = Vector2.zero;
    }

    private void BehaviorBoss()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            StopBoss();
            animator.SetBool("isRunning", false);
            secondTimer -= Time.deltaTime;
            if (secondTimer <= 0)
            {
                animator.SetBool("isRunning", true);   
                Vector3 playerPosition = playerLastPosition;
                moveSpeed = 8f;
                Vector3 direction = playerPosition - transform.position;
                direction.Normalize();
                transform.position += direction * moveSpeed * Time.deltaTime;
                thirdTimer -= Time.deltaTime;
                if (thirdTimer <= 0)
                {
                    moveSpeed = 2f;
                    timer = 5f;
                    secondTimer = 1f;
                    thirdTimer = 2f;
                }
            }
        }
        else
        {
            if (playerLastPosition != target.position)
            {
                playerLastPosition = target.position;
            }
            rigidbody2d.velocity = (target.position - transform.position).normalized * moveSpeed;
        }
    }
}
