using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusLevelCrossfade : MonoBehaviour
{
    [Space(10)]
    // Animator for crossfade effect.
    public Animator crossfadeAnimator;

    [Space(10)]
    // Duration of transition.
    public float transitionTime = 1f;

    [Space(10)]
    // AudioSource for music.
    public AudioSource audioSource;
    // Duration for fading out music.
    public float musicFadeOutTime = 1f;

    // Check for trigger collision.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Stairs"))
        {
            // Initiate crossfade to the next level.
            NextLevelCrossfade();
        }
    }

    // Initiate crossfade to the next level.
    public void NextLevelCrossfade()
    {
        // Load the next level.
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 2));
        // Fade out music.
        StartCoroutine(FadeOutMusic());
    }

    // Coroutine to load the next level with a crossfade effect.
    IEnumerator LoadLevel(int levelIndex)
    {
        // Trigger crossfade animation.
        crossfadeAnimator.SetTrigger("Start");

        // Wait for transition time.
        yield return new WaitForSeconds(transitionTime);

        // Load the next level.
        SceneManager.LoadScene(levelIndex);
    }

    // Coroutine to fade out the music.
    IEnumerator FadeOutMusic()
    {
        // If audio source is not assigned, get the component.
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Initial volume of the audio source.
        float startVolume = audioSource.volume;

        // Fade out the music gradually.
        while (audioSource.volume > 0)
        {
            // Decrease volume over time.
            audioSource.volume -= startVolume * Time.deltaTime / musicFadeOutTime;
            yield return null;
        }

        // Stop the music.
        audioSource.Stop();
    }
}
