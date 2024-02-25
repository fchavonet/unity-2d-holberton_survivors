using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
    [Space(10)]
    public int experienValue;
    public float moveSpeed;
    public float timeBetweenChecks = .2f;
    public float lifeDuration = 60f;

    private PlayerController player;
    private bool movingToPlayer;
    private float checkCounter;
    private float lifeTimer;

    void Start()
    {
        player = PlayerHealthController.instance.GetComponent<PlayerController>();
        lifeTimer = lifeDuration;
    }

    void Update()
    {
        lifeTimer -= Time.deltaTime;

        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }

        if (movingToPlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            checkCounter -= Time.deltaTime;
            if (checkCounter <= 0)
            {
                checkCounter = timeBetweenChecks;

                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    movingToPlayer = true;
                    moveSpeed += player.speed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SFXManager.instance.PlaySFXPitched(1);

            LevelController.instance.GetExp(experienValue);

            Destroy(gameObject);
        }
    }
}
