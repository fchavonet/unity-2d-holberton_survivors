using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;
    LeaderboardDisplay leaderboardDisplay;

    private const string privateCode = "r1tjaAOI3k6WiJJP4gWdEQfKuaHpyEqEGKsCxnFjdfLQ";
    private const string publicCode = "65eb5bc08f40bcbe8897ef97";
    private const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoresList;

    private void Awake()
    {
        instance = this;
        leaderboardDisplay = GetComponent<LeaderboardDisplay>();
    }

    // Method to add a new highscore to the leaderboard
    public static void AddNewHighscore(string username, int score)
    {
        instance.StartCoroutine(instance.UploadNewHighscore(username, score));
    }

    // Coroutine to upload a new highscore to the server
    IEnumerator UploadNewHighscore(string username, int score)
    {
        string url = webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            // Check if the request was successful
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error uploading: " + webRequest.error);
            }
            else
            {
                Debug.Log("Upload Successful!");
                // After successful upload, download the updated highscores
                DownloadHiscores();
            }
        }
    }

    // Method to download highscores from the server
    public void DownloadHiscores()
    {
        StartCoroutine(DownloadHighscoresFromDatabase());
    }

    // Coroutine to download highscores from the server
    IEnumerator DownloadHighscoresFromDatabase()
    {
        string url = webURL + publicCode + "/pipe/";
    
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            // Check if the request was successful
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error downloading: " + webRequest.error);
            }
            else
            {
                // Format the downloaded highscores and update the display
                FormatHighscores(webRequest.downloadHandler.text);
                leaderboardDisplay.OnHiscoresDownloaded(highscoresList);
            }
        }
    }

    // Method to format the downloaded highscores data
    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        highscoresList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });

            // Extract username and score from each entry
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);

            // Create a Highscore object and add it to the list
            highscoresList[i] = new Highscore(username, score);
        }
    }
}

// Struct to represent a single highscore entry
public struct Highscore
{
    public string username;
    public int score;

    // Constructor for Highscore struct
    public Highscore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}
