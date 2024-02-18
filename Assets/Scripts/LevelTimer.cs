using System.Collections;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer instance;

    [Space(10)]
    public EnemySpawner enemySpawner;
    public GameObject deathEffect;

    [Space(10)]
    public GameObject boss;

    [Space(10)]
    public float endTimer = 600f;

    private PlayerController player;
    private float timer;
    private float waitToShowEndScreen = 1f;
    private bool bossSpawned = false;
    private bool gameActive;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gameActive = true;
        player = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (gameActive == true)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);
        }

        if (timer >= endTimer)
        {
            BossSpawn();
            EndGame();
        }
    }

    public void GameOver()
    {
        gameActive = false;

        StartCoroutine(GameOverCo());
    }

    IEnumerator GameOverCo()
    {
        yield return new WaitForSeconds(waitToShowEndScreen);

        Time.timeScale = 0f;

        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60);

        UIController.instance.endTimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        UIController.instance.gameOverScreen.SetActive(true);
    }

    public void BossSpawn()
    {
        if (!bossSpawned)
        {
            Instantiate(boss, new Vector3(player.transform.position.x, player.transform.position.y + 15, 0), Quaternion.identity);

            bossSpawned = true;
        }
    }

    public void EndGame()
    {
        GameObject bossObject = GameObject.FindGameObjectWithTag("Boss");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemySpawner.StopEnemyGeneration();
        
        foreach (GameObject enemy in enemies)
        {
            Vector3 enemyPosition = enemy.transform.position;

            Instantiate(deathEffect, enemyPosition, Quaternion.identity);

            Destroy(enemy);
        }
        

        if (bossObject == null)
        {
            gameActive = false;
            UIController.instance.levelEndScreen.SetActive(true);
        }
    }
}
