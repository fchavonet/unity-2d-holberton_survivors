using TMPro;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    // Reference to the TMP_InputField for player name input
    public TMP_InputField inputField;

    // Variable to store the best timer value
    private int bestTimer;

    // Called when the player wants to upload their score
    public void UploadScore()
    {
        // Retrieve the best timer value from the game controller
        bestTimer = GameController.instance.bestTimer;

        // Call the AddNewHighscore method of the Leaderboard class
        Leaderboard.AddNewHighscore(inputField.text, bestTimer);
    }
}
