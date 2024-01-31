using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public float moveSpeed;
    private Transform target;

    void Start() 
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        rb.velocity = (target.position - transform.position).normalized * moveSpeed;
        FlipTowardsPlayer();
    }

    private void FlipTowardsPlayer()
    {
        if (transform.position.x < target.position.x)
        {
            sp.flipX = false;
        }
        else if(transform.position.x > target.position.x)
        {
            sp.flipX = true;
        }
    }
}
