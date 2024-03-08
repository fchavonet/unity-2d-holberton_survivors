using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{
    public TMP_Text upgradeDescText, nameLevelText;
    public Image weaponIcon;

    private Weapon assignedWeapon;

    public void UpdateButtonDisplay(Weapon theWeapon)
    {

        if (theWeapon.gameObject.activeSelf == true)
        {
            upgradeDescText.text = theWeapon.stats[theWeapon.weaponLevel].upgradeText;
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.name;
        }
        else
        {
            if (theWeapon.tag != "PlayerUpdate")
            {
                upgradeDescText.text = "UNLOCK\n" + theWeapon.name;
                weaponIcon.sprite = theWeapon.icon;

                nameLevelText.text = theWeapon.name;
            }
            else
            {
                upgradeDescText.text = "+10%";
                weaponIcon.sprite = theWeapon.icon;

                nameLevelText.text = theWeapon.name;
            }
        }

        assignedWeapon = theWeapon;
    }

    public void SelectUpgrade()
    {
        if (assignedWeapon != null)
        {
            if (assignedWeapon.gameObject.activeSelf == true)
            {
                assignedWeapon.LevelUp();
            }
            else
            {
                PlayerController.instance.AddWeapon(assignedWeapon);
            }

            UIController.instance.levelUpPanel.SetActive(false);
            GameController.instance.gameActive = true;
            
            Time.timeScale = 1f;
        }
    }
}
