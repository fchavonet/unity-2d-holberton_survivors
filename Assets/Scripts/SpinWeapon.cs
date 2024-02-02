using UnityEngine;

public class SpinWeapon : MonoBehaviour
{
    [Space(10)]
    public Transform holder;
    public Transform shurikenToSpawn;

    [Space(10)]
    public float rotateSpeed = 180f;
    public float timeBetweenSpawn = 5f;

    private float spawnCounter;

    void Update()
    {
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));

        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;

            Instantiate(shurikenToSpawn, shurikenToSpawn.position, shurikenToSpawn.rotation, holder).gameObject.SetActive(true);
        }
    }
}
