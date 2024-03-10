using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Singleton instance.
    public static PlayerController instance;

    [Space(10)]
    // Animator component.
    public Animator animator;
    // SpriteRenderer component.
    public SpriteRenderer spriteRenderer;
    // List of footstep particles.
    public List<ParticleSystem> footParticles;

    [Space(10)]
    // Player movement speed.
    public float speed = 3f;
    // Speed multiplier for leveling up.
    public float speedMultiplier = 1.1f;

    [Space(10)]
    // Pickup range for weapons.
    public float pickupRange = 1.5f;
    // Pickup range multiplier for leveling up.
    public float pickupRangeMultiplier = 1.1f;

    [Space(10)]
    // Distance traveled by the player.
    public float playerDistance;

    [Space(10)]
    // Maximum number of weapons.
    public int maxWeapons = 3;
    // List of unassigned weapons.
    public List<Weapon> unassignedWeapons;
    // List of assigned weapons.
    public List<Weapon> assignedWeapons;
    // List of all weapons.
    public List<Weapon> listWeapons;

    [HideInInspector]
    // Fully levelled weapons.
    public List<Weapon> fullyLevelledWeapons = new List<Weapon>();

    [HideInInspector]
    // Flag for chest state.
    public bool isChestClosed = true;
    [HideInInspector]
    // Flag for spawned chest.
    public bool isChestSpawned = false;

    [HideInInspector]
    // List of dialogue triggers.
    public List<DialogueTrigger> dialogueTriggers = new List<DialogueTrigger>();

    // Movement vector.
    Vector3 movement;

    private void Awake()
    {
        // Set instance as this.
        instance = this;
    }

    void Start()
    {
        // Add a weapon if no weapons are assigned.
        if (assignedWeapons.Count == 0)
        {
            AddWeapon(Random.Range(0, unassignedWeapons.Count));
        }
    }

    // Movement input handler.
    private void OnMovement(InputValue value)
    {
        Vector2 inputMovement = value.Get<Vector2>();
        movement = new Vector3(inputMovement.x, inputMovement.y, 0);
    }

    // Open dialogue handler.
    public void OnOpenDialogue()
    {
        foreach (DialogueTrigger trigger in dialogueTriggers)
        {
            if (trigger.isInRange && !DialogueManager.instance.isDialogueOpen)
            {
                trigger.TriggerDialogue();
            }
        }
    }

    // Next sentence handler.
    public void OnNextSentence()
    {
        DialogueManager.instance.DisplayNextSentence();
    }

    // Fixed update for movement.
    private void FixedUpdate()
    {
        transform.position += movement * speed * Time.fixedDeltaTime;

        float distanceThisFrame = movement.magnitude * speed * Time.fixedDeltaTime;
        playerDistance += distanceThisFrame;

        // Flip player sprite.
        Flip();
        // Handle running animation and particles.
        Running();
    }

    // Flip player sprite based on movement direction.
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

    // Handle running animation and particles.
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
                    SFXManager.instance.PlaySFX(0);
                }
            }
            else
            {
                animator.SetBool("isRunning", false);

                if (particles.isPlaying)
                {
                    particles.Stop();
                    SFXManager.instance.StopSFX(0);
                }
            }
        }
    }

    // Add a weapon to the player's inventory by index.
    public void AddWeapon(int weaponNumber)
    {
        if (weaponNumber < unassignedWeapons.Count)
        {
            if (unassignedWeapons[weaponNumber].tag == "PlayerUpdate")
            {
                for (int i = 0; i < unassignedWeapons.Count; i++)
                {
                    if (i != weaponNumber && unassignedWeapons[i].tag != "PlayerUpdate")
                    {
                        weaponNumber = i;
                        break;
                    }
                }
            }

            assignedWeapons.Add(unassignedWeapons[weaponNumber]);
            listWeapons.Add(unassignedWeapons[weaponNumber]);

            unassignedWeapons[weaponNumber].gameObject.SetActive(true);

            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }

    // Add a weapon to the player's inventory directly.
    public void AddWeapon(Weapon weaponToAdd)
    {
        weaponToAdd.gameObject.SetActive(true);

        assignedWeapons.Add(weaponToAdd);

        if (weaponToAdd.tag != "PlayerUpdate")
        {
            listWeapons.Add(weaponToAdd);
        }

        unassignedWeapons.Remove(weaponToAdd);
    }

    // Level up speed.
    public void SpeedLevelUp()
    {
        speed *= speedMultiplier;
    }

    // Level up pickup range.
    public void PickupRangeLevelUp()
    {
        pickupRange *= pickupRangeMultiplier;
    }
}
