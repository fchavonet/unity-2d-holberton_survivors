using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCrossfade : MonoBehaviour
{
    [Space(10)]
    // Animator for the crossfade transition
    public Animator animator;

    [Space(10)]
    // Duration of the crossfade transition
    public float transitionTime = 1f;

    [Space(10)]
    // Audio source for background music
    public AudioSource audioSource;
    // Time to fade out the music
    public float musicFadeOutTime = 1f;

    // Initiates the crossfade animation and loads the next level.
    public void OnPressStart()
    {
        // Start the crossfade animation and load the next level
        NextLevelCrossfade();
    }

    // Initiates the crossfade animation and loads the next level after a delay.
    public void NextLevelCrossfade()
    {
        // Start the crossfade animation and load the next level
        StartCoroutine(loadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        // Fade out the background music
        StartCoroutine(FadeOutMusic());
    }

    // Coroutine to load a level with a crossfade animation.
    IEnumerator loadLevel(int levelIndex)
    {
        // Trigger the crossfade animation
        animator.SetTrigger("Start");
        // Wait for the specified transition time
        yield return new WaitForSeconds(transitionTime);
        yield return new WaitForSeconds(musicFadeOutTime);

        // Load the next scene
        SceneManager.LoadScene(levelIndex);
    }

    // Coroutine to fade out the background music.
    IEnumerator FadeOutMusic()
    {
        // If the audio source is not assigned, try to get it from the component
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Store the initial volume of the audio source
        float startVolume = audioSource.volume;
        // Gradually decrease the volume of the background music
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / musicFadeOutTime;
            yield return null;
        }
        
        // Stop the music when the volume reaches zero
        audioSource.Stop();
    }
}
