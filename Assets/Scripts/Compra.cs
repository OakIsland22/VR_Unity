using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compra : MonoBehaviour
{
    public GameObject compra;
    public Transform spawnPoint;


    void SpawnPrefab()
    {

        Instantiate(compra, spawnPoint.position, spawnPoint.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        SpawnPrefab();
    }
}
