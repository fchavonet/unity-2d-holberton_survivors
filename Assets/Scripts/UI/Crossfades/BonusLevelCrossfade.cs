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

    [Space(10)]
    // Reference to the SpriteRenderer of the player.
    public SpriteRenderer playerSpriteRenderer;

    // Check for trigger collision.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Stairs"))
        {
            // Initiate crossfade to the next level.
            StartCoroutine(NextLevelSequence());
        }
    }

    // Initiate crossfade to the next level.
    IEnumerator NextLevelSequence()
    {
        // Set player scale to initial scale
        PlayerController.instance.transform.localScale = Vector3.one;
        
        // Trigger scale down with fade out.
        yield return StartCoroutine(ScaleDownPlayer());

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

    // Coroutine to scale down the player with fade out.
    IEnumerator ScaleDownPlayer()
    {
        Vector3 targetScale = new Vector3(0.5f, 0.5f, 1f);
        float scaleSpeed = 1f; // Adjust as needed
        float fadeOutDuration = 1f; // Adjust as needed
        Color originalColor = playerSpriteRenderer.color;
        float initialAlpha = originalColor.a;
        float fadeOutStartTime = Time.time; // Start time for fade out

        while (PlayerController.instance.transform.localScale.x > targetScale.x || playerSpriteRenderer.color.a > 0)
        {
            // Scale down the player
            PlayerController.instance.transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, 0f) * Time.deltaTime;

            // Calculate alpha value for fading out
            float newAlpha = Mathf.Lerp(initialAlpha, 0f, Mathf.Clamp01((Time.time - fadeOutStartTime) / fadeOutDuration));
            playerSpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

            yield return null;
        }

        // Ensure the player is properly scaled and faded out
        PlayerController.instance.transform.localScale = targetScale;
        playerSpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
