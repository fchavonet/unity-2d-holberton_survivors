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

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Door"))
        {
            NextLevelCrossfade();
            doorAnimator.SetTrigger("Open");
        }
    }

    public void NextLevelCrossfade()
    {
        StartCoroutine(loadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator loadLevel(int levelIndex)
    {
        crossfadeAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
