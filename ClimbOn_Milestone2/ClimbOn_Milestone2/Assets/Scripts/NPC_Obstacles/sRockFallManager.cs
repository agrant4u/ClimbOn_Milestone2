using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sRockFallManager : MonoBehaviour
{

    public GameObject pRock;

    Transform rockSpawnLocation;

    int maxNumberOfRocks = 10;

    float minSpawnInterval = 0.2f;
    float maxSpawnInterval = 1.5f;

    float minSpawnTime = 1.5f;
    float maxSpawnTime = 4f;

    float minRocksToSpawn = 1f;
    float maxRocksToSpawn = 10f;

    List<GameObject> rockList;

    private void Start()
    {
        rockSpawnLocation = this.gameObject.transform;

        //RockSpawner();


        InvokeRepeating("RockSpawner", Random.Range(minSpawnInterval, maxSpawnInterval), Random.Range(minSpawnTime, maxSpawnTime));
    }

    private void Update()
    {

        //CheckForMaxRocks();

    }

    public void RockSpawner()
    {
        float randomNumberOfRocks = Random.Range(minRocksToSpawn, maxRocksToSpawn);

        for (int i = 0; i < randomNumberOfRocks; i++)
        {
           Instantiate(pRock, rockSpawnLocation.position, Quaternion.identity);
        }
        
        //InvokeRepeating("RockSpawner", Random.Range(minSpawnInterval, maxSpawnInterval), 10f);

    }

    public void CheckForMaxRocks()
    {

        if (rockList.Count == maxNumberOfRocks)
        {

                rockList.RemoveAt(0);     

        }

    }

}
