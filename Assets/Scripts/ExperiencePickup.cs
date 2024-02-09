using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
    [Space(10)]
    public int experienValue;
    public float moveSpeed;
    public float timeBetweenChecks = .2f;

    private PlayerController player;
    private bool movingToPlayer;
    private float checkCounter;

    void Start()
    {
        player = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    void Update()
    {
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
            LevelController.instance.GetExp(experienValue);

            Destroy(gameObject);
        }
    }
}
