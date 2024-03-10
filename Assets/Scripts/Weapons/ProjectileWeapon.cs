using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    [Space(10)]
    // Speed of the projectile.
    public float moveSpeed;

    void Update()
    {
        // Move the projectile forward.
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
}
