using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody2D rb;
    public float speed = 3f;
    private SpriteRenderer sp;
    private Animator animator;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnMovement(InputValue value) {
        movement = value.Get<Vector2>();
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        Flip();
        Running();
    }

    private void Flip() {
        if (movement.x > 0) {
            sp.flipX = false;
        }
        else if (movement.x < 0) {
            sp.flipX = true;
        }
    }

    private void Running() {
        if(movement.magnitude > 0) {
            animator.SetBool("isRunning", true);
        }
        else {
            animator.SetBool("isRunning", false);
        }
    }
}
