using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Space(10)]
    // Reference to the EnemySpawner script
    public EnemySpawner enemySpawner;
    // Effect to play upon enemy death
    public GameObject deathEffect;

    [Space(10)]
    // Boss GameObject to spawn
    public GameObject boss;

    [Space(10)]
    // Duration of the game before boss spawns
    public float endTimer = 600f;

    private PlayerController player;
    private float timer;
    public int bestTimer;
    private float waitToShowEndScreen = 2f;
    private bool bossSpawned = false;
    public bool gameActive;

    public bool endGame = true;

    // Default selected button for game over screen
    public GameObject defaultGameOverSelectedButton;
    // Default selected button for end game screen
    public GameObject defaultEndGameSelectedButton;

    public void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }

    void Start()
    {
        gameActive = true;
        player = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    void Update()
    {
        // Update the timer if the game is active
        if (gameActive == true)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);
        }

        // Check if it's time to spawn the boss and end the game
        if (endGame == true && timer >= endTimer)
        {
            BossSpawn();
            EndGame();
        }
    }

    // Method to handle game over
    public void GameOver()
    {
        gameActive = false;

        StartCoroutine(GameOverCo());
    }

    // Coroutine for game over screen
    IEnumerator GameOverCo()
    {
        SFXManager.instance.StopSFX(0);
    
        yield return new WaitForSeconds(waitToShowEndScreen);
        
        SFXManager.instance.PlaySFX(8);

        Time.timeScale = 0f;

        bestTimer = Mathf.FloorToInt(timer);

        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60);

        UIController.instance.gameOverTimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        UIController.instance.gameOverScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultGameOverSelectedButton);
    }

    // Method to spawn the boss
    public void BossSpawn()
    {
        if (!bossSpawned)
        {
            CameraShake.instance.ShakeIt(0.5f, 0.2f);
            
            // Spawn the boss near the player
            Instantiate(boss, new Vector3(player.transform.position.x + (-30), player.transform.position.y, 0), Quaternion.identity);

            bossSpawned = true;
        }
    }

    // Method to handle end of game
    public void EndGame()
    {
        GameObject bossObject = GameObject.FindGameObjectWithTag("Boss");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Stop spawning enemies
        enemySpawner.StopEnemyGeneration();

        // Instantiate death effect for each enemy and destroy them
        foreach (GameObject enemy in enemies)
        {
            Vector3 enemyPosition = enemy.transform.position;

            Instantiate(deathEffect, enemyPosition, transform.rotation);

            Destroy(enemy);
        }

        // If boss is defeated, end the game
        if (bossObject == null)
        {
            gameActive = false;

            StartCoroutine(GameEndCo());
        }
    }

    // Coroutine for end game screen
    IEnumerator GameEndCo()
    {
        SFXManager.instance.StopSFX(0);
        
        yield return new WaitForSeconds(waitToShowEndScreen);

        // Stop the background music, play end game sounds
        BGMManager.instance.StopBGM(0);
        SFXManager.instance.PlaySFX(9);
        SFXManager.instance.PlaySFX(10);

        Time.timeScale = 0f;

        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60);

        // Display end game screen with timer
        UIController.instance.endTimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        UIController.instance.levelEndScreen.SetActive(true);

        EventSystem.current.SetSelectedGameObject(defaultEndGameSelectedButton);
    }
}
