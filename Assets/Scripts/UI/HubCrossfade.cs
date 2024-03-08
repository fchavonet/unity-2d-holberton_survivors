using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubCrossfade : MonoBehaviour
{
    [Space(10)]
    public Animator crossfadeAnimator;
    public Animator doorAnimator;

    [Space(10)]
    public float transitionTime = 1f;

    [Space(10)]
    public AudioSource audioSource;
    public float musicFadeOutTime = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Door"))
        {
            NextLevelCrossfade();
            doorAnimator.SetTrigger("Open");
            SFXManager.instance.PlaySFX(1);
        }
    }

    public void NextLevelCrossfade()
    {
        StartCoroutine(loadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        StartCoroutine(FadeOutMusic());
    }

    IEnumerator loadLevel(int levelIndex)
    {
        crossfadeAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator FadeOutMusic()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / musicFadeOutTime;
            yield return null;
        }

        audioSource.Stop();
    }
}
