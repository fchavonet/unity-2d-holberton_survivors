using UnityEngine;

public class PlayerSpeed : Weapon
{
    public void Start()
    {
        UpgradeMovespeed();
    }

    public void Update()
    {
        // Check if stats are updated and upgrade move speed accordingly.
        if (statsUpdated == true)
        {
            statsUpdated = false;
            UpgradeMovespeed();
        }
    }

    // Upgrade move speed method.
    public void UpgradeMovespeed()
    {
        // Call PlayerController to level up move speed.
        PlayerController.instance.SpeedLevelUp();
    }
}
