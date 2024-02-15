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
        PauseUnpause();
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
}
