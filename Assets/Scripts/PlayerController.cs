using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Space(10)]
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public List<ParticleSystem> footParticles;

    [Space(10)]
    public float speed = 3f;
    public float pickupRange = 1.5f;

    Vector3 movement;

    /*
    private Rigidbody2D rigidbody2d;
    private Vector2 movement;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    */

    private void OnMovement(InputValue value)
    {
        //movement = value.Get<Vector2>();

        Vector2 inputMovement = value.Get<Vector2>();
        movement = new Vector3(inputMovement.x, inputMovement.y, 0);
    }

    private void FixedUpdate()
    {
        //rigidbody2d.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        transform.position += movement * speed * Time.fixedDeltaTime;
        Flip();
        Running();
    }

    private void Flip()
    {
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void Running()
    {
        foreach (ParticleSystem particles in footParticles)
        {
            if (movement.magnitude > 0)
            {
                animator.SetBool("isRunning", true);

                if (!particles.isPlaying)
                {
                    particles.Play();
                }
            }
            else
            {
                animator.SetBool("isRunning", false);

                if (particles.isPlaying)
                {
                    particles.Stop();
                }
            }
        }
    }
}
