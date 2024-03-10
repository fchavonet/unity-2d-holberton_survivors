using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Singleton instance to allow easy access from other scripts
    public static CameraShake instance;

    [Space(10)]
    // Duration of the shake effect
    public float shakeDuration = 0.1f;
    // Magnitude of the shake effect
    public float shakeAmount = 0.2f;

    // Flag to check if the camera is already shaking
    private bool isShaking = false;

    void Awake()
    {
        instance = this;
    }

    // Coroutine to perform the shake effect
    private IEnumerator Shake(float shakeDuration, float shakeAmount)
    {
        // If the camera is already shaking, exit the coroutine
        if(isShaking)
        {
            yield return null;
        }
        isShaking = true;
        // Store the original position of the camera
        Vector3 originalCameraPosition = transform.localPosition;

        // Track the elapsed time since the shake started
        float elapsed = 0.0f;

        // Continue shaking until the elapsed time is less than the shake duration
        while (elapsed < shakeDuration)
        {
            // Generate random new x and y positions based on the shake amount
            float x = Random.Range(-1f, 1f) * shakeAmount;
            float y = Random.Range(-1f, 1f) * shakeAmount;

            // Set the camera's position to these new coordinates, keeping the original z position       
            transform.localPosition = new Vector3(originalCameraPosition.x + x, originalCameraPosition.y + y, originalCameraPosition.z);

            // Increment the elapsed time
            elapsed += Time.deltaTime;

            // Wait for the next frame before continuing the loop
            yield return null;
        }

        // Once the shaking duration has finished, reset the camera's position to its original state
        transform.localPosition = originalCameraPosition;
        // Reset the isShaking flag
        isShaking = false;
    }

    // Public method to start the shake effect with specified duration and amount
    public void ShakeIt(float shakeDuration, float shakeAmount)
    {
        // Start the shake coroutine with the given parameters
        StartCoroutine(Shake(shakeDuration, shakeAmount));
    }
}
