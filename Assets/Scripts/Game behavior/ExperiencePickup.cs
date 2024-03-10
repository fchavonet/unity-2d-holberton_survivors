using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
    [Space(10)]
    // The experience value this pickup gives to the player
    public int experienValue;
    // The movement speed of the pickup towards the player
    public float moveSpeed;
    // Time between distance checks to the player
    public float timeBetweenChecks = .2f;
    // Duration before the pickup disappears if not collected
    public float lifeDuration = 60f;

    // Reference to the PlayerController script
    private PlayerController player;
    // Flag indicating if the pickup is moving towards the player
    private bool movingToPlayer;
    // Counter for time between distance checks
    private float checkCounter;
    // Timer for the pickup's life duration
    private float lifeTimer;

    void Start()
    {
        // Assign the PlayerController instance
        player = PlayerHealthController.instance.GetComponent<PlayerController>();
        // Initialize the life timer
        lifeTimer = lifeDuration;
    }

    void Update()
    {
        // Countdown the life timer
        lifeTimer -= Time.deltaTime;

        // Destroy the pickup if the life timer reaches zero
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }

        // Move the pickup towards the player if it's flagged to do so
        if (movingToPlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Countdown the distance check counter
            checkCounter -= Time.deltaTime;
            if (checkCounter <= 0)
            {
                checkCounter = timeBetweenChecks;

                // Check if the player is within pickup range
                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    // Start moving towards the player and increase speed with player's speed
                    movingToPlayer = true;
                    moveSpeed += player.speed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the pickup collides with the player
        if (collision.tag == "Player")
        {
            // Play pickup sound effect
            SFXManager.instance.PlaySFXPitched(1);

            // Give experience to the player
            LevelController.instance.GetExp(experienValue);

            // Destroy the pickup
            Destroy(gameObject);
        }
    }
}
