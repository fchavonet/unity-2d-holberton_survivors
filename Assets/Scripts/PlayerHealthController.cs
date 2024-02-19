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

    private void OnHit()
    {
        TakeDamage(10f);
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);

            LevelTimer.instance.GameOver();
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
