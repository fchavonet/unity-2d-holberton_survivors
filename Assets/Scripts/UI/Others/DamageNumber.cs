using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [Space(10)]
    // Reference to the text component displaying the damage number
    public TMP_Text damageText;

    [Space(10)]
    // The lifetime of the damage number before it's returned to the pool
    public float lifetime;
    // The speed at which the damage number floats up
    public float floatSpeed = 2f;

    private float lifeCounter;

    void Start()
    {
        lifeCounter = lifetime;
    }

    void Update()
    {
        // Decrease the life counter
        if(lifeCounter > 0)
        {
            lifeCounter -= Time.deltaTime;
            // If the lifetime is over, return the damage number to the pool
            if(lifeCounter < 0)
            {
                DamageController.instance.PlaceInPool(this);
            }
        }

        // Move the damage number up to create a floating effect
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }

    // Setup the damage number with the given damage value
    public void Setup(int damageDisplay)
    {
        lifeCounter = lifetime;
        damageText.text = damageDisplay.ToString();
    }
}
