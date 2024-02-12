using UnityEngine;

public class DangerZone : Weapon
{
    public EnemyDamager damager;

    private float spawnTime;
    private float spawnCounter;

    void Start()
    {
        SetStats();
    }

    void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
        }

        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0f)
        {
            spawnCounter = spawnTime;

            Instantiate(damager, damager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);
        }
    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;

        damager.lifeTime = stats[weaponLevel].duration;

        damager.timeBetweenDamage = stats[weaponLevel].speed;

        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;

        spawnTime = stats[weaponLevel].timeBetweenAttacks;

        spawnCounter = 0f;
    }
}
