using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    [Space(10)]
    public float damageAmount = 5f;
    public float lifeTime = 3f;
    public float growSpeed = 5f;

    [Space(10)]
    public bool destroyParent;
    public bool shouldKnockBack;
    public bool destroyOnImpact;

    [Space(10)]
    public bool damageOverTime;
    public float timeBetweenDamage;

    private float damageCounter;
    private List<EnemyController> enemiesInRange = new List<EnemyController>();
    private List<BossController> bossInRange = new List<BossController>();
    private Vector3 targetSize;

    void Start()
    {
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            targetSize = Vector3.zero;

            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);

                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        if (damageOverTime == true)
        {
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
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }

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
        if (damageOverTime == false)
        {
            if (collision.tag == "Enemy" || collision.tag == "Boss")
            {
                if(collision.CompareTag("Boss"))
                {
                    collision.GetComponent<BossController>().TakeDamage(damageAmount, shouldKnockBack);
                }
                else
                {
                collision.GetComponent<EnemyController>().TakeDamage(damageAmount, shouldKnockBack);
                }
                if(destroyOnImpact)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
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
