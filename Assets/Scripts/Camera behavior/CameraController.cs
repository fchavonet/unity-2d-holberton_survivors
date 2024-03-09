using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Reference to the target transform (in this case, the player's transform)
    private Transform target;

    private void Awake()
    {
        // Find and assign the PlayerController's transform as the target
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void FixedUpdate()
    {
        // Update the camera's position to match the target's X and Y position, but keep its own Z position
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
