using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Singleton instance for LevelController
    public static LevelController instance;

    [Space(10)]
    // Reference to the ExperiencePickup script
    public ExperiencePickup pickup;
    // Reference to the VortexCoin script
    public VortexCoin vortex;

    [Space(10)]
    // Current experience points of the player
    public int currentExperience;
    // List of experience points required for each level
    public List<int> expLevels;
    // Current level of the player
    public int currentLevel = 1;
    // Total number of levels
    public int levelCount = 100;
    // List of weapons that can be upgraded
    public List<Weapon> weaponsToUpgrade;
    // Default selected button for UI navigation-
    public GameObject defaultSelectedButton;

    // Total number of enemies defeated by the player
    public int enemiesDefeated = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Ensure the expLevels list has enough levels to cover the specified levelCount
        while (expLevels.Count < levelCount)
        {
             // Incrementally increase experience levels for progression
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    // Method called when player gains experience
    public void GetExp(int amountToGet)
    {
        // Increase the player's current experience
        currentExperience += amountToGet;

        // Check if current experience is enough to level up
        if (currentExperience >= expLevels[currentLevel])
        {
            // Call method to handle player leveling up
            LevelUp();
        }

        // Update the UI to reflect the current experience and level
        UIController.instance.UpdateExperience(currentExperience, expLevels[currentLevel], currentLevel);
    }

    // Method to spawn an experience pickup at a specific position
    public void SpawnExp(Vector3 position, int expValue)
    {
        // Instantiate an experience pickup at the given position with a specific experience value
        Instantiate(pickup, position, Quaternion.identity).experienValue = expValue;
    }

    // Method to spawn a VortexCoin (chest) at a specific position
    public void SpawnChest(Vector3 position)
    {
        // Check if a chest has already been spawned
        if (PlayerController.instance.isChestSpawned == false)
        {
            // Spawn a chest (VortexCoin) at the specified position
            PlayerController.instance.isChestSpawned = true;
            Instantiate(vortex, position, Quaternion.identity);
        }
    }

    // Method called when the player levels up
    void LevelUp()
    {
        // Stop any active sound effects
        SFXManager.instance.StopSFX(0);

        // Deduct the required experience for the current level from the player's current experience
        currentExperience -= expLevels[currentLevel];

        // Increment the player's level
        currentLevel++;

        // Ensure the current level does not exceed the maximum available level
        if (currentLevel >= expLevels.Count)
        {
            currentLevel = expLevels.Count - 1;
        }

        // Pause the game and display the level up panel
        GameController.instance.gameActive = false;
        UIController.instance.levelUpPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultSelectedButton);

        // Set the time scale to 0 to pause the game
        Time.timeScale = 0f;

        weaponsToUpgrade.Clear();

        // Create a list of available weapons for upgrade
        List<Weapon> availableWeapons = new List<Weapon>();
        availableWeapons.AddRange(PlayerController.instance.assignedWeapons);

        // Select a weapon from the assigned weapons list for upgrade
        if (availableWeapons.Count > 0)
        {
            int selected = Random.Range(0, availableWeapons.Count);
            weaponsToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }

        // Fill remaining upgrade slots with unassigned weapons
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

         // Update UI buttons for weapon upgrades
        for (int i = 0; i < weaponsToUpgrade.Count; i++)
        {
            UIController.instance.levelUpButton[i].UpdateButtonDisplay(weaponsToUpgrade[i]);
        }

        // Activate or deactivate buttons based on available upgrades
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
}
