using UnityEngine;

public class DoubleAxe : Weapon
{
    public static DoubleAxe instance;

    [Space(10)]
    // Damager component reference.
    public EnemyDamager damager;

    // Counter for throwing attacks.
    private float throwCounter;

    public int doubleAxeLevel;

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

        // Countdown for throwing attacks.
        throwCounter -= Time.deltaTime;

        if (throwCounter <= 0)
        {
            // Reset throw timer.
            throwCounter = stats[weaponLevel].timeBetweenAttacks;

            // Throw the double axe.
            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                // Spawn damager.
                Instantiate(damager, damager.transform.position, damager.transform.rotation).gameObject.SetActive(true);

                // Play attack sound effect.
                SFXManager.instance.PlaySFXPitched(4);
            }
        }
        
        doubleAxeLevel = weaponLevel;
    }

    // Update weapon stats.
    void SetStats()
    {
        // Set damager stats.
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        // Reset throw counter.
        throwCounter = 0f;
    }
}
