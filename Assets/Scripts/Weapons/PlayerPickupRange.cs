using UnityEngine;

public class PlayerPickupRange : Weapon
{
    public void Start()
    {
        UpgradePickupRange();
    }

    public void Update()
    {
        // Check if stats are updated and upgrade pickup range accordingly.
        if (statsUpdated == true)
        {
            statsUpdated = false;
            UpgradePickupRange();
        }
    }

    // Upgrade pickup range method.
    public void UpgradePickupRange()
    {
        // Call PlayerController to level up pickup range.
        PlayerController.instance.PickupRangeLevelUp();
    }
}
