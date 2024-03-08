using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCrossfade : MonoBehaviour
{
    [Space(10)]
    public Animator animator;

    [Space(10)]
    public float transitionTime = 1f;

    [Space(10)]
    public AudioSource audioSource;
    public float musicFadeOutTime = 1f;

    public void OnPressStart()
    {
        NextLevelCrossfade();
    }

    public void NextLevelCrossfade()
    {
        StartCoroutine(loadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        StartCoroutine(FadeOutMusic());
    }

    IEnumerator loadLevel(int levelIndex)
    {

        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        yield return new WaitForSeconds(musicFadeOutTime);

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
