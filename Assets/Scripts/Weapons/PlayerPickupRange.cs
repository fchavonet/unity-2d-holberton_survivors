using UnityEngine;

public class PlayerPickupRange : Weapon
{
    public void Start()
    {
        UpgradePickupRange();
    }

    public void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;
            UpgradePickupRange();
        }
    }

    public void UpgradePickupRange()
    {
        PlayerController.instance.PickupRangeLevelUp();
    }
}
