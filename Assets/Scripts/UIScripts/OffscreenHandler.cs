using UnityEngine;

public class OffscreenHandler : MonoBehaviour
{
    [SerializeField] Camera camera;

    public void Initialize(Camera cam)
    {
        camera = cam;
    }

    void Update()
    {
        // Check if the object is offscreen
        if (IsOffscreen())
        {
            Destroy(gameObject);
        }
    }

    bool IsOffscreen()
    {
        Vector3 screenPosition = camera.WorldToViewportPoint(transform.position);

        // Check if the position is outside the viewport
        return screenPosition.x < 0 || screenPosition.x > 1 ||
               screenPosition.y < 0 || screenPosition.y > 1 ||
               screenPosition.z < 0;
    }
}
