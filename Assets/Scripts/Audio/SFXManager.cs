using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // Singleton instance of SFXManager for easy access from other scripts
    public static SFXManager instance;

    [Space(10)]
    // Array of audio sources for sound effects
    public AudioSource[] soundEffects;

    private void Awake()
    {
        instance = this;
    }

    // Function to play the specified sound effect
    public void PlaySFX(int sfxToPlay)
    {
        // Stop the sound effect first to ensure it plays from the beginning
        soundEffects[sfxToPlay].Stop();
        // Play the sound effect
        soundEffects[sfxToPlay].Play();
    }

    // Function to stop the specified sound effect
    public void StopSFX(int sfxToPlay)
    {
        // Stop the specified sound effect
        soundEffects[sfxToPlay].Stop();
    }

    // Function to play the specified sound effect with a random pitch
    public void PlaySFXPitched(int sfxToPlay)
    {
        // Set a random pitch between 0.8 and 1.2
        soundEffects[sfxToPlay].pitch = Random.Range(0.8f, 1.2f);
        // Play the sound effect with the new pitch
        PlaySFX(sfxToPlay);
    }

    // Function to stop all sound effects
    public void StopAllSFX()
    {
        // Loop through all sound effects and stop each one
        foreach (AudioSource sfx in soundEffects)
        {
            sfx.Stop();
        }
    }
}
