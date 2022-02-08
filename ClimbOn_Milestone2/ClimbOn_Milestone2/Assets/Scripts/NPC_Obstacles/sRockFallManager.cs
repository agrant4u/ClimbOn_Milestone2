using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sRockFallManager : MonoBehaviour
{

    public GameObject pRock;

    Transform rockSpawnLocation;

    public int minRocksToSpawn;
    public int maxRocksToSpawn;

    int randomSpawnAmount;

    public float spawnStartTime = 1f;
    public float spawnTime = 1f;

    float currentSpawnTimer;
    float currentSpawnInterval;

    public float minSpawnInterval = 50f;
    public float maxSpawnInterval = 500f;

    private void Start()
    {
        rockSpawnLocation = this.gameObject.transform;

        RockSpawner();
    }

    private void Update()
    {
     

        
    }

    public void RockSpawner()
    {

        //Vector3 spawnLocation = new Vector3();

        Instantiate(pRock, rockSpawnLocation.position, Quaternion.identity);

        Invoke("RockSpawner", Random.Range(minSpawnInterval, maxSpawnInterval));

    }

}
