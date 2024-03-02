using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Space(10)]
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public List<ParticleSystem> footParticles;

    [Space(10)]
    public float speed = 3f;
    public float speedMultiplier = 1.1f;
    public float pickupRange = 1.5f;
    public float pickupRangeMultiplier = 1.1f;

    public float playerDistance;

    // public Weapon acticeWeapon;

    public List<Weapon> unassignedWeapons, assignedWeapons, listWeapons;
    public int maxWeapons = 3;

    [HideInInspector]
    public List<Weapon> fullyLevelledWeapons = new List<Weapon>();

    Vector3 movement;

    public bool isChestClosed = true;

    public List<DialogueTrigger> dialogueTriggers = new List<DialogueTrigger>();

    /*
    private Rigidbody2D rigidbody2d;
    private Vector2 movement;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    */

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (assignedWeapons.Count == 0)
        {
            AddWeapon(Random.Range(0, unassignedWeapons.Count));
        }
    }

    private void OnMovement(InputValue value)
    {
        //movement = value.Get<Vector2>();

        Vector2 inputMovement = value.Get<Vector2>();
        movement = new Vector3(inputMovement.x, inputMovement.y, 0);
    }

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

    public void OnNextSentence()
    {
        DialogueManager.instance.DisplayNextSentence();
    }

    private void FixedUpdate()
    {
        //rigidbody2d.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        transform.position += movement * speed * Time.fixedDeltaTime;

        float distanceThisFrame = movement.magnitude * speed * Time.fixedDeltaTime;
        playerDistance += distanceThisFrame;

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

    public void SpeedLevelUp()
    {
        speed *= speedMultiplier;
    }

    public void PickupRangeLevelUp()
    {
        pickupRange *= pickupRangeMultiplier;
    }
}
