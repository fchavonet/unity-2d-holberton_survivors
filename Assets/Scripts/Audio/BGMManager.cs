using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // Singleton instance of BGMManager for easy access from other scripts
    public static BGMManager instance;

    [Space(10)]
    // Array of audio sources for background music
    public AudioSource[] music;

    private void Awake()
    {
        instance = this;
    }

     // Function to stop the specified background music
    public void StopBGM(int bgmToStop)
    {
        // Stop the background music at the given index
        music[bgmToStop].Stop();
    }
}
