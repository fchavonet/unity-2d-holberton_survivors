using UnityEngine;

public class PlayerSpeed : Weapon
{
    public void Start()
    {
        UpgradeMovespeed();
    }

    public void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;
            UpgradeMovespeed();
        }
    }

    public void UpgradeMovespeed()
    {
        PlayerController.instance.SpeedLevelUp();
    }
}
