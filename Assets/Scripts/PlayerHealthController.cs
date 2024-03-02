using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [Space(10)]
    public Slider healthSlider;

    [Space(10)]
    public float currentHealth;
    public float maxHealth = 100f;
    public float totalDamage = 0f;
    public GameObject deathEffect;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void Update()
    {
        Regeneration();
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        totalDamage += damageToTake;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);

            LevelTimer.instance.GameOver();
            CameraShake.instance.ShakeIt(0.5f, 0.2f);
            Instantiate(deathEffect, transform.position, transform.rotation);
        }

        healthSlider.value = currentHealth;
    }

    public void Regeneration()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 1 * Time.deltaTime;
            healthSlider.value = currentHealth;
        }
    }
}
