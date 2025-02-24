using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_NPC : MonoBehaviour
{
    public GameObject NPC; 
    public Transform spawnPoint; 
    public float spawnInterval = 2f; 
    public int maxSpawnedObjects = 10;

    private int spawnedCount = 0; 

    void Start()
    {
        
        InvokeRepeating("SpawnPrefab", 0f, spawnInterval);
    }

    void SpawnPrefab()
    {
        
        if (maxSpawnedObjects == -1 || spawnedCount < maxSpawnedObjects)
        {
            Instantiate(NPC, spawnPoint.position, spawnPoint.rotation);
            spawnedCount++; 
        }
        else
        {
            Debug.Log("Aaaaaltooooo");
            CancelInvoke("SpawnPrefab");
        }
    }

}
