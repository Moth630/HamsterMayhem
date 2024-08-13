using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomPrefabs : MonoBehaviour
{
  public GameObject[] prefab; // Assign the prefab to spawn in the Inspector
  public float spawnInterval = 0.5f; // Time between spawns
  public float spawnRange = 10f; // Range within which prefabs will be spawned
  public int nums;
  public Vector3 rotationSpeed = new Vector3(30f, 0f, 0f);
  private Camera mainCamera;
  public Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f); // Minimum scale
  public Vector3 maxScale = new Vector3(1.5f, 1.5f, 1.5f); // Maximum scale

  private System.Random random = new System.Random();

     void Start()
     {
//       if(prefab != null)
  //     {
    //     nums = prefab.Length;
      // }
         mainCamera = Camera.main;
         Debug.Log("at least this should work");
         InvokeRepeating(nameof(SpawnPrefab), 0f, spawnInterval);
     }

     void SpawnPrefab()
     {
         // Random position within a specified range
         Vector3 spawnPosition = new Vector3(
             Random.Range(-spawnRange, spawnRange),
             Random.Range(-spawnRange, spawnRange),
             0
         );
        // Debug.Log("test tes");

         GameObject selectedPrefab = prefab[random.Next(prefab.Length)];
         GameObject spawnedObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
         if (spawnedObject.GetComponent<Rigidbody>() != null)
         {
           spawnedObject.GetComponent<Rigidbody>().useGravity = true;
         }
         else
         {
           Rigidbody rb = spawnedObject.AddComponent<Rigidbody>();
        rb.useGravity = true; // Ensure gravity is applied to the Rigidbody
         }
         Vector3 randomScale = new Vector3(
           Random.Range(minScale.x, maxScale.x),
           Random.Range(minScale.y, maxScale.y),
           Random.Range(minScale.z, maxScale.z)
       );
       spawnedObject.transform.localScale = randomScale;
       RotationScript rotationScript = spawnedObject.AddComponent<RotationScript>();
       rotationSpeed = new Vector3(
       Random.Range(-60,60),
       Random.Range(-60,60),
       Random.Range(-60,60)
       );
      rotationScript.rotationSpeed = rotationSpeed;
         // Attach an OffscreenHandler to manage offscreen status
         OffscreenHandler offscreenHandler = spawnedObject.AddComponent<OffscreenHandler>();
         offscreenHandler.Initialize(mainCamera);
     }
   }
