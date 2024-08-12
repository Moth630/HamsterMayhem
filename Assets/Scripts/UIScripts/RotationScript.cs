using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public Vector3 rotationSpeed; // The speed of rotation along each axis

    void Update()
    {
        // Rotate the object continuously
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
