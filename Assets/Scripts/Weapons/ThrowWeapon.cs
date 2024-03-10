using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    [Space(10)]
     // Rigidbody component.
    public Rigidbody2D theRB;

    [Space(10)]
    // Power of throw in X direction.
    public float throwPowerX; 
    // Power of throw in Y direction.
    public float throwPowerY; 

    [Space(10)]
    // Rotation speed.
    public float rotateSpeed; 

    void Start()
    {
        // Set initial velocity for throwing.
        theRB.velocity = new Vector2(Random.Range(-throwPowerX, throwPowerX), throwPowerY);
    }

    void Update()
    {
        // Rotate the weapon based on velocity.
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotateSpeed * 360f * Time.deltaTime * Mathf.Sign(theRB.velocity.x)));
    }
}
