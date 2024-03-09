using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    [Space(10)]
    // Specifies the damage dealt to enemies.
    public float damageAmount = 5f;
    // Duration before the damage object is automatically destroyed.
    public float lifeTime = 3f;
    // The speed at which the damage object grows to its target size.
    public float growSpeed = 5f;

    [Space(10)]
    public bool destroyParent;
    // If true, enemies will be knocked back when damaged.
    public bool shouldKnockBack;
    // If true, the damage source is destroyed upon first impact with an enemy.
    public bool destroyOnImpact;

    [Space(10)]
    // Enables or disables damage over time functionality.
    public bool damageOverTime;
    // Time interval between successive damage applications in DoT mode.
    public float timeBetweenDamage;

    // Tracks time between DoT applications.
    private float damageCounter;
    // List of enemies currently within damage range for DoT.
    private List<EnemyController> enemiesInRange = new List<EnemyController>();
    // List of bosses currently within damage range for DoT.
    private List<BossController> bossInRange = new List<BossController>();
    // The intended final size of the damage source object.
    private Vector3 targetSize;

    void Start()
    {
        // Initialize the target size and set the initial scale to zero
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        // Grow the object to its target size over time
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        // Decrease the lifetime of the object and check for expiration
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            // Begin shrinking the object down to removal size
            targetSize = Vector3.zero;
            
            // Check if the object has completely shrunk
            if (transform.localScale.x == 0f)
            {
                // Destroy this object and optionally its parent
                Destroy(gameObject);

                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        // Applies damage over time if enabled.
        if (damageOverTime == true)
        {
            // Resets the damage timer and applies damage to all enemies in range.
            damageCounter -= Time.deltaTime;

            if (damageCounter <= 0)
            {
                damageCounter = timeBetweenDamage;

                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i] != null)
                    {
                        enemiesInRange[i].TakeDamage(damageAmount, shouldKnockBack);
                    }
                    else
                    {
                        // Removes any null references from the list.
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }

                // Applies damage to all bosses in range in a similar manner.
                for (int b = 0; b < bossInRange.Count; b++)
                {
                    if (bossInRange[b] != null)
                    {
                        bossInRange[b].TakeDamage(damageAmount, shouldKnockBack);
                    }
                    else
                    {
                        bossInRange.RemoveAt(b);
                        b--;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Direct damage or addition to the DOT list upon collision.
        if (damageOverTime == false)
        {
            // Instant damage on collision.
            if (collision.tag == "Enemy" || collision.tag == "Boss")
            {
                if(collision.CompareTag("Boss"))
                {
                    // Applies damage to the boss.
                    collision.GetComponent<BossController>().TakeDamage(damageAmount, shouldKnockBack);
                }
                else
                {
                    // Applies damage to an enemy.
                    collision.GetComponent<EnemyController>().TakeDamage(damageAmount, shouldKnockBack);
                }
                if(destroyOnImpact)
                {
                    // Destroys the damage object on impact.
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            // Adds the collided enemy or boss to the appropriate list for DOT.
            if (collision.tag == "Enemy" || collision.tag == "Boss")
            {
                if (collision.CompareTag("Boss"))
                {
                    bossInRange.Add(collision.GetComponent<BossController>());
                }
                else
                {
                    enemiesInRange.Add(collision.GetComponent<EnemyController>());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Removes enemies or bosses from the DOT list when they exit the trigger area.
        if (damageOverTime)
        {
            if (collision.CompareTag("Enemy"))
            {
                enemiesInRange.Remove(collision.GetComponent<EnemyController>());
            }
            else if (collision.CompareTag("Boss"))
            {
                bossInRange.Remove(collision.GetComponent<BossController>());
            }
        }
    }

}
