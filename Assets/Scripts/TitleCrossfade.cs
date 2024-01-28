using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleCrossfade : MonoBehaviour
{
    [Space(10)]
    public Animator animator;
    [Space(10)]
    public float transitionTime = 1f;

    public void OnPressStart()
    {
        NextLevelCrossfade();
    }

    public void NextLevelCrossfade()
    {
        StartCoroutine(loadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator loadLevel(int levelIndex)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
