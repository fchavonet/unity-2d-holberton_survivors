using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{
    // Text displaying the weapon's upgrade description
    public TMP_Text upgradeDescText;
    // Text displaying the weapon's name and level
    public TMP_Text nameLevelText;

    // Image displaying the weapon's icon
    public Image weaponIcon;

    // The weapon assigned to this button
    private Weapon assignedWeapon;

    // Update the button's display with the given weapon
    public void UpdateButtonDisplay(Weapon theWeapon)
    {
        // If the weapon is active, display its upgrade description and icon
        if (theWeapon.gameObject.activeSelf == true)
        {
            upgradeDescText.text = theWeapon.stats[theWeapon.weaponLevel].upgradeText;
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.name;
        }
        else
        {
            // If the weapon is not active but can be unlocked, display unlock text and icon
            if (theWeapon.tag != "PlayerUpdate")
            {
                upgradeDescText.text = "UNLOCK\n" + theWeapon.name;
                weaponIcon.sprite = theWeapon.icon;

                nameLevelText.text = theWeapon.name;
            }
            else
            {
                // If the weapon is a player update, display a generic upgrade description
                upgradeDescText.text = "+10%";
                weaponIcon.sprite = theWeapon.icon;

                nameLevelText.text = theWeapon.name;
            }
        }

        assignedWeapon = theWeapon;
    }

    // Function to handle selecting the upgrade
    public void SelectUpgrade()
    {
        if (assignedWeapon != null)
        {
            // If the assigned weapon is active, level it up
            if (assignedWeapon.gameObject.activeSelf == true)
            {
                assignedWeapon.LevelUp();
            }
            else
            {
                // If the weapon is not active, add it to the player's weapons
                PlayerController.instance.AddWeapon(assignedWeapon);
            }

            // Close the level up panel and resume the game
            UIController.instance.levelUpPanel.SetActive(false);
            GameController.instance.gameActive = true;       
            Time.timeScale = 1f;
        }
    }
}
