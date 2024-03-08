using UnityEngine;

public class InfiniteGrid : MonoBehaviour
{
    [Space(10)]
    public Transform target;

    [Space(10)]
    public float snap = 2f;

    void Update()
    {
        Vector2 position = new Vector2(Mathf.Round(target.position.x / snap) * snap, Mathf.Round(target.position.y / snap) * snap);
        transform.position = position;
    }
}
