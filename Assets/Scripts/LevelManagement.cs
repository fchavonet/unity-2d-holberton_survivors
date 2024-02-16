using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    public static LevelManagement instance;
    private bool gameActive;
    public float timer;

    public float waitToShowEndScreen = 1f;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gameActive = true;
    }

    void Update()
    {
        if(gameActive == true)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);
        }
    }

    public void EndLevel()
    {
        gameActive = false;

        StartCoroutine(EndLevelCo());
    }

    IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(waitToShowEndScreen);

        Time.timeScale = 0f;

        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60);

        UIController.instance.endTimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        UIController.instance.levelEndScreen.SetActive(true);
    }
}
