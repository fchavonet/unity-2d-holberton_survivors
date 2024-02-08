using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public TMP_Text damageText;
    public float lifetime;
    private float lifeCounter;

    public float floatSpeed = 2f;

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
