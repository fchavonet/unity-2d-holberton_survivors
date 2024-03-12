using UnityEngine;

public class Lightning : Weapon
{
    public static Lightning instance;

    [Space(10)]
    // Damager component reference.
    public EnemyDamager damager;
    // Projectile prefab reference.
    public ProjectileWeapon projectile;

    [Space(10)]
    // Layer mask for detecting enemies.
    public LayerMask whatIsEnemy;

    [Space(10)]
    // Range of the weapon.
    public float weaponRange;

    // Counter for timing attacks.
    private float shotCounter;

    public int lightningLevel;

    void Awake()
    {
        instance = this;
    }

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

                    // Instantiate projectile at target position.
                    Instantiate(projectile, targetPosition, Quaternion.identity).gameObject.SetActive(true);
                }

                // Play attack sound effect.
                SFXManager.instance.PlaySFXPitched(6);

                // Shake the camera.
                CameraShake.instance.ShakeIt(0.1f, 0.2f);
            }
        }
        
        lightningLevel = weaponLevel;
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
    }
}
