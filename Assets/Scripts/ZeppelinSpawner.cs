using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeppelinSpawner : MonoBehaviour
{
    public GameObject zeppelinRightPrefab; 
    public GameObject zeppelinLeftPrefab; 
    public Transform rightSpawnPoint;  
    public Transform leftSpawnPoint;  
    public float spawnInterval = 16f;  

    void Start()
    {
        InvokeRepeating("SpawnZeppelins", 0f, spawnInterval);
    }

    void SpawnZeppelins()
    {
        Instantiate(zeppelinRightPrefab, rightSpawnPoint.position, Quaternion.identity);

        Instantiate(zeppelinLeftPrefab, leftSpawnPoint.position, Quaternion.identity);
    }
}
