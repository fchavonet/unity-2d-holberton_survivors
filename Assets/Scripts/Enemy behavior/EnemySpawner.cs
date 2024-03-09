using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Variables for defining spawn area
    [Space(10)]
    // The minimum point of the spawn area.
    public Transform minSpawn;
    // The maximum point of the spawn area.
    public Transform maxSpawn;

    // Variables for enemy to spawn
    [Space(10)]
    // The enemy prefab to spawn.
    public GameObject enemyToSpawn;

    [Space(10)]
    // Time between each enemy spawn.
    public float timeToSpawn;

    // Variables for despawning and tracking
    [Space(10)]
    // Number of enemies to check for despawning per frame.
    public int checkPerFrame;
    // List of WaveInfo objects defining different waves of enemy spawns.
    public List<WaveInfo> waves;

    // List to keep track of spawned enemies
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    // Index of the enemy to check for despawning.
    private int enemyToCheck;
    // Target position for the spawner to follow (usually the player).
    private Transform target;
    // Distance from the spawner where enemies will despawn.
    private float despawnDistance;
    // Countdown timer for spawning enemies.
    private float spawnCounter;
    // Index of the current wave.
    private int currentWave;
    // Countdown timer for the current wave duration.
    private float waveCounter;

    void Start()
    {
        // Initialization
        spawnCounter = timeToSpawn;
        target = PlayerHealthController.instance.transform;
        // Calculate the distance for despawning enemies
        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 5f;
        currentWave = -1;
        // Start the first wave.
        GoToNextWave();
    }

    void Update()
    {
        if (PlayerHealthController.instance.gameObject.activeSelf)
        {
            // Ensure the player is active before continuing wave and spawn logic
            if (currentWave < waves.Count)
            {
                // Countdown for the current wave
                waveCounter -= Time.deltaTime;
                if (waveCounter <= 0)
                {
                    // Move to the next wave when the current wave duration is over
                    GoToNextWave();
                }
                spawnCounter -= Time.deltaTime;
                if (spawnCounter <= 0)
                {
                    // Spawn an enemy when the spawn counter reaches zero
                    spawnCounter = waves[currentWave].timeBetweenSpawns;

                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);

                    // Add the spawned enemy to the list
                    spawnedEnemies.Add(newEnemy);
                }
            }
        }
        // Move the spawner position to the target (player)
        transform.position = target.position;

        // Despawn logic for enemies out of range
        int checkTarget = enemyToCheck + checkPerFrame;

        while (enemyToCheck < checkTarget)
        {
            if (enemyToCheck < spawnedEnemies.Count)
            {
                if (spawnedEnemies[enemyToCheck] != null)
                {
                    if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance)
                    {
                        // Destroy the enemy and remove from the list if it's out of range
                        Destroy(spawnedEnemies[enemyToCheck]);
                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    }
                    else
                    {
                        // Move to the next enemy to check
                        enemyToCheck++;
                    }
                }
                else
                {
                    // Remove null enemies from the list
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            }
            else
            {
                // Reset the check index if it exceeds the list size
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    // Selects a random spawn point within the defined area
    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;
        // Determine whether to spawn near vertical or horizontal edge
        bool spawVerrticalEdge = Random.Range(0f, 1f) > .5f;
        if (spawVerrticalEdge)
        {
            // Randomly select y-coordinate within spawn area
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);
            // Randomly choose to spawn at the max or min x-position
            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.x = maxSpawn.position.x;
            }
            else
            {
                spawnPoint.x = minSpawn.position.x;
            }
        }
        else
        {
            // Randomly select x-coordinate within the spawn area
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);
            // Randomly choose to spawn at the max or min y-position
            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = minSpawn.position.y;
            }
        }
        return spawnPoint;
    }

    // Move to the next wave in the list
    public void GoToNextWave()
    {
        currentWave++;
        // Ensure the current wave index does not exceed the number of waves
        if (currentWave >= waves.Count)
        {
            currentWave = waves.Count - 1;
        }
        // Set timers for the next wave
        waveCounter = waves[currentWave].waveLength;
        spawnCounter = waves[currentWave].timeBetweenSpawns;
    }

    // Stop generating enemies
    public void StopEnemyGeneration()
    {
        enabled = false;
    }
}

[System.Serializable]
public class WaveInfo
{
    // The enemy prefab to spawn for this wave.
    public GameObject enemyToSpawn;
    // Duration of this wave.
    public float waveLength = 10f;
    // Time between each enemy spawn in this wave.
    public float timeBetweenSpawns = 1f;
}
