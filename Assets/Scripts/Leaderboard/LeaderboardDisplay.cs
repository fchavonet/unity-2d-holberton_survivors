using System.Collections;
using UnityEngine;

public class LeaderboardDisplay : MonoBehaviour
{
    [Space(10)]
    // Array of TextMeshProUGUI elements to display highscores
    public TMPro.TextMeshProUGUI[] highscoreText;

    // Reference to the Leaderboard class
    private Leaderboard leaderboard;

    void Start()
    {
        // Initialize highscore text fields with "Fetching..." message
        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". Fetching...";
        }

        // Get reference to the Leaderboard component
        leaderboard = GetComponent<Leaderboard>();

        // Start refreshing highscores
        StartCoroutine("RefreshHighscores");
    }

    // Called when the highscores are downloaded from the server
    public void OnHiscoresDownloaded(Highscore[] highscoresList)
    {
        // Update the highscore display with the downloaded highscores
        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". ";

            if (highscoresList.Length > i)
            {
                // Convert the score to minutes and seconds format
                float minutes = Mathf.FloorToInt(highscoresList[i].score / 60f);
                float seconds = Mathf.FloorToInt(highscoresList[i].score % 60);

                // Format the best time as MM:SS
                string bestTime = minutes.ToString("00") + ":" + seconds.ToString("00");

                // Display the username and best time in the highscore text
                highscoreText[i].text += highscoresList[i].username + " - " + bestTime;
            }
        }
    }

    // Coroutine to periodically refresh highscores
    IEnumerator RefreshHighscores()
    {
        // Keep refreshing highscores
        while (true)
        {
            // Download the latest highscores from the server
            leaderboard.DownloadHiscores();

            // Wait for some time before refreshing again (e.g., every 10 seconds)
            yield return new WaitForSeconds(10);
        }
    }
}
