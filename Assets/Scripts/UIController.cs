using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Space(10)]
    public Slider experienceLevelSlider;
    public TMP_Text experienceLevelText;

    public LevelUpSelectionButton[] levelUpButton;

    public GameObject levelUpPanel;

    public string mainMenuName;

    public GameObject pauseScreen;

    public GameObject defaultSelectedButton;

    public TMP_Text timeText;

    public GameObject gameOverScreen;
    public GameObject levelEndScreen;
    public TMP_Text gameOverTimerText;
    public TMP_Text endTimerText;

    private void Awake()
    {
        instance = this;
    }

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }*/

    public void OnPressPause()
    {
        if (LevelTimer.instance.gameActive == true)
        {
            PauseUnpause();
        }
    }

    public void UpdateExperience(int currentExperience, int levelExperience, int currentLevel)
    {
        experienceLevelSlider.maxValue = levelExperience;
        experienceLevelSlider.value = currentExperience;

        experienceLevelText.text = "Level " + currentLevel;
    }

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
        LevelTimer.instance.gameActive = true;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void Quitgame()
    {
        Application.Quit();
    }

    public void PauseUnpause()
    {
        if (pauseScreen.activeSelf == false)
        {
            SFXManager.instance.StopSFX(1);
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(defaultSelectedButton);
        }
        else
        {
            pauseScreen.SetActive(false);
            if (levelUpPanel.activeSelf == false)
            {
                Time.timeScale = 1f;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60);

        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
