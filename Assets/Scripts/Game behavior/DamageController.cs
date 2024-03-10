using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public static DamageController instance;

    [Space(10)]
    // The canvas where damage numbers will be spawned
    public Transform numberCanvas;
    // The DamageNumber prefab to spawn
    public DamageNumber numberToSpawn;

    private List<DamageNumber> numberPool = new List<DamageNumber>();

    private void Awake()
    {
        instance = this;
    }

    // Method to spawn a damage number at a specified location
    public void SpawnDamage(float damageAmount, Vector3 location)
    {
        // Round the damage amount to the nearest integer
        int rounded = Mathf.RoundToInt(damageAmount);

        // Get a DamageNumber object from the pool
        DamageNumber newDamage = GetFromPool();

        // Setup the damage number with the rounded damage amount
        newDamage.Setup(rounded);
        newDamage.gameObject.SetActive(true);

        // Set the position of the damage number to the specified location
        newDamage.transform.position = location;
    }

    // Method to get a DamageNumber object from the pool
    public DamageNumber GetFromPool()
    {
        DamageNumber numberToOutput = null;

        // If the pool is empty, instantiate a new DamageNumber object
        if (numberPool.Count == 0)
        {
            numberToOutput = Instantiate(numberToSpawn, numberCanvas);
        }
        else
        {
            // Get a DamageNumber object from the pool
            numberToOutput = numberPool[0];
            numberPool.RemoveAt(0);
        }

        return numberToOutput;
    }

    // Method to place a DamageNumber object back into the pool
    public void PlaceInPool(DamageNumber numberToPlace)
    {
        // Deactivate the DamageNumber object
        numberToPlace.gameObject.SetActive(false);

        // Add the DamageNumber object back to the pool
        numberPool.Add(numberToPlace);
    }

}
