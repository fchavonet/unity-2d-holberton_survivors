using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    public float shakeDuration = 0.1f;
    public float shakeAmount = 0.2f;
    private bool isShaking = false;

    void Awake()
    {
        instance = this;
    }

    private IEnumerator Shake(float shakeDuration, float shakeAmount)
    {
        if(isShaking)
        {
            yield return null;
        }
        isShaking = true;
        Vector3 originalCameraPosition = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeAmount;
            float y = Random.Range(-1f, 1f) * shakeAmount;
        
            transform.localPosition = new Vector3(originalCameraPosition.x + x, originalCameraPosition.y+y, originalCameraPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalCameraPosition;
        isShaking = false;
    }

    public void ShakeIt(float shakeDuration, float shakeAmount)
    {
        StartCoroutine(Shake(shakeDuration, shakeAmount));
    }
}
