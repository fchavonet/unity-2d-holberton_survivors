using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Space(10)]
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public ParticleSystem rightFootParticlesDust;
    public ParticleSystem leftFootParticlesDust;

    [Space(10)]
    public float speed = 3f;

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
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
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0) {
            spriteRenderer.flipX = true;
        }
    }

    private void ManageFootParticles(bool play)
{
    ParticleSystem[] footParticleSystems = { rightFootParticlesDust, leftFootParticlesDust };

    foreach (ParticleSystem footParticleSystem in footParticleSystems)
    {
        if (play)
        {
            if (!footParticleSystem.isPlaying)
            {
                footParticleSystem.Play();
            }
        }
        else
        {
            if (footParticleSystem.isPlaying)
            {
                footParticleSystem.Stop();
            }
        }
    }
}

    private void Running() {
        if(movement.magnitude > 0) {
            animator.SetBool("isRunning", true);
            ManageFootParticles(true);
        }
        else {
            animator.SetBool("isRunning", false);
            ManageFootParticles(false);
        }
    }
}
