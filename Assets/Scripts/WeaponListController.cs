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
        for (int i = 0; i < weaponImagesUI.Length; i++)
        {
            if (i < playerController.assignedWeapons.Count)
            {
                Weapon weapon = playerController.assignedWeapons[i];

                if (weapon.icon != null)
                {
                    weaponImagesUI[i].gameObject.SetActive(true);
                    weaponImagesUI[i].sprite = weapon.icon;

                    if (i >= weaponImages.Count)
                    {
                        weaponImages.Add(weapon.icon);
                    }
                    else
                    {
                        weaponImages[i] = weapon.icon;
                    }
                }
                else
                {
                    weaponImagesUI[i].gameObject.SetActive(false);
                }
            }
            else
            {
                weaponImagesUI[i].gameObject.SetActive(false);
            }
        }
    }
}
