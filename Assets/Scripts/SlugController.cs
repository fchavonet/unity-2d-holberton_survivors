using System.Collections;
using UnityEngine;

public class Slug : MonoBehaviour
{
    [Space(10)]
    public SpriteRenderer spriteRenderer;

    [Space(10)]
    public float movementSpeed = 0.1f;
    public float movementWidth = 4f;
    public float movementHeight = 4f;

    private Vector2 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;

        MoveRandomly();
    }

    private void MoveRandomly()
    {
        float newPosX = initialPosition.x + Random.Range(-movementWidth / 2f, movementWidth / 2f);
        float newPosY = initialPosition.y + Random.Range(-movementHeight / 2f, movementHeight / 2f);
        Vector2 newPosition = new Vector2(newPosX, newPosY);

        spriteRenderer.flipX = (newPosition.x < transform.position.x);

        StartCoroutine(MoveToPosition(newPosition));
    }

    private IEnumerator MoveToPosition(Vector2 destination)
    {
        while ((Vector2)transform.position != destination)
        {
            Vector2 movement = Vector2.ClampMagnitude(destination - (Vector2)transform.position, movementSpeed * Time.deltaTime);

            transform.position += (Vector3)movement;

            yield return null;
        }

        yield return new WaitForSeconds(1f);

        MoveRandomly();
    }
}
