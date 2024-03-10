using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [Space(10)]
    public TMP_Text damageText;

    [Space(10)]
    public float lifetime;
    public float floatSpeed = 2f;

    private float lifeCounter;

    void Start()
    {
        lifeCounter = lifetime;
    }

    void Update()
    {
        if(lifeCounter > 0)
        {
            lifeCounter -= Time.deltaTime;
            if(lifeCounter < 0)
            {
                DamageController.instance.PlaceInPool(this);
            }
        }

        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }

    public void Setup(int damageDisplay)
    {
        lifeCounter = lifetime;
        damageText.text = damageDisplay.ToString();
    }
}
