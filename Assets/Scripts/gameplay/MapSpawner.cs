using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public GameObject prefab; // The prefab to instantiate
    public int numberOfClones = 10; // Number of clones to spawn
    public Vector3 areaSize = new Vector3(10, 0, 10); // Size of the area to populate
    public float maxTryCount = 100; // Maximum attempts to find a clear position
    public float checkRadius = 0.5f; // Radius to check for collisions
    void Start()
    {
        PopulateMap();
    }

    void PopulateMap()
    {
        for (int i = 0; i < numberOfClones; i++)
        {
            Vector3 spawnPosition = Vector3.zero;
            bool positionFound = false;

            for (int j = 0; j < maxTryCount; j++)
            {
                // Generate a random position within the area
                spawnPosition = new Vector3(
                    Random.Range(-areaSize.x / 2, areaSize.x / 2),
                    Random.Range(-areaSize.y/2, areaSize.y/2),
                    Random.Range(-areaSize.z / 2, areaSize.z / 2)
                );

                // Check if the position is clear
                if (IsPositionClear(spawnPosition))
                {
                    positionFound = true;
                    break;
                }
            }

            if (positionFound)
            {
                // Instantiate the prefab at the valid position
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Could not find a clear position for prefab after multiple attempts.");
            }
        }
    }

    bool IsPositionClear(Vector3 position)
    {
        // Check for overlaps with colliders within the specified radius
        Collider[] colliders = Physics.OverlapSphere(position, checkRadius);

        // Return true if no colliders are found (position is clear)
        return colliders.Length == 0;
    }
}
