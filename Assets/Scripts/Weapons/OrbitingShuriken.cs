using UnityEngine;

public class OrbitingShuriken : Weapon
{
    [Space(10)]
    // Reference to the enemy damager.
    public EnemyDamager damager;
    // Shuriken prefab to spawn.
    public Transform projectile;
    // Holder object for orbiting.
    public Transform holder;

    [Space(10)]
    // Speed of rotation.
    public float rotateSpeed = 180f;
    // Time between shuriken spawns.
    public float timeBetweenSpawn = 5f;

    // Counter for spawn timing.
    private float spawnCounter;

    void Start()
    {
        // Initialize weapon stats.
        SetStats();
    }

    void Update()
    {
        // Rotate the holder object.
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * stats[weaponLevel].speed));

        // Countdown for shuriken spawning.
        spawnCounter -= Time.deltaTime;

        // Spawn shurikens if counter reaches zero.
        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;

            // Instantiate shurikens around the holder.
            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                float rot = (360f / stats[weaponLevel].amount) * i;

                // Instantiate and activate shuriken.
                Instantiate(projectile, projectile.position, Quaternion.Euler(0f, 0f, rot), holder).gameObject.SetActive(true);
            }
        }

        // Update stats if they have been modified.
        if (statsUpdated == true)
        {
            statsUpdated = false;

            // Update the weapon stats.
            SetStats();
        }
    }

    // Update weapon stats.
    public void SetStats()
    {
        // Set damager stats.
        damager.damageAmount = stats[weaponLevel].damage;
        // Set scale for shuriken range.
        transform.localScale = Vector3.one * stats[weaponLevel].range;
        // Set time between shuriken spawns.
        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;
        // Set shuriken lifespan.
        damager.lifeTime = stats[weaponLevel].duration;
    }
}
