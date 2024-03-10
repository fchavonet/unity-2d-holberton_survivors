using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    [Space(10)]
    // Rotation speed.
    public float rotateSpeed = 360f;

    void Update()
    {
        // Rotate the weapon.
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));
    }
}
