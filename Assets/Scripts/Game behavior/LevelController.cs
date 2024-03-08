using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    [Space(10)]
    public ExperiencePickup pickup;
    public VortexCoin vortex;

    [Space(10)]
    public int currentExperience;
    public List<int> expLevels;
    public int currentLevel = 1;
    public int levelCount = 100;
    public List<Weapon> weaponsToUpgrade;
    public GameObject defaultSelectedButton;

    public int enemiesDefeated = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;

        if (currentExperience >= expLevels[currentLevel])
        {
            LevelUp();
        }

        UIController.instance.UpdateExperience(currentExperience, expLevels[currentLevel], currentLevel);
    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(pickup, position, Quaternion.identity).experienValue = expValue;
    }

    public void SpawnChest(Vector3 position)
    {
        if (PlayerController.instance.isChestSpawned == false)
        {
            PlayerController.instance.isChestSpawned = true;
            Instantiate(vortex, position, Quaternion.identity);
        }
    }

    void LevelUp()
    {
        SFXManager.instance.StopSFX(0);

        currentExperience -= expLevels[currentLevel];

        currentLevel++;

        if (currentLevel >= expLevels.Count)
        {
            currentLevel = expLevels.Count - 1;
        }

        //PlayerController.instance.acticeWeapon.LevelUp();
        GameController.instance.gameActive = false;
        UIController.instance.levelUpPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultSelectedButton);


        Time.timeScale = 0f;

        //UIController.instance.levelUpButton[1].UpdateButtonDisplay(PlayerController.instance.acticeWeapon);
        //UIController.instance.levelUpButton[0].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[0]);

        //UIController.instance.levelUpButton[1].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[0]);
        //UIController.instance.levelUpButton[2].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[1]);

        weaponsToUpgrade.Clear();

        List<Weapon> availableWeapons = new List<Weapon>();
        availableWeapons.AddRange(PlayerController.instance.assignedWeapons);

        if (availableWeapons.Count > 0)
        {
            int selected = Random.Range(0, availableWeapons.Count);
            weaponsToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }

        if (PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyLevelledWeapons.Count < PlayerController.instance.maxWeapons)
        {
            availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);
        }

        for (int i = weaponsToUpgrade.Count; i < 3; i++)
        {
            if (availableWeapons.Count > 0)
            {
                int selected = Random.Range(0, availableWeapons.Count);
                weaponsToUpgrade.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }
        }

        for (int i = 0; i < weaponsToUpgrade.Count; i++)
        {
            UIController.instance.levelUpButton[i].UpdateButtonDisplay(weaponsToUpgrade[i]);
        }

        for (int i = 0; i < UIController.instance.levelUpButton.Length; i++)
        {
            if (i < weaponsToUpgrade.Count)
            {
                UIController.instance.levelUpButton[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.instance.levelUpButton[i].gameObject.SetActive(false);
            }
        }
    }

    public void IncrementEnemiesDefeated()
    {
        enemiesDefeated++;
    }
}
