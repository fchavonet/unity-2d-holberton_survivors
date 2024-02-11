using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Space(10)]
    public Slider experienceLevelSlider;
    public TMP_Text experienceLevelText;

    public LevelUpSelectionButton[] levelUpButton;

    public GameObject levelUpPanel;

    private void Awake()
    {
        instance = this;
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
}
