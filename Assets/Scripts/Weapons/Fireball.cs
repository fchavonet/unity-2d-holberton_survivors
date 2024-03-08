using UnityEngine;

public class Fireball : Weapon
{
    [Space(10)]
    // Damager component reference.
    public EnemyDamager damager;
    // Projectile prefab reference.
    public Projectile projectile;

    [Space(10)]
    // Layer mask for detecting enemies.
    public LayerMask whatIsEnemy;

    [Space(10)]
    // Range of the weapon.
    public float weaponRange;

    // Counter for timing attacks.
    private float shotCounter;

    void Start()
    {
        // Initialize weapon stats.
        SetStats();
    }

    void Update()
    {
        // Check if stats need updating.
        if (statsUpdated == true)
        {
            statsUpdated = false;
            // Update stats.
            SetStats();
        }

        // Countdown for attacks.
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0)
        {
            // Reset attack timer.
            shotCounter = stats[weaponLevel].timeBetweenAttacks;

            // Detect enemies within range.
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLevel].range, whatIsEnemy);
            if (enemies.Length > 0)
            {
                // Attack each enemy within range.
                for (int i = 0; i < stats[weaponLevel].amount; i++)
                {
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;

                    // Calculate projectile direction.
                    Vector3 direction = targetPosition - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angle -= 90;

                    // Set projectile rotation and instantiate.
                    projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    Instantiate(projectile, projectile.transform.position, projectile.transform.rotation).gameObject.SetActive(true);
                }

                // Play attack sound effect.
                SFXManager.instance.PlaySFXPitched(5);
            }
        }
    }

    // Update weapon stats.
    void SetStats()
    {
        // Set damager stats.
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        // Reset attack counter.
        shotCounter = 0f;
        // Set projectile speed.
        projectile.moveSpeed = stats[weaponLevel].speed;
    }
}
