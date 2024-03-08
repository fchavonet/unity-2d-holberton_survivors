using UnityEngine;

public class DangerZone : Weapon
{
    [Space(10)]
    // Damager component reference.
    public EnemyDamager damager;

    // Time between spawning damagers.
    private float spawnTime;
    // Counter for spawning damagers.
    private float spawnCounter;

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

        // Countdown for spawning.
        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0f)
        {
            // Reset spawn timer.
            spawnCounter = spawnTime;
            // Spawn damager.
            Instantiate(damager, damager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);
        }
    }

    // Update weapon stats.
    void SetStats()
    {
        // Set damager stats.
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;
        damager.timeBetweenDamage = stats[weaponLevel].speed;
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        // Set spawn time.
        spawnTime = stats[weaponLevel].timeBetweenAttacks;
    }
}
