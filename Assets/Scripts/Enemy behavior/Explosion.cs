using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Effect to triger.
    public GameObject deathEffect;

    void OnDestroy()
    {
        // Instantiate the death effect when the object is destroyed.
        Instantiate(deathEffect, transform.position, Quaternion.identity);
    }  
}