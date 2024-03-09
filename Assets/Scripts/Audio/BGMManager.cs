using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // Singleton instance of BGMManager for easy access from other scripts
    public static BGMManager instance;

    // Array of audio sources for background music
    public AudioSource[] bgm;

    private void Awake()
    {
        instance = this;
    }

     // Function to stop the specified background music
    public void StopBGM(int bgmToStop)
    {
        // Stop the background music at the given index
        bgm[bgmToStop].Stop();
    }
}
