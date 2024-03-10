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

        for (int i = 0; i < weaponImagesUI.Length; i++)
        {
            if (i < playerController.listWeapons.Count)
            {
                Weapon weapon = playerController.listWeapons[i];

                if (weapon.tag != "PlayerUpdate" && weapon.icon != null)
                {
                    weaponImagesUI[displayedWeaponsCount].gameObject.SetActive(true);
                    weaponImagesUI[displayedWeaponsCount].sprite = weapon.icon;

                    if (displayedWeaponsCount >= weaponImages.Count)
                    {
                        weaponImages.Add(weapon.icon);
                    }
                    else
                    {
                        weaponImages[displayedWeaponsCount] = weapon.icon;
                    }
                    displayedWeaponsCount++;
                }
                else
                {
                    weaponImagesUI[displayedWeaponsCount].gameObject.SetActive(false);
                }
            }
            else
            {
                weaponImagesUI[i].gameObject.SetActive(false);
            }
        }
    }
}
