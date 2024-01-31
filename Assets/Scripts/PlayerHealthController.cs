using Unity.VisualScripting;
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
        }

        healthSlider.value = currentHealth;
    }
}
