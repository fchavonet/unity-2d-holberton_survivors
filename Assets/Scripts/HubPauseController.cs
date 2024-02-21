
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HubPauseController : MonoBehaviour
{
    public static HubPauseController instance;

    public GameObject pauseScreen;
    public GameObject defaultSelectedButton;
    public string mainMenuName;

    private void Awake()
    {
        instance = this;
    }

    public void OnPressPause()
    {
            PauseUnpause();
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
            Time.timeScale = 1f;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
        Time.timeScale = 1f;
    }

    public void Quitgame()
    {
        Application.Quit();
    }
}
