using UnityEngine;

public class InfiniteGrid : MonoBehaviour
{
    [Space(10)]
    // Target to follow.
    public Transform target;

    [Space(10)]
    // Snap value for grid snapping.
    public float snap = 2f;

    void Update()
    {
        // Calculate the snapped position based on target position and snap value.
        Vector2 position = new Vector2(Mathf.Round(target.position.x / snap) * snap, Mathf.Round(target.position.y / snap) * snap);

        // Set the position of the grid object.
        transform.position = position;
    }
}
