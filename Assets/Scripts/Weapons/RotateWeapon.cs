using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    [Space(10)]
    public float rotateSpeep = 360f;

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotateSpeep * Time.deltaTime));
    }
}
