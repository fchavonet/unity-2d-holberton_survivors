using UnityEngine;

public class SpinWeapon : Weapon
{
    [Space(10)]
    public Transform holder;
    public Transform shurikenToSpawn;

    [Space(10)]
    public float rotateSpeed = 180f;
    public float timeBetweenSpawn = 5f;

    private float spawnCounter;

    public EnemyDamager damager;

    void Start()
    {
        SetStats();

        //UIController.instance.levelUpButton[0].UpdateButtonDisplay(this);
    }

    void Update()
    {
        //holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));

        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * stats[weaponLevel].speed));


        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;

            Instantiate(shurikenToSpawn, shurikenToSpawn.position, shurikenToSpawn.rotation, holder).gameObject.SetActive(true);
        }

        if (statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
        }
    }

    public void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;

        transform.localScale = Vector3.one * stats[weaponLevel].range;

        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;

        damager.lifeTime = stats[weaponLevel].duration;

        spawnCounter = 0f;
    }
}
