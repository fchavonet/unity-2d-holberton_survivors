using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponListController : MonoBehaviour
{
    public PlayerController playerController;
    public Image[] weaponImagesUI;
    private List<Sprite> weaponImages = new();

    void Update()
    {
        int displayedWeaponsCount = 0;

        // Iterate through weapon images UI elements
        for (int i = 0; i < weaponImagesUI.Length; i++)
        {
            // Check if there are weapons in the player's list at this index
            if (i < playerController.listWeapons.Count)
            {
                Weapon weapon = playerController.listWeapons[i];

                // Check if the weapon's tag is not "PlayerUpdate" and it has an icon
                if (weapon.tag != "PlayerUpdate" && weapon.icon != null)
                {
                    // Activate the UI element and set its sprite to the weapon's icon
                    weaponImagesUI[displayedWeaponsCount].gameObject.SetActive(true);
                    weaponImagesUI[displayedWeaponsCount].sprite = weapon.icon;

                    // If the displayedWeaponsCount is greater or equal to the weaponImages list count, add the icon to the list
                    if (displayedWeaponsCount >= weaponImages.Count)
                    {
                        weaponImages.Add(weapon.icon);
                    }
                    else
                    {
                        // Otherwise, update the icon in the existing list
                        weaponImages[displayedWeaponsCount] = weapon.icon;
                    }
                    displayedWeaponsCount++;
                }
                else
                {
                    // Deactivate the UI element if the weapon's tag is "PlayerUpdate" or it has no icon
                    weaponImagesUI[displayedWeaponsCount].gameObject.SetActive(false);
                }
            }
            else
            {
                // Deactivate the UI element if there are no weapons at this index
                weaponImagesUI[i].gameObject.SetActive(false);
            }
        }
    }
}
