using UnityEngine;

public class SpinWeapon : MonoBehaviour
{
    public Transform holder;
    [Space(10)]
    public float rotateSpeed = 180f;

    void Update()
    {
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));
    }
}
