using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Space(10)]
    // List of weapon statistics.
    public List<WeaponStats> stats;
    // Current level of the weapon.
    public int weaponLevel;
    // Icon for the weapon.
    public Sprite icon;

    [HideInInspector]
    // Flag to indicate if stats are updated.
    public bool statsUpdated;

    // Level up the weapon.
    public void LevelUp()
    {
        // Check if there are more levels to go.
        if (weaponLevel < stats.Count - 1)
        {
            // Increase the weapon level.
            weaponLevel++;
            // Flag stats as updated.
            statsUpdated = true;

            // If reached the maximum level, manage player's weapon lists.
            if (weaponLevel >= stats.Count - 1)
            {
                // Add fully leveled weapon to the list and remove from assigned weapons.
                PlayerController.instance.fullyLevelledWeapons.Add(this);
                PlayerController.instance.assignedWeapons.Remove(this);
            }
        }
    }
}

// Serializable class for weapon statistics.
[System.Serializable]
public class WeaponStats
{
    // Attributes for weapon stats.
    public float speed, damage, range, timeBetweenAttacks, amount, duration;
    // Text for upgrade.
    public string upgradeText;
}
