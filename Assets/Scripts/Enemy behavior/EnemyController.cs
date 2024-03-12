using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Space(10)]
    public Rigidbody2D rigidbody2d;
    public SpriteRenderer spriteRenderer;

    [Space(10)]
    // Enemies attributes for health, damage, and movement speed
    public float health = 5f;
    public float moveSpeed = 2f;
    public float damage = 5f;

    [Space(10)]
    // Time before the enemy can hit again
    public float hitWaitTime = 0.5f;
    // Duration of the knockback effect when hit
    public float knockBackTime = .5f;

    [Space(10)]
    // Amount of experience given to the player upon defeat
    public int experienceToGive = 1;
    // Probability of dropping a coin upon death
    public float coinDropRate = 0.5f;
    // Probability of dropping a chest upon death
    private float chestDropRate = 0.001f;

    // Timer to manage hit frequency
    private float hitCounter;
    // Timer to manage knockback duration
    private float knockBackCounter;
    // The target the enemy will pursue
    private Transform target;

    private bool isDefeated = false;

    void Start()
    {
        // Find the player in the scene and set it as the target
        target = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        // Handle knockback effect
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            // Reverse and increase move speed during knockback
            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }

            // Reset move speed once knockback duration is over
            if (knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * 0.5f);
            }
        }

        // Move towards the player
        rigidbody2d.velocity = (target.position - transform.position).normalized * moveSpeed;

        // Flip sprite depending on player's position
        FlipTowardsPlayer();

        // Reset hit counter after an attack
        if (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    private void FlipTowardsPlayer()
    {
        // Flip the sprite depending on the relative position of the target
        if (transform.position.x < target.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x > target.position.x)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Damage the player on collision
        if (collision.gameObject.tag == "Player" && hitCounter <= 0f)
        {
            PlayerHealthController.instance.TakeDamage(damage);

            // Reset hit counter to prevent immediate subsequent hits
            hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        // Si l'ennemi a déjà été vaincu, ne faites rien
        if (isDefeated)
            return;

        // Réduire la santé de l'ennemi
        health -= damageToTake;

        // Vérifier si la santé tombe en dessous de zéro et gérer la mort de l'ennemi
        if (health <= 0)
        {
            // Marquer l'ennemi comme vaincu
            isDefeated = true;

            // Supprimer l'ennemi du jeu
            Destroy(gameObject);

            // Incrémenter le compteur d'ennemis vaincus
            UIController.instance.IncrementEnemiesDefeated();

            // Déterminer si l'ennemi doit laisser tomber une pièce ou un coffre en fonction de la probabilité
            float random = Random.value;

            if (random <= coinDropRate && random > chestDropRate)
            {
                LevelController.instance.SpawnExp(transform.position, experienceToGive);
            }
            else if (random <= chestDropRate)
            {
                LevelController.instance.SpawnChest(transform.position);
            }
        }

        // Afficher visuellement l'effet de dégâts
        DamageController.instance.SpawnDamage(damageToTake, transform.position);
    }

    public void TakeDamage(float damageToTake, bool shouldKnockBack)
    {
        // Apply damage and check for knockback
        TakeDamage(damageToTake);

        if (shouldKnockBack == true)
        {
            knockBackCounter = knockBackTime;
        }
    }
}
