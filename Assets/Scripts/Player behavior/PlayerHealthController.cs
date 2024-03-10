using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    // Singleton instance.
    public static PlayerHealthController instance;

    [Space(10)]
    // UI slider for health.
    public Slider healthSlider;

    [Space(10)]
    // Current health value.
    public float currentHealth;
    // Maximum health value.
    public float maxHealth = 100f;

    [Space(10)]
    // Total damage taken.
    public float totalDamage = 0f;

    [Space(10)]
    // Death effect object.
    public GameObject deathEffect;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Set current health to max health.
        currentHealth = maxHealth;

        // Set max value for health slider.
        healthSlider.maxValue = maxHealth;
        // Set current value for health slider.
        healthSlider.value = currentHealth;
    }

    public void Update()
    {
        // Handle health regeneration.
        Regeneration();
    }

    public void TakeDamage(float damageToTake)
    {
        // Reduce health by damage taken.
        currentHealth -= damageToTake;
        // Update total damage.
        totalDamage += damageToTake;

        // Check if health is depleted.
        if (currentHealth <= 0)
        {
            // Deactivate player object.
            gameObject.SetActive(false);

            // Trigger game over event.
            GameController.instance.GameOver();
            // Shake the camera.
            CameraShake.instance.ShakeIt(0.5f, 0.2f);
            // Create death effect.
            Instantiate(deathEffect, transform.position, transform.rotation);
        }

        // Update health slider value.
        healthSlider.value = currentHealth;
    }

    public void Regeneration()
    {
        if (currentHealth < maxHealth)
        {
            // Increase health over time.
            currentHealth += 1 * Time.deltaTime;
            // Update health slider value.
            healthSlider.value = currentHealth;
        }
    }
}
