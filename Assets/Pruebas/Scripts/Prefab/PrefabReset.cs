using UnityEngine;

public class PrefabReset : MonoBehaviour
{
    public Transform spawnPoint;
    private GameObject currentObject;
    public GameObject objectPrefab;

    void Start()
    {
        if (objectPrefab != null)
        {
            currentObject = Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void ResetObject()
    {
        if (currentObject != null)
        {
            Destroy(currentObject);
        }
        currentObject = Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}