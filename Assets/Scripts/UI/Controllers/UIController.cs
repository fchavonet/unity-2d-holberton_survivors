using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Singleton instance of the UIController
    public static UIController instance;

    // UI elements for experience display
    [Space(10)]
    public Slider experienceLevelSlider;
    public TMP_Text experienceLevelText;

    // UI elements for level up selection buttons
    public LevelUpSelectionButton[] levelUpButton;

    // UI panels
    public GameObject levelUpPanel;
    public GameObject statisticsPanel;

    // Flag to track if statistics panel is displayed
    public bool isStatisticsPanelDisplayed = false;

    // Name of the main menu scene
    public string mainMenuName;

    // UI elements for pause screen
    public GameObject pauseScreen;
    public GameObject defaultSelectedButton;

    [Space(10)]
    // UI elements for displaying statistics
    public TMP_Text totalPlayerDistance;
    public TMP_Text totalDamageReceived;
    public TMP_Text totalEnemiesSpawned;
    public TMP_Text totalEnemiesDefeated;
    public TMP_Text totalCoinsCollected;

    [Space(10)]
    public int enemiesSpawned;
    public int enemiesDefeated;
    public int coinsCollected;

    [Space(10)]
    public TMP_Text daggerLevel;
    public TMP_Text dangerZoneLevel;
    public TMP_Text doubleAxeLevel;
    public TMP_Text fireballLevel;
    public TMP_Text lightningLevel;
    public TMP_Text orbitingShurikenLevel;

    [Space(10)]
    // UI elements for displaying time
    public TMP_Text timeText;

    // UI elements for game over screen and level end screen
    public GameObject gameOverScreen;
    public GameObject levelEndScreen;
    public TMP_Text gameOverTimerText;
    public TMP_Text endTimerText;

    private void Awake()
    {
        instance = this;
    }

    // Update statistics panel with current values
    private void Update()
    {
        totalPlayerDistance.text = "PLAYER DISTANCE: " + PlayerController.instance.playerDistance.ToString("000000");
        totalDamageReceived.text = "DAMAGE RECEIVED: " + PlayerHealthController.instance.totalDamage.ToString("000000");
        totalEnemiesSpawned.text = "ENEMIES SPAWNED: " + enemiesSpawned.ToString("000000");
        totalEnemiesDefeated.text = "ENEMIES DEFEATED: " + enemiesDefeated.ToString("000000");
        totalCoinsCollected.text = "COINS COLLECTED: " + coinsCollected.ToString("000000");

        if (Dagger.instance != null && Dagger.instance.gameObject.activeSelf)
        {
            daggerLevel.text = "DAGGER LEVEL " + (Dagger.instance.daggerLevel + 1) + "/15";
        }
        if (DangerZone.instance != null && DangerZone.instance.gameObject.activeSelf)
        {
            dangerZoneLevel.text = "DANGER ZONE LEVEL " + (DangerZone.instance.dangerZoneLevel + 1) + "/15";
        }
        if (DoubleAxe.instance != null && DoubleAxe.instance.gameObject.activeSelf)
        {
            doubleAxeLevel.text = "DOUBLE AXE LEVEL " + (DoubleAxe.instance.doubleAxeLevel + 1) + "/15";
        }
        if (Fireball.instance != null && Fireball.instance.gameObject.activeSelf)
        {
            fireballLevel.text = "FIREBALL LEVEL " + (Fireball.instance.fireballLevel + 1) + "/15";
        }
        if (Lightning.instance != null && Lightning.instance.gameObject.activeSelf)
        {
            lightningLevel.text = "LIGHTNING LEVEL " + (Lightning.instance.lightningLevel + 1) + "/15";
        }
        if (OrbitingShuriken.instance != null && OrbitingShuriken.instance.gameObject.activeSelf)
        {
            orbitingShurikenLevel.text = "SHURIKEN LEVEL " + (OrbitingShuriken.instance.orbitingShurikenLevel + 1) + "/15";
        }
    }

    // Method called when the pause button is pressed
    public void OnPressPause()
    {
        // Check if the game is currently active
        if (GameController.instance.gameActive == true)
        {
            // If the game is active, toggle pause/unpause
            PauseUnpause();
        }
    }

    // Method to toggle display of statistics panel
    public void OnDisplayStatistics()
    {
        if (statisticsPanel.activeSelf == false)
        {
            statisticsPanel.SetActive(true);
        }
        else
        {
            statisticsPanel.SetActive(false);
        }
    }

    // Update experience UI elements
    public void UpdateExperience(int currentExperience, int levelExperience, int currentLevel)
    {
        experienceLevelSlider.maxValue = levelExperience;
        experienceLevelSlider.value = currentExperience;

        experienceLevelText.text = "Level " + currentLevel;
    }

    // Method to skip the level up panel
    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
        GameController.instance.gameActive = true;
    }

    // Method to go back to the main menu
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
        Time.timeScale = 1f;
    }

    // Method to restart the current scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    // Method to quit the game
    public void Quitgame()
    {
        Application.Quit();
    }

    // Method to pause or unpause the game
    public void PauseUnpause()
    {
        if (pauseScreen.activeSelf == false)
        {
            // Pause the game
            SFXManager.instance.StopAllSFX();
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(defaultSelectedButton);
        }
        else
        {
            // Unpause the game
            pauseScreen.SetActive(false);
            if (levelUpPanel.activeSelf == false)
            {
                Time.timeScale = 1f;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

    // Update the timer display
    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60);

        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void IncrementEnemiesSpawned()
    {
        enemiesSpawned++;
    }

    // Method to increment the count of enemies defeated by the player
    public void IncrementEnemiesDefeated()
    {
        enemiesDefeated++;
    }

    public void IncrementCoinsCollected()
    {
        coinsCollected++;
    }
}
