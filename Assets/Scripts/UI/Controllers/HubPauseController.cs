
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HubPauseController : MonoBehaviour
{
    public static HubPauseController instance;

    // The pause screen game object
    public GameObject pauseScreen;
    // The default button selected when pause screen is activated
    public GameObject defaultSelectedButton;
    // The name of the main menu scene to load
    public string mainMenuName;

    private void Awake()
    {
        instance = this;
    }

    // Method called when the pause button is pressed
    public void OnPressPause()
    {
            PauseUnpause();
    }

    // Method to pause or unpause the game
    public void PauseUnpause()
    {
        // If the pause screen is not active, pause the game
        if (pauseScreen.activeSelf == false)
        {
            // Stop all sound effects when pausing
            SFXManager.instance.StopAllSFX();

            // Activate the pause screen
            pauseScreen.SetActive(true);
            // Set the time scale to 0 to pause the game
            Time.timeScale = 0f;
            // Set the selected button on the event system to the default selected button
            EventSystem.current.SetSelectedGameObject(defaultSelectedButton);
        }
        else
        {
            // If the pause screen is active, unpause the game
            pauseScreen.SetActive(false);
            // Set the time scale back to 1 to resume the game
            Time.timeScale = 1f;
            // Set the selected button on the event system to null (no selected button)
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    // Method to go back to the main menu
    public void GoToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene(mainMenuName);
        // Set the time scale back to 1 to ensure normal time flow
        Time.timeScale = 1f;
    }

    // Method to quit the game
    public void Quitgame()
    {
        // Quit the application
        Application.Quit();
    }
}
